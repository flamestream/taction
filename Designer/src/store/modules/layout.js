import defaultLayout from '@/layouts/default'
import LayoutType from '@/types/LayoutType'

const state = {
	layout: new LayoutType({value: defaultLayout})
};

const getters = {
	json(state) {

		return JSON.stringify(state.layout, 4, 4);
	}
};

const mutations = {
	reset(state, {layout}) {

		state.layout = new LayoutType({value: layout});
	},
	setValue(state, {obj, value}) {

		// @TODO Do it right
		obj.init(value);
	},
	setDefined(state, {obj, value}) {

		// @TODO Do it right
		obj.notDefined = !value;
	},
	changeType(state, {obj, value}) {

		// @TODO Do it right
		obj.changeType(value);
	},
	addValueElement(state, {obj, key, value}) {

		// @TODO Do it right
		obj.pushElement && obj.pushElement(key, value);
	},
	removeValueElement(state, {obj, key, value}) {

		// @TODO Do it right
		obj.pullElement && obj.pullElement(key, value);
	}
};

const actions = {
	reset({commit}, {layout}) {

		if (!layout)
			layout = defaultLayout;

		commit('reset', {layout});
		commit('ui/setActiveItem', {}, {root: true});

		return true;
	},
	setValue({commit}, {obj, value}) {

		commit('setValue', {
			obj,
			value
		});

		return true;
	},
	setDefined({commit}, {obj, value}) {

		commit('setDefined', {
			obj,
			value
		});

		return true;
	},
	changeType({commit}, {obj, value}) {

		commit('changeType', {
			obj,
			value
		});

		return true;
	},
	addItem({state, commit, rootGetters}, {value, parent, active}) {

		let obj = parent || state.layout;

		// Check if possible
		if (!obj.pushElement)
			return false;

		commit('addValueElement', {
			key: 'items',
			value,
			obj
		});

		if (active) {

			let item = rootGetters.lastRegistered({type: 'ItemType'})
			commit('ui/setActiveItem', {item}, {root: true});
		}

		return true;
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
			commit('ui/setActiveItem', undefined, {root: true});

		return true;
	}
};

export default {
	namespaced: true,
	state,
	getters,
	mutations,
	actions
};
