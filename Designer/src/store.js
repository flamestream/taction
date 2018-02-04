import Vue from 'vue'
import Vuex from 'vuex'

import GlobalType from './layout/GlobalType'
import registry from './layout/registry'

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		layout: {},
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

			state.layout = new GlobalType({value: layout});
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

			if (!parent) return false;

			let values = parent.value[key];
			if (!Array.isArray(values))
				return false;

			parent.init({[key]: data}, {
				append: true
			});
		},
		removeValueElement(state, { parent, key, obj }) {

			if (!parent) return false;

			let values = parent.value[key];
			console.log('array check', values);
			if (!Array.isArray(values)) return false;

			console.log('okay');
			parent.value[key] = values.filter(i => i !== obj);
		}
	},
	actions: {
		reset({commit}, {data}) {

			let {layout, zip} = data || {};
			commit('resetLayout', layout);
			commit('resetZip', zip);
		}
	}
});
