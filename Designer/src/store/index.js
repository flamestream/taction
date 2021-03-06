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
		async reset({dispatch}, {layout, zip} = {}) {

			registry.$clear();
			await dispatch('layout/reset', {layout});
			if (zip)
				await dispatch('assets/loadZip', {zip})
			else
				await dispatch('assets/clear', {zip})
		},
		async setErrorMsg({dispatch}, {msg} = {}) {

			await dispatch('ui/setErrorMsg', {msg});
		}
	}
})
