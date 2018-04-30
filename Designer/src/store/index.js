import Vue from 'vue';
import Vuex from 'vuex';
import layout from './modules/layout'
import assets from './modules/assets'
import ui from './modules/ui'
import registry from '@/types/registry'
import layouts from '@/definitions/layouts.json'
import importAsync from '@/helpers/import-async'

Vue.use(Vuex)

export default new Vuex.Store({
	modules: {
		layout,
		assets,
		ui
	},
	state: {
		registry, // @Note: Exceptional
		cachedLayoutRegistry: {}
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
	mutations: {
		cacheTemplateLayout(state, {name, file}) {

			state.cachedLayoutRegistry[name] = file;
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
		},
		async loadTemplateLayout({dispatch, commit, state}, {name} = {}) {

			let layout, zip;

			// Check cache
			let file = state.cachedLayoutRegistry[name];
			if (!file) {

				let match = layouts.find(l => l.name === name);
				if (!match)
					throw new Error(`Could not find layout named '${name}'`);

				let blob;
				if (match.code) {

					layout = match.code;

				} else if (match.url) {

					let resp = await fetch(match.url);
					if (resp.status !== 200)
						throw new Error('Could not fetch layout file');

					blob = await resp.blob();

					let fileName = resp.url;
					try {

						file = new File([blob], fileName);

					} catch (e) { // Assume msedge issue

						/* eslint no-proto: 0 */
						file = blob;
						file.lastModifiedDate = new Date();
						file.name = name;
						file.__proto__ = File.prototype;
					}

					commit('cacheTemplateLayout', {
						name: name,
						file
					});

				} else {

					throw new Error(`Invalid layout data '${name}'`);
				}
			}

			if (file) {

				let imported = await importAsync({file});
				layout = imported.layout;
				zip = imported.zip;
			}

			dispatch('reset', {layout, zip});
		}
	}
})
