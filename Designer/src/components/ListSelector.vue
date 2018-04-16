<template>
	<div class="root">
		<div class="command">
			<i class="material-icons" @click.stop="emitBack">arrow_back</i>
			<input ref="search" type="text"
				@input="filterItems"
				@keyup.enter="onSearchEnter"
				@keyup.esc="emitBack"
				@keydown.up="moveFocusedIndex(-1)"
				@keydown.down="moveFocusedIndex(1)"
			/>
		</div>
		<transition-group name="list" class="items section-details-scrollable">
			<div v-for="(item, idx) in filteredDisplayItems" ref="items"
				:key="item.label"
				:class="itemClassNames(idx)"
				@mouseenter="focusedIndex = idx"
				@click.stop="emitSelect(item)">{{ item.label }}</div>
		</transition-group>
	</div>
</template>

<script>
export default {
	name: 'ListSelector',
	props: {
		items: { type: Array },
		fnLabelMap: { type: Function }
	},
	data() {
		return {
			filteredDisplayItems: [],
			focusedIndex: null
		}
	},
	computed: {
		displayItems() {

			/* eslint no-mixed-operators: 0 */
			return this.items.map(i => {
				return {
					original: i,
					label: this.fnLabelMap && this.fnLabelMap(i) || i
				}
			});
		}
	},
	methods: {
		focus() {
			this.$refs.search.focus();
		},
		emitBack() {
			this.$emit('back');
		},
		emitSelect(item) {
			this.$emit('select', item.original);
		},
		itemClassNames(idx) {
			return {
				item: true,
				selected: this.focusedIndex === idx
			}
		},
		filterItems() {

			let out = this.displayItems.slice();
			let { value } = this.$refs.search;
			if (value) {

				value = value.toUpperCase();
				out = out.filter(c => c.label.toUpperCase().includes(value));
			}

			this.focusedIndex = 0;
			this.filteredDisplayItems = out;
		},
		onSearchEnter() {

			let item = this.filteredDisplayItems[this.focusedIndex];
			if (!item)
				return;

			this.emitSelect(item);
		},
		moveFocusedIndex(i = 0) {

			let out = this.focusedIndex + i;
			let len = this.filteredDisplayItems.length;
			out = ((out % len) + len) % len;
			this.focusedIndex = out;
			this.$refs.items[out].scrollIntoView({ block: 'end' });
		}
	},
	mounted() {
		this.filterItems();
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.root {
	display: flex;
	flex-direction: column;
}

.command {
	display: flex;
	margin-bottom: 8px;
	flex-shrink: 0;
}

.command i {
	cursor: pointer;
	margin-right: 4px;
}
.command i:hover {
	color: #FFF;
}

.command input[type=text] {
	flex: 1 1 auto;
}

.items {
	flex: 1 1 auto;
	overflow-y: auto;
}

.item {
	cursor: pointer;
	font-size: 15px;
	padding: 4px 8px;
	text-align: center;
	overflow: hidden;
	transition: all 0.2s ease-out;
}

.item.selected {
	color: #FFF;
	background: linear-gradient(90deg, transparent 0%, #00000066 20%, #00000066 80%, transparent 100%);
	font-size: 16px;
	padding-top: 8px;
	padding-bottom: 8px;
}

.list-enter-active, .list-leave-active {
	transition: all 0.2s ease-out;
}

.list-enter, .list-leave-to {
	opacity: 0;
}

.list-leave-active {
	position: absolute; /* For move effect */
	left: 0;
	right: 0;
}

.list-move {
	transition: transform 0.2s ease-out;
}

</style>
