<template>
	<div>
		ViewTree
		<Tree v-if="layout" :items="layout.value && layout.value.items"></Tree>
	</div>
</template>

<script>
import { mapState } from 'vuex'
import Tree from './Tree'
export default {
	name: 'ViewTree',
	components: {
		Tree
	},
	computed: {
		...mapState(['layout'])
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
	background-color: #0000FF55;
}
</style>
