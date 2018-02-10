import Vue from 'vue';
import fileType from 'file-type'
import filesize from 'filesize'
import fileReaderAsync from '@/helpers/fileReaderAsync'
import config from '@/config';

const state = {
	registry: {}
};

const getters = {
	item: (state) => ({id}) => {

		return state.registry[id];
	},
	names(state) {

		return Object.keys(state.registry);
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
	async add({commit, dispatch}, {file, active}) {

		let {name} = file;

		// Size check
		let {maximumAssetSize} = config;
		if (file.size > maximumAssetSize) {

			let maxLabel = filesize(maximumAssetSize);
			throw new Error(`${name}: File is too big (Maximum ${maxLabel})`);
		}

		let ext = name.split('.').pop();
		let header = file.slice(0, 4100);
		let blob = await fileReaderAsync(header);
		let type = fileType(blob);

		if (!type)
			throw new Error(`${name}: Error reading file`);

		// Type check
		if (!config.supportedMimeTypes.includes(type.mime))
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
