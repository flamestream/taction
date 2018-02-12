const state = {
	activeMenu: undefined,
	activeItem: undefined,
	activeAsset: undefined,
	errorMsg: undefined
};

const getters = {
	activeAssetName(state) {

		return state.activeAsset && state.activeAsset.name;
	},
	activeFont(state) {

		let {activeAsset} = state;
		if (!activeAsset)
			return;

		if (!activeAsset.name.endsWith('.ttf'))
			return;

		return activeAsset;
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
	},
	setErrorMsg(state, {msg}) {

		state.errorMsg = msg;
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
	},
	setErrorMsg({commit}, {msg} = {}) {

		commit('setErrorMsg', {msg});
	}
};

export default {
	namespaced: true,
	state,
	getters,
	mutations,
	actions
};
