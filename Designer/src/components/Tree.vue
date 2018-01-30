<template>
	<div>
		<TreeNode v-for="(item, index) in items" :itemData="item" :key="index" />
	</div>
</template>

<script>
import TreeNode from './TreeNode'
export default {
	name: 'Tree',
	props: [ 'items' ],
	components: {
		TreeNode
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
	background-color: #FF000011;
}
</style>
