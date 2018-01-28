<template>
	<div>
		<div>{{ itemData.type }}</div>
		<div v-if="itemData.items">
			<TreeNode v-for="item in itemData.items" :itemData="item"/>
		</div>
	</div>
</template>

<script>
import TreeNode from './TreeNode'
export default {
	name: 'TreeNode',
	props: [ 'itemData' ],
	components: {
		TreeNode
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
	background-color: #FFFF0055;
}
</style>
