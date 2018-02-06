import Vue from 'vue'
import Vuex from 'vuex'

import LayoutType from './layout/LayoutType'
import registry from './layout/registry'

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		layout: new LayoutType(),
		zip: undefined,
		activeItem: undefined,
		registry // @Note: Exceptional
	},
	getters: {
		assets(state) {

			let { zip } = state;
			if (!zip)
				return [];

			return Object.keys(zip.files);
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
		setActiveItem(state, item) {

			state.activeItem = item;
		}
	},
	actions: {
		reset({commit}, {data}) {

			let {layout, zip} = data || {};

			registry.$clear();
			commit('resetLayout', layout);
			commit('resetZip', zip);
		},
		setActiveItem({commit, getters}, {id}) {

			let item = getters.registered('ItemType', id);
			commit('setActiveItem', item);
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
		}
	}
});
