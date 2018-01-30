<template>
	<div>
		ViewTree
		<Tree v-if="layout" :items="layout.items"></Tree>
	</div>
</template>

<script>
import Tree from './Tree'
export default {
	name: 'ViewTree',
	props: ['layout'],
	components: {
		Tree
	},
	data() {
		return {
		}
	},
	methods: {
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
	background-color: #0000FF55;
}
</style>
