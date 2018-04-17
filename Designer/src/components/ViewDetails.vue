<template>
	<div class="section-properties-content">
		<ViewAssets v-if="activeMenu === 'assets'"></ViewAssets>
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
	overflow: hidden;
}

.section-properties-content:hover {
	overflow-y: auto;
}

.section-properties-content::-webkit-scrollbar
{
	width: 5px;
	background-color: #FFFFFF33;
}

.section-properties-content::-webkit-scrollbar-thumb
{
	background-color: #FFFFFFAA;
	border-radius: 10px;
}
</style>
