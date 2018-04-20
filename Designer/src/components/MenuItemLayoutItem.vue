<template>
	<div v-if="value" :class="{ 'menu-item': true, active }" :data-id="obj.id" @mouseenter="handleMouseEnter">
		<div class="label">
			<span v-for="i in depth" class="spacer" :key="i"></span>
			<span class="text">
				{{ value.type.value }}
				<span v-if="value.command" class="key">{{ value.command.value }}</span>
			</span>
			<span v-if="active" class="command" @click.stop="handleRemoverClick">－</span>
			<span v-if="active && value.type.value === 'pivot'" class="command" @click.stop="handleAdderClick">＋</span>
		</div>
		<div v-if="value.items">
			<MenuItemLayoutItem v-for="item in value.items" :obj="item" :key="item.id" :parent="obj" :depth="depth + 1"></MenuItemLayoutItem>
		</div>
	</div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import MenuItemLayoutItem from './MenuItemLayoutItem'
import ItemType from '../types/ItemType';
export default {
	name: 'MenuItemLayoutItem',
	props: {
		obj: { type: ItemType },
		parent: { type: Object },
		depth: { type: Number, default: 0 }
	},
	components: {
		MenuItemLayoutItem
	},
	computed: {
		...mapState('ui', ['activeMenu', 'activeItem']),
		active() {

			return !this.activeMenu && this.activeItem === this.obj;
		},
		value() {

			let obj = this.obj || {};
			return obj.value;
		}
	},
	methods: {
		...mapActions({
			setHighlightItem: 'ui/setHighlightItem'
		}),
		handleRemoverClick(ev) {

			this.$store.dispatch({
				type: 'layout/removeItem',
				item: this.obj,
				parent: this.parent
			});
		},
		handleAdderClick(ev) {

			this.$store.dispatch({
				type: 'layout/addItem',
				parent: this.obj,
				value: {
					type: 'hold'
				},
				active: true
			});
		},
		handleMouseEnter(ev) {

			let { id } = this.obj;
			this.setHighlightItem({id});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.label {
	display: flex;
	align-items: center;
}

.text {
	flex: 1 1;
}

.key {
	font-size: 9px;
	white-space: nowrap;
}

.spacer {
	display: inline-block;
}

.command {
	margin-right: 8px;
	transition: all 0.1s ease-out;
}

.command:hover {
	color: #C00;
}

</style>
