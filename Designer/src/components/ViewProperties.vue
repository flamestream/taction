<template>
	<div class="subtle-scroll">
		<ViewAssets v-if="activeMenu === 'assets'"></ViewAssets>
		<div v-else-if="activeItem">
			<PropertyItem label="Button Properties" type="item" :obj="activeItem"></PropertyItem>
			<PropertyItem label="Style" type="button-style-set" :obj="activeItem.value.style"></PropertyItem>
		</div>
		<div v-else>
			<PropertyItem label="Global" type="global" :obj="layout"></PropertyItem>
			<PropertyItem label="Global Button Style" type="button-style-set" :obj="layout.value['default-button-style']"></PropertyItem>
		</div>
	</div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import PropertyItem from './PropertyItem'
import ViewAssets from './ViewAssets'
export default {
	name: 'ViewProperties',
	components: {
		ViewAssets,
		PropertyItem
	},
	computed: {
		...mapState(['layout', 'activeItem', 'activeMenu']),
		...mapGetters(['assets'])
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
