<template>
	<div class="slide-menu-container">
		<transition name="menu" @after-enter="onMenuAfterEnter">
			<ListSelector v-if="isSubActive"
				ref="selector"
				:items="inactiveComponents"
				:fnLabelMap="(i) => { return i.label }"
				@back="focusMain"
				@select="onInactiveComponentSelect">
			</ListSelector>
			<div v-else class="main">
				<div v-if="inactiveComponents.length" class="add-button" @click.stop="focusSub">Add Component</div>
				<PropertyItem label="Mandatory Properties" type="item" :obj="activeItem"></PropertyItem>
				<div class="component-container">
					<ComponentProperty v-for="c in activeComponents"
						:typeMeta="c"
						:activeItem="activeItem"
						:key="c.name"
						@inactive="onComponentInactive">
					</ComponentProperty>
				</div>
			</div>
		</transition>
	</div>
</template>

<script>
import { get } from 'lodash'
import ComponentProperty from './ComponentProperty'
import PropertyItem from './PropertyItem'
import ListSelector from './ListSelector'
import itemComponentDefinitions from '@/definitions/item-components';
export default {
	name: 'ViewPropertiesActive',
	components: {
		PropertyItem,
		ComponentProperty,
		ListSelector
	},
	props: {
		activeItem: { type: Object }
	},
	data() {
		return {
			isSubActive: false,
			activeComponents: [],
			inactiveComponents: []
		}
	},
	methods: {
		focusMain() {
			this.isSubActive = false;
		},
		focusSub() {
			this.isSubActive = true;
		},
		setComponentDefined(c) {

			let obj = this.activeItem.getObj(c.path, true);
			this.$store.commit({
				type: 'layout/setDefined',
				obj,
				value: true
			});

			// Make it appear on top
			this.activeComponents.unshift(c);
			this.inactiveComponents = this.inactiveComponents.filter(i => i !== c);
		},
		refreshComponents() {

			this.activeComponents = [];
			this.inactiveComponents = [];
			for (let k in itemComponentDefinitions) {

				let c = itemComponentDefinitions[k];
				if (this.activeItem.getObj(c.path))
					this.activeComponents.push(c);
				else
					this.inactiveComponents.push(c);
			}
		},
		onComponentInactive(ev) {

			let { component } = ev;
			this.inactiveComponents.unshift(component);
			this.activeComponents = this.activeComponents.filter(i => i !== component);
		},
		onInactiveComponentSelect(c) {

			this.focusMain();
			this.setComponentDefined(c)
		},
		onMenuAfterEnter(el, rawr) {

			if (typeof get(el, '__vue__.focus') === 'function')
				el.__vue__.focus();
		}
	},
	watch: {
		activeItem() {
			this.refreshComponents();
			this.focusMain();
		}
	},
	mounted() {
		this.refreshComponents();
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.slide-menu-container {
	position: relative;
	overflow: hidden;
	flex: 1 1 auto;
}

.slide-menu-container:hover {
	overflow-y: auto;
}

.slide-menu-container::-webkit-scrollbar
{
	width: 5px;
	background-color: #FFFFFF33;
}

.slide-menu-container::-webkit-scrollbar-thumb
{
	background-color: #FFFFFFAA;
	border-radius: 10px;
}

.slide-menu-container > div {
	left: 0;
	right: 0;
	padding: 12px 6px 0;
}

.menu-leave-active, .menu-enter-active {
	position: absolute;
	transition: all 0.3s;
}

.main.menu-leave-to, .main.menu-enter {
	transform: translateX(-100%);
}

.menu-leave-to, .menu-enter {
	transform: translateX(100%);
}

</style>
