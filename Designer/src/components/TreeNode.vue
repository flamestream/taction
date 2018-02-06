<template>
	<div v-if="value" :class="{ 'tree-node': true, active }" :data-id="obj.id">
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
			<TreeNode v-for="item in value.items" :obj="item" :key="item.id" :parent="obj" :depth="depth + 1"></TreeNode>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex'
import TreeNode from './TreeNode'
import ItemType from '../layout/ItemType';
export default {
	name: 'TreeNode',
	props: {
		obj: { type: ItemType },
		parent: { type: Object },
		depth: { type: Number, default: 0 }
	},
	components: {
		TreeNode
	},
	computed: {
		...mapState(['activeItem']),
		active() {

			return this.activeItem === this.obj;
		},
		value() {

			let obj = this.obj || {};
			return obj.value;
		}
	},
	methods: {
		handleRemoverClick(ev) {

			this.$store.dispatch({
				type: 'removeItem',
				item: this.obj,
				parent: this.parent
			});
		},
		handleAdderClick(ev) {

			this.$store.dispatch({
				type: 'addItem',
				parent: this.obj,
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

.key {
	font-size: 9px;
	white-space: nowrap;
}

.spacer {
	display: inline-block;
}

.command {
	margin-right: 8px;
}

</style>
