<template>
	<div class="subtle-scroll">
		<div v-if="layout" @click.stop="handleClick">
			<div :class="{ 'tree-node': true, active: !activeItem }">
				<div class="label">
					<span class="text">Root</span>
					<span class="command" @click.stop="handleAdderClick">＋</span>
				</div>
				<TreeNode v-for="item in value.items" :obj="item" :key="item.id" :parent="layout" :depth="1"></TreeNode>
			</div>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex'
import TreeNode from './TreeNode'
export default {
	name: 'ViewTree',
	components: {
		TreeNode
	},
	computed: {
		...mapState(['layout', 'activeItem']),
		value() {
			let layout = this.layout || {};
			return layout.value;
		}
	},
	methods: {
		handleClick(ev) {
			let { target } = ev;
			target = target.closest('.tree-node');

			let id = (!target)
				? undefined
				: target.dataset.id;

			this.$store.dispatch({
				type: 'setActiveItem',
				id
			});
		},
		handleAdderClick(ev) {

			this.$store.dispatch({
				type: 'addItem',
				parent: this.layout,
				value: {
					type: 'hold'
				}
			});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.label {
	display: flex;
}

.text {
	flex: 1 1;
}

.command {
	margin-right: 8px;
}

</style>
