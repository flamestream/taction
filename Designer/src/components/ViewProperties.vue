<template>
	<div :class="rootClassNames">
		<transition name="menu" @enter="onMenuEnter" @after-enter="onMenuAfterEnter">
			<ListSelector v-if="isSubActive"
				ref="selector"
				:items="inactiveComponents"
				:fnLabelMap="(i) => { return i.label }"
				@back="focusMain"
				@select="onInactiveComponentSelect">
			</ListSelector>
			<div v-else class="main">
				<div v-if="inactiveComponents.length" class="add-button" @click.stop="focusSub">Add Component</div>
				<div class="component-container">
					<ComponentProperty v-for="c in activeComponents"
						:typeMeta="c"
						:rootObj="rootObj"
						:key="c.name"
						@change="onComponentChange"
						@inactive="onComponentInactive">
					</ComponentProperty>
				</div>
			</div>
		</transition>
	</div>
</template>

<script>
import { get } from 'lodash'
import { mapState } from 'vuex'
import ComponentProperty from './ComponentProperty'
import ListSelector from './ListSelector'
import itemComponentDefinitions from '@/definitions/item-components';
import globalComponentDefinitions from '@/definitions/global-components';
export default {
	name: 'ViewProperties',
	components: {
		ComponentProperty,
		ListSelector
	},
	data() {
		return {
			isMenuTransitioning: false,
			isSubActive: false,
			activeComponents: [],
			inactiveComponents: []
		}
	},
	computed: {
		...mapState('layout', ['layout']),
		...mapState('ui', ['activeItem']),
		rootClassNames() {

			return {
				'slide-menu-container': true,
				'section-details-scrollable': !this.isMenuTransitioning,
				'slide-menu-container-transition': this.isMenuTransitioning
			}
		},
		rootObj() {
			return this.activeItem || this.layout;
		},
		definitions() {
			return this.activeItem ? itemComponentDefinitions : globalComponentDefinitions;
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

			let obj = this.rootObj.getObj(c.path, true);
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

			for (let c of this.definitions) {

				if (this.rootObj.getObj(c.path))
					this.activeComponents.push(c);
				else if (!c.required)
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
		onMenuEnter(el) {

			this.isMenuTransitioning = true;
		},
		onMenuAfterEnter(el) {

			this.isMenuTransitioning = false;
			if (typeof get(el, '__vue__.focus') === 'function')
				el.__vue__.focus();
		},
		onComponentChange(ev) {

			let { component } = ev;
			if (!component.type)
				return;

			this.refreshComponents();
		}
	},
	watch: {
		rootObj() {
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
	display: flex;
	padding: 0 6px;
	padding-right: 1px; /*scrollbar*/
	overflow-x: hidden;
	overflow-y: scroll;
}

.slide-menu-container::-webkit-scrollbar-thumb {
	background-color: transparent;
}

.slide-menu-container-transition {
	margin-left: 6px; /* innerWidth padding */
	padding: 0;
}

.slide-menu-container::-webkit-scrollbar
{
	background-color: transparent;
}
.section-details-scrollable::-webkit-scrollbar-thumb
{
	/*background-color: transparent;*/
	transition: all 0.2s ease-out;
}
.section-details-scrollable:hover::-webkit-scrollbar-thumb
{
	background-color: #FFFFFFAA;
}

.slide-menu-container > div {
	left: 0;
	right: 0;
	padding: 12px 0 0;
	flex: 1 1 auto;
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

.component-container:last-child {
	margin-bottom: 100%;
}

</style>
