<template>
	<div data-simplebar>
		<div v-if="layout" @click.stop="handleClick">
			<div :class="{ 'menu-item': true, root: true, active: !activeMenu && !activeItem }">
				<div class="label">
					<span class="text">Root</span>
					<span class="command" @click.stop="handleAdderClick">＋</span>
				</div>
				<MenuItemLayoutItem v-for="item in value.items" :obj="item" :key="item.id" :parent="layout" :depth="1"></MenuItemLayoutItem>
			</div>
		</div>
	</div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import MenuItemLayoutItem from './MenuItemLayoutItem'
export default {
	name: 'ViewTree',
	components: {
		MenuItemLayoutItem
	},
	computed: {
		...mapState('layout', ['layout']),
		...mapState('ui', ['activeMenu', 'activeItem']),
		value() {
			let layout = this.layout || {};
			return layout.value;
		}
	},
	methods: {
		...mapActions({
			setActiveMenu: 'ui/setActiveMenu',
			setActiveItem: 'ui/setActiveItem',
			addItem: 'layout/addItem'
		}),
		handleClick(ev) {

			let { target } = ev;
			target = target.closest('.menu-item');

			let id = (!target)
				? undefined
				: target.dataset.id;

			this.setActiveMenu();
			this.setActiveItem({id});
		},
		handleAdderClick(ev) {

			this.addItem({
				parent: this.layout,
				value: {
					type: 'hold',
					size: 50,
					style: {
						base: {
							content: {
								type: 'text',
								value: 'New Button'
							}
						}
					}
				},
				active: true
			});
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

.command {
	margin-right: 8px;
}

</style>
