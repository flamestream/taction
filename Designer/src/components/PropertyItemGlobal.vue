<template>
	<div>
		<PropertyItem v-for="(property, label) in properties" :label="label" :obj="property.obj" :type="property.type" :options="property.options" :parent="obj" :key="label"></PropertyItem>
	</div>
</template>

<script>
import PropertyItem from './PropertyItem'
import LayoutType from '../types/LayoutType'
export default {
	name: 'PropertyItemGlobal',
	components: {
		PropertyItem
	},
	props: {
		obj: { type: LayoutType }
	},
	computed: {
		value() {

			let obj = this.obj || {};
			return obj.value;
		},
		properties() {

			let item = this.value || {};

			return {
				'Name': {
					obj: item.name
				},
				'Orientation': {
					obj: item.orientation,
					type: 'option',
					options: {
						options: {
							Vertical: 'vertical',
							Horizontal: 'horizontal'
						}
					}
				},
				'Size': {
					obj: item.size,
					type: 'number',
					options: {
						min: 0
					}
				},
				'Background color': {
					obj: item.color,
					type: 'color'
				},
				'Border': {
					obj: item.border,
					type: 'border'
				},
				'Margin': {
					obj: item.margin,
					type: 'rectangle'
				},
				'Opacity': {
					obj: item.opacity,
					type: 'number',
					options: {
						min: 0,
						max: 1,
						step: 0.001
					}
				},
				'Hide on mouse-over': {
					obj: item['disable-hide'],
					type: 'checkbox',
					options: {
						invert: true
					}
				},
				'Fade out animation on hide': {
					obj: item['disable-fade-animation'],
					type: 'checkbox',
					options: {
						invert: true
					}
				},
				'Fade animation time': {
					obj: item['fade-animation-time'],
					type: 'number',
					options: {
						min: 0
					}
				},
				'Opacity (hidden)': {
					obj: item['opacity-hide'],
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
