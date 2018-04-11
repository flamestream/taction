<template>
	<div class="base-section-properties-content properties-root">
		<div :class="mainClassNames">
			<div v-if="inactiveComponents.length" class="add-button" @click.stop="focusSub">Add Component</div>
			<PropertyItem label="Mandatory Properties" type="item" :obj="activeItem"></PropertyItem>
			<div class="component-container">
				<ComponentProperty v-for="c in activeComponents" :typeMeta="c" :activeItem="activeItem" :key="c.name" @inactive="onInactiveComponent"></ComponentProperty>
			</div>
			<!-- <PropertyItem label="Style" type="button-style-set" :obj="activeItem.value.style"></PropertyItem> -->
		</div>
		<div :class="subClassNames">
			<div class="command">
				<i class="material-icons" @click.stop="focusMain()">arrow_back</i>
				<input ref="search" type="text" @input="filterInactiveComponents" @keyup.enter="onSearchEnter" @keyup.esc="onSearchEsc" />
			</div>
			<div v-for="c in filteredInactiveComponents" :class="inactiveComponentClassNames(c)" :key="c.label" @mouseenter="selectInactiveComponents(c)" @click.stop="setDefined(c)">{{ c.label }}</div>
		</div>
	</div>
</template>

<script>
import ComponentProperty from './ComponentProperty'
import PropertyItem from './PropertyItem'
import itemComponentDefinitions from '@/definitions/item-components';
export default {
	name: 'ViewPropertiesActive',
	components: {
		PropertyItem,
		ComponentProperty
	},
	props: {
		activeItem: { type: Object }
	},
	data() {
		return {
			isSubActive: false,
			itemComponentDefinitions,
			activeComponents: [],
			inactiveComponents: [],
			filteredInactiveComponents: null,
			selectedInactiveComponent: null
		}
	},
	computed: {
		mainClassNames() {
			return {
				main: true,
				inactive: this.isSubActive
			}
		},
		subClassNames() {
			return {
				sub: true,
				active: this.isSubActive
			}
		},
		sortedInactiveComponents() {

			var out = this.inactiveComponents.slice();
			out.sort((a, b) => {

				var A = a.label.toUpperCase();
				var B = b.label.toUpperCase();

				if (A > B) return 1;
				if (A < B) return -1;
				return 0;
			});

			return out;
		}
	},
	methods: {
		inactiveComponentClassNames(component) {
			return {
				component: true,
				selected: this.selectedInactiveComponent === component,
				invisible: component.invisible
			}
		},
		selectInactiveComponents(component) {
			this.selectedInactiveComponent = component;
		},
		focusMain() {
			this.isSubActive = false;
		},
		focusSub() {

			this.isSubActive = true;
			this.selectedInactiveComponent = null;
			this.$refs.search.value = '';
			this.filterInactiveComponents();
			setTimeout(() => this.$refs.search.focus(), 200); // After animation
		},
		setDefined(component) {

			let obj = this.activeItem.getObj(component.path, true);
			this.$store.commit({
				type: 'layout/setDefined',
				obj,
				value: true
			});

			// Make it appear on top
			this.activeComponents.unshift(component);
			this.inactiveComponents = this.inactiveComponents.filter(i => i !== component);

			this.focusMain();
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
		onInactiveComponent(ev) {

			let { component } = ev;
			this.inactiveComponents.unshift(component);
			this.activeComponents = this.activeComponents.filter(i => i !== component);
		},
		filterInactiveComponents() {

			let { value } = this.$refs.search;

			this.filteredInactiveComponents = this.sortedInactiveComponents.slice();

			if (value)
				value = value.toUpperCase();

			let firstVisible = null;
			for (let c of this.filteredInactiveComponents) {

				c.invisible = !c.label.toUpperCase().includes(value);
				if (!firstVisible && !c.invisible)
					firstVisible = c;
			}

			let { selectedInactiveComponent } = this;
			if (!selectedInactiveComponent || selectedInactiveComponent.invisible)
				this.selectedInactiveComponent = firstVisible;
		},
		onSearchEnter() {

			if (!this.selectedInactiveComponent)
				return;

			this.setDefined(this.selectedInactiveComponent);
		},
		onSearchEsc() {
			this.focusMain();
		}
	},
	watch: {
		activeItem() {
			this.refreshComponents();
		}
	},
	mounted() {
		this.refreshComponents();
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.properties-root {
	position: relative;
	overflow: hidden;
	width: 100%;
	height: 100%;
}

.properties-root:hover {
	overflow-y: auto;
}

.properties-root::-webkit-scrollbar
{
	width: 5px;
	background-color: #FFFFFF33;
}

.properties-root::-webkit-scrollbar-thumb
{
	background-color: #FFFFFFAA;
	border-radius: 10px;
}

.properties-root > div {
	position: absolute;
	width: 280px; /* Container - margin */
	margin: 0 10px;
}

.main {
	left: 0;
	transition: left 0.2s ease-out;
}

.main.inactive {
	left: -300px;
}

.sub {
	left: 300px;
	transition: left 0.2s ease-out;
	display: flex;
	flex-direction: column;
}

.sub .command {
	display: flex;
	margin-bottom: 8px;
}

.sub .command i {
	cursor: pointer;
	margin: 0 4px;
}
.sub .command i:hover {
	color: #FFF;
}

.sub .command input {
	flex: 1 1 auto;
	border: 0;
	border-radius: 3px;
	padding: 2px 4px;
}

.sub.active {
	left: 0;
}

.sub .component {
	cursor: pointer;
	font-size: 15px;
	padding: 4px 8px;
	text-align: center;
	overflow: hidden;
}

.sub .component.invisible {
	height: 0;
	padding: 0;
}

.sub .component.selected {
	color: #FFF;
	background: linear-gradient(90deg, transparent 0%, #00000066 20%, #00000066 80%, transparent 100%);
	font-size: 16px;
	padding-top: 8px;
	padding-bottom: 8px;
}

</style>
