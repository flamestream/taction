import Vue from 'vue'
import Vuex from 'vuex'
import fileType from 'file-type'
import filesize from 'filesize'
import fileReaderAsync from './helpers/fileReaderAsync'

import config from './config'
import LayoutType from './types/LayoutType'
import registry from './types/registry'

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		layout: new LayoutType({value: config.defaultLayout}),
		assetRegistry: {},
		activeMenu: undefined,
		activeItem: undefined,
		activeAsset: undefined,
		registry // @Note: Exceptional
	},
	getters: {
		activeAssetName(state) {

			return state.activeAsset && state.activeAsset.name;
		},
		assetNames(state) {

			return Object.keys(state.assetRegistry);
		},
		registered: (state) => (type, id) => {

			// Type check
			let typeRegistry = state.registry[type];
			if (!typeRegistry) return;

			return typeRegistry[id];
		}
	},
	mutations: {
		resetLayout(state, layout) {

			state.layout = new LayoutType({value: layout});
		},
		resetZip(state, zip) {

			// Clean up memory
			if (state.zip) {
				state.zip.remove('/');
			}

			// Update zip
			state.zip = zip && typeof zip === 'object'
				? zip
				: undefined;
			console.log('zip!', state);
		},
		setValue(state, { obj, value }) {

			// @TODO Do it right
			obj.init(value);
		},
		setDefined(state, { obj, value }) {

			// @TODO Do it right
			obj.notDefined = !!value;
		},
		changeType(state, { obj, value }) {

			// @TODO Do it right
			obj.changeType && obj.changeType(value);
		},
		addValueElement(state, { obj, key, value }) {

			// @TODO Do it right
			obj.pushElement && obj.pushElement(key, value);
		},
		removeValueElement(state, { obj, key, value }) {

			// @TODO Do it right
			obj.pullElement && obj.pullElement(key, value);
		},
		setActiveMenu(state, id) {

			state.activeMenu = id;
		},
		setActiveItem(state, item) {

			state.activeItem = item;
			console.log(item);
		},
		setActiveAsset(state, {asset}) {

			state.activeAsset = asset;
		},
		addAsset(state, {id, file}) {

			Vue.set(state.assetRegistry, id, file);
		},
		removeAsset(state, {id}) {

			let asset = state.assetRegistry[id];
			if (asset === state.activeAsset)
				state.activeAsset = undefined;

			Vue.delete(state.assetRegistry, id);
		}
	},
	actions: {
		reset({commit}, {layout, zip}) {

			registry.$clear();
			commit('resetLayout', layout);
			commit('resetZip', zip);
		},
		setActiveMenu({commit}, {id}) {

			commit('setActiveMenu', id);
		},
		setActiveItem({commit, getters}, {id}) {

			let asset = getters.registered('ItemType', id);
			commit('setActiveItem', asset);
		},
		addItem({state, commit}, {value, parent}) {

			let obj = parent || state.layout;

			commit('addValueElement', {
				key: 'items',
				value,
				obj
			});

			// @TODO Find better way
			let itemRegistry = registry['ItemType'];
			let newItem = itemRegistry[itemRegistry._maxId];
			commit('setActiveItem', newItem);
		},
		removeItem({state, commit}, {item, parent}) {

			let obj = parent || state.layout;

			commit('removeValueElement', {
				key: 'items',
				value: item,
				obj
			});

			let activeItem = state.activeItem;
			if (activeItem === item)
				commit('setActiveItem', undefined);
		},
		setActiveAsset({commit, state, getters}, {id}) {

			let asset = state.assetRegistry[id];
			commit('setActiveAsset', {asset});

			if (asset.ext === 'ttf') {

				var style = document.getElementById('active-font-style');
				style.innerHTML = `@font-face { font-family: 'Active Font'; src: url('${asset.url}'); }`;
			}
		},
		async addAsset({commit, dispatch}, {file, active}) {

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

			commit('addAsset', {
				id: file.name,
				file
			});

			if (active) {

				dispatch('setActiveAsset', {
					id: file.name
				});
			}
		},
		removeAsset({commit}, {id}) {

			commit('removeAsset', {id});
		}
	}
});
