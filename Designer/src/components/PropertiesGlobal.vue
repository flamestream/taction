<template>
	<table>
		<caption>▼ Global</caption>
		<PropertyRow v-for="(property, index) in properties" :label="property.label" :value="property.value" :type="property.type" :key="index"/>
	</table>
</template>

<script>
import PropertyRow from './PropertyRow'
export default {
	name: 'PropertyGlobal',
	components: {
		PropertyRow
	},
	props: {
		layout: {
			type: Object,
			default: () => {}
		}
	},
	computed: {
		properties() {
			let layout = this.layout || {};
			return [
				new PropertyRowSpecs('Name', layout['name']),
				new PropertyRowSpecs('Orientation', layout['orientation'], 'orientation'),
				new PropertyRowSpecs('Size', layout['size'], 'number-positive'),
				new PropertyRowSpecs('Background color', layout['color'], 'color'),
				new PropertyRowSpecs('Border', layout['border'], 'border'),
				new PropertyRowSpecs('Margin', layout['margin'], 'rectangle'),
				new PropertyRowSpecs('Opacity', layout['opacity'], 'number-100'),
				new PropertyRowSpecs('Disable hide', layout['disable-hide'], 'boolean'),
				new PropertyRowSpecs('Disable fade animation', layout['disable-fade-animation'], 'boolean'), // @TODO auto-set when 0
				new PropertyRowSpecs('Fade animation time', layout['fade-animation-time'], 'number-positive'),
				new PropertyRowSpecs('Opacity (hidden)', layout['opacity-hide'], 'number-100')
			]
		}
	}
}

class PropertyRowSpecs {
	constructor(label, value, type) {
		this.label = label;
		this.value = value;
		this.type = type;
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
table {
	background-color: #FF00FF55;
}
</style>
