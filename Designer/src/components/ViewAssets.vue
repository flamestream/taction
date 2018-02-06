<template>
	<div>
		ViewAssets
		<div v-for="filename in assets" :key="filename">
			{{ filename }}
		</div>
	</div>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
	name: 'ViewAssets',
	computed: {
		...mapGetters(['assets'])
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
	/*background-color: #0000FF55;*/
}
</style>
