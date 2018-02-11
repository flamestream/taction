const state = {
	activeMenu: undefined,
	activeItem: undefined,
	activeAsset: undefined
};

const getters = {
	activeAssetName(state) {

		return state.activeAsset && state.activeAsset.name;
	}
};

const mutations = {
	setActiveMenu(state, {id}) {

		state.activeMenu = id;
	},
	setActiveItem(state, {item}) {

		state.activeItem = item;
	},
	setActiveAsset(state, {asset}) {

		state.activeAsset = asset;
	}
};

const actions = {
	setActiveMenu({commit}, {id} = {}) {

		commit('setActiveMenu', {id});
	},
	setActiveItem({commit, rootGetters}, {id} = {}) {

		let item = rootGetters.registered({type: 'ItemType', id});
		commit('setActiveItem', {item});
	},
	setActiveAsset({commit, state, rootGetters}, {id} = {}) {

		let asset = rootGetters['assets/item']({id});
		commit('setActiveAsset', {asset});

		if (asset && asset.ext === 'ttf') {

			var style = document.getElementById('active-font-style');
			style.innerHTML = `@font-face { font-family: 'Active Font'; src: url('${asset.url}'); }`;
		}
	}
};

export default {
	namespaced: true,
	state,
	getters,
	mutations,
	actions
};
