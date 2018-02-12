import Vue from 'vue';
import filesize from 'filesize'
import _ from 'lodash'
import config from '@/config';
import fileTypeAsync from '@/helpers/file-type-async'

const state = {
	registry: {}
};

const getters = {
	item: (state) => ({id}) => {

		return state.registry[id];
	},
	names(state) {

		return Object.keys(state.registry);
	},
	fonts(state) {

		var out = _.pickBy(state.registry, (value, key) => _.endsWith(key, '.ttf'));
		return out;
	}
};

const mutations = {
	reset(state) {

		state.registry = {};
	},
	add(state, {id, file}) {

		Vue.set(state.registry, id, file);
	},
	remove(state, {id}) {

		let asset = state.registry[id];
		if (asset === state.activeAsset)
			state.activeAsset = undefined;

		Vue.delete(state.registry, id);
	}
};

const actions = {
	async loadZip({commit, dispatch}, {zip}) {

		// Load files
		let assets = [];
		for (let name in zip.files) {

			let file = zip.files[name];

			// Directory check
			if (file.dir || ~name.indexOf('/'))
				continue;

			// Ext check
			let ext = name.split('.').pop();
			if (!config.supportedImportExts.includes(ext))
				continue;

			let content = await zip.file(name).async('arraybuffer');

			// Type check
			var blob = new Blob([content]);
			let type = await fileTypeAsync(blob);
			if (!config.supportedImportExts.includes(type.ext))
				continue;

			blob.name = name;
			assets.push(blob);
		}

		let errors;
		for (let file of assets) {

			try {

				await dispatch('add', {file});

			} catch (err) {

				if (!errors) errors = [];
				errors.push(err);
			}
		}

		return {
			errors
		};
	},
	async add({commit, dispatch}, {file, active}) {

		let {name} = file;

		// Size check
		let {maximumAssetSize} = config;
		if (file.size > maximumAssetSize) {

			let maxLabel = filesize(maximumAssetSize);
			throw new Error(`${name}: File is too big (Maximum ${maxLabel})`);
		}

		let ext = name.split('.').pop();
		let type = await fileTypeAsync(file);

		if (!type)
			throw new Error(`${name}: Error reading file`);

		// Type check
		if (!config.supportedImportExts.includes(type.ext))
			throw new Error(`${name}: File type is not supported`);

		// Ext vs. Type check
		if (ext !== type.ext)
			throw new Error(`${name}: Invalid ${ext} file`);

		// Add additional information
		file.ext = ext;
		file.url = URL.createObjectURL(file);

		commit('add', {
			id: file.name,
			file
		});

		if (active)
			dispatch('ui/setActiveAsset', {id: file.name}, {root: true});
	},
	remove({commit}, {id}) {

		commit('remove', {id});
	}
};

export default {
	namespaced: true,
	state,
	getters,
	mutations,
	actions
};
