<template>
	<div>
		ViewTree
		<Tree v-if="state && state.loadedLayout" :items="state.loadedLayout.items"></Tree>
	</div>
</template>

<script>
import Tree from './Tree'
export default {
	name: 'ViewTree',
	props: ['state'],
	components: {
		Tree
	},
	data() {
		return {
		}
	},
	methods: {
		onLayoutLoad(loadedLayout) {
			this.state.loadedLayout = loadedLayout;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
	background-color: #0000FF55;
}
</style>
