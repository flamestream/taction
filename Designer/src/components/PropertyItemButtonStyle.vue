<template>
	<div>
		<PropertyItem v-for="(property, label) in properties" :label="label" :obj="property.obj" :type="property.type" :options="property.options" :parent="obj" :key="label"></PropertyItem>
	</div>
</template>

<script>
import PropertyItem from './PropertyItem'
import ButtonStyleType from '../types/ButtonStyleType'
export default {
	name: 'PropertyItemButtonStyle',
	components: {
		PropertyItem
	},
	props: {
		obj: { type: ButtonStyleType }
	},
	computed: {
		value() {

			let obj = this.obj || {};
			return obj.value;
		},
		properties() {

			let item = this.value || {};

			return {
				'Content': {
					obj: item.content,
					type: 'content'
				},
				'Background color': {
					obj: item.color,
					type: 'color'
				},
				'Text Style': {
					obj: item['text-style'],
					type: 'text-style'
				},
				'Margin': {
					obj: item.margin,
					type: 'rectangle'
				},
				'Padding': {
					obj: item.padding,
					type: 'rectangle'
				},
				'Border': {
					obj: item.border,
					type: 'border'
				},
				'Opacity': {
					obj: item.opacity,
					type: 'number',
					options: {
						min: 0,
						max: 1,
						step: 0.001
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
