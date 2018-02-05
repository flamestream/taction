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

			obj.init(value);
		},
		addValueElement(state, { parent, key, data }) {

			// @TODO Do it right
			if (!parent) return false;

			let values = parent.value[key];
			if (!Array.isArray(values))
				return false;

			parent.init({[key]: data}, {
				append: true
			});
		},
		removeValueElement(state, { parent, key, obj }) {

			// @TODO Do it right
			if (!parent) return false;

			let values = parent.value[key];
			if (!Array.isArray(values)) return false;

			parent.value[key] = values.filter(i => i !== obj);
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
