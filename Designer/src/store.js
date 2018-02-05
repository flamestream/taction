import Vue from 'vue'
import Vuex from 'vuex'

import LayoutType from './layout/LayoutType'
import registry from './layout/registry'

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		layout: new LayoutType(),
		zip: undefined,
		registry // @Note: Exceptional
	},
	getters: {
		assets(state) {

			let out = {};
			if (state.zip) {

				out = state.zip.files;
			}

			return out;
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

			obj.pullElement && obj.pullElement(key, value);
		}
	},
	actions: {
		reset({commit}, {data}) {

			let {layout, zip} = data || {};

			registry.$clear();
			commit('resetLayout', layout);
			commit('resetZip', zip);
		}
	}
});
