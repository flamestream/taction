<template>
	<div>
		ViewAssets
		<div v-for="(asset, index) in assets" :key="index">
			{{ asset.name }}
		</div>
	</div>
</template>

<script>
export default {
	name: 'ViewAssets',
	props: {
		assets: {
			type: Object,
			default: () => {}
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
