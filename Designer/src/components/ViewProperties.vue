<template>
	<div class="section-properties-content">
		<ViewAssets v-if="activeMenu === 'assets'"></ViewAssets>
		<ViewPropertiesActive v-else-if="activeItem" class="view-properties" :activeItem="activeItem"></ViewPropertiesActive>
		<div v-else class="view-properties">
			<div class="add-button">Add Component</div>
			<PropertyItem label="Global" type="global" :obj="layout"></PropertyItem>
			<PropertyItem label="Global Button Style" type="button-style-set" :obj="layout.value['default-button-style']"></PropertyItem>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex'
import PropertyItem from './PropertyItem'
import ViewAssets from './ViewAssets'
import ViewPropertiesActive from './ViewPropertiesActive'
export default {
	name: 'ViewProperties',
	components: {
		ViewAssets,
		ViewPropertiesActive,
		PropertyItem
	},
	computed: {
		...mapState('layout', ['layout']),
		...mapState('ui', ['activeMenu', 'activeItem'])
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.section-properties-content {
	width: 100%;
}
</style>
