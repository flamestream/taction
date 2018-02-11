import Vue from 'vue';
import Vuex from 'vuex';
import layout from './modules/layout'
import assets from './modules/assets'
import ui from './modules/ui'
import registry from '@/types/registry'

Vue.use(Vuex)

export default new Vuex.Store({
	modules: {
		layout,
		assets,
		ui
	},
	state: {
		registry // @Note: Exceptional
	},
	getters: {
		registered: (state) => ({type, id}) => {

			// Type check
			let typeRegistry = state.registry[type];
			if (!typeRegistry) return;

			return typeRegistry[id];
		},
		lastRegistered: (state) => ({type}) => {

			let itemRegistry = registry[type];
			if (!itemRegistry)
				return;

			return itemRegistry[itemRegistry._maxId];
		}
	},
	actions: {
		reset({dispatch}, {layout, zip}) {

			registry.$clear();
			dispatch('layout/reset', {layout});
			if (zip) dispatch('assets/loadZip', {zip})
		}
	}
})
