<template>
	<table>
		<caption>▶ {{ caption }}</caption>
		<PropertyRow v-for="(property, index) in properties" :label="property.label" :value="property.value" :type="property.type" :key="index"/>
	</table>
</template>

<script>
import PropertyRow from './PropertyRow'
export default {
	name: 'PropertyButton',
	components: {
		PropertyRow
	},
	props: {
		buttonProps: {
			type: Object,
			default: () => {}
		},
		caption: {
			type: String,
			default: 'Button style'
		}
	},
	computed: {
		properties() {
			let p = this.buttonProps || {};
			return [
				// new PropertyRowSpecs('Type', p['type']),
				new PropertyRowSpecs('Content', p['content'], 'content'),
				new PropertyRowSpecs('Color', p['color'], 'color'),
				new PropertyRowSpecs('Text style', p['text-style'], 'text-style'),
				new PropertyRowSpecs('Margin', p['margin'], 'rectangle'),
				new PropertyRowSpecs('Padding', p['padding'], 'rectangle'),
				new PropertyRowSpecs('Border', p['border'], 'border'),
				new PropertyRowSpecs('Opacity', p['opacity'], 'number-100')
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
	display: block;
	background-color: #FF00FF55;
}

</style>
