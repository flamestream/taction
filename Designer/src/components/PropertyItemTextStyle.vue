<template>
	<div>
		<PropertyItem v-for="(property, label) in properties" :label="label" :obj="property.obj" :type="property.type" :options="property.options" :parent="obj" :key="label"></PropertyItem>
	</div>
</template>

<script>
import PropertyItem from './PropertyItem'
import TextStyleType from '../types/TextStyleType'
export default {
	name: 'PropertyItemTextStyle',
	components: {
		PropertyItem
	},
	props: {
		obj: { type: TextStyleType }
	},
	computed: {
		value() {

			let obj = this.obj || {};
			return obj.value;
		},
		properties() {

			let item = this.value || {};

			return {
				'Size': {
					obj: item['font-size'],
					type: 'number'
				},
				'Color': {
					obj: item.color,
					type: 'color'
				},
				'Font Family': {
					obj: item['font-family']
				},
				'Weight': {
					obj: item['font-weight'],
					type: 'option',
					options: {
						options: item['font-weight'].options
					}
				}
			};
		}
	}
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
