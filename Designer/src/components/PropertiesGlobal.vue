<template>
	<div>
		<div class="label">▼ Global</div>
		<PropertyRow v-for="(property, label) in properties" :label="label" :obj="property.obj" :type="property.type" :options="property.options" :key="label"/>
	</div>
</template>

<script>
import PropertyRow from './PropertyRow'
export default {
	name: 'PropertyGlobal',
	components: {
		PropertyRow
	},
	computed: {
		layout() {

			let { layout } = this.$store.state;
			return layout && layout.value;
		},
		properties() {

			let layout = this.layout || {};

			return {
				'Name': {
					obj: layout.name
				},
				'Orientation': {
					obj: layout.orientation,
					type: 'option',
					options: {
						options: ['vertical', 'horizontal']
					}
				},
				'Size': {
					obj: layout.size,
					type: 'number',
					options: {
						min: 0
					}
				},
				'Background color': {
					obj: layout.color,
					type: 'color'
				},
				'Border': {
					obj: layout.border,
					type: 'border'
				},
				'Margin': {
					obj: layout.margin,
					type: 'rectangle'
				},
				'Opacity': {
					obj: layout.opacity,
					type: 'range',
					options: {
						min: 0,
						max: 1,
						step: 0.001
					}
				},
				'Disable hide': {
					obj: layout['disable-hide'],
					type: 'checkbox'
				},
				'Disable fade animation': {
					obj: layout['disable-fade-animation'],
					type: 'checkbox'
				},
				'Fade animation time': {
					obj: layout['fade-animation-time'],
					type: 'number',
					options: {
						min: 0
					}
				},
				'Opacity (hidden)': {
					obj: layout['opacity-hide'],
					type: 'range',
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
div {
	background-color: #FF00FF55;
}
</style>
