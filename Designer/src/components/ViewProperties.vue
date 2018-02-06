<template>
	<div class="subtle-scroll">
		<div v-if="activeMenu === 'assets'">
			<input type="file" label="Add asset"/>
			<div v-for="filename in assets" :key="filename">
				{{ filename }}
			</div>
		</div>
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
export default {
	name: 'ViewProperties',
	components: {
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
