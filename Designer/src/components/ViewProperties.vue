<template>
	<div class="subtle-scroll">
		<div v-if="!activeItem">
			<PropertyItem label="Global" type="global" :obj="layout"></PropertyItem>
			<PropertyItem label="Global Button Style" type="button-style-set" :obj="layout.value['default-button-style']"></PropertyItem>
		</div>
		<div v-else>
			<PropertyItem label="Button Properties" type="item" :obj="activeItem"></PropertyItem>
			<PropertyItem label="Style" type="button-style-set" :obj="activeItem.value.style"></PropertyItem>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex'
import PropertyItem from './PropertyItem'
export default {
	name: 'ViewProperties',
	components: {
		PropertyItem
	},
	computed: {
		...mapState(['layout', 'activeItem'])
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

div > .section {
	padding: 5px;
	margin-bottom: 16px;
	background-color: #FFFFFF22;
	border-radius: 5px;
	box-shadow: 0px 2px 6px 0px rgba(0, 0, 0, 0.3);
}

</style>
