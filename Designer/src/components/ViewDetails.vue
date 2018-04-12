<template>
	<div class="section-properties-content">
		<ViewAssets v-if="activeMenu === 'assets'"></ViewAssets>
		<!-- <ViewPropertiesActive v-else-if="activeItem" :activeItem="activeItem"></ViewPropertiesActive> -->
		<ViewProperties v-else></ViewProperties>
	</div>
</template>

<script>
import { mapState } from 'vuex'
import ViewAssets from './ViewAssets'
import ViewProperties from './ViewProperties'
export default {
	name: 'ViewDetails',
	components: {
		ViewAssets,
		ViewProperties
	},
	computed: {
		...mapState('ui', ['activeMenu'])
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.section-properties-content {
	flex: 1 1 auto;
	display: flex;
}
</style>
