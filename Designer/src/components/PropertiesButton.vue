<template>
	<table>
		<caption>▶ {{ caption }}</caption>
		<PropertyItem v-for="(property, index) in properties" :label="property.label" :value="property.value" :type="property.type" :key="index"/>
	</table>
</template>

<script>
import PropertyItem from './PropertyItem'
export default {
	name: 'PropertyButton',
	components: {
		PropertyItem
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
				// new PropertyItemSpecs('Type', p['type']),
				new PropertyItemSpecs('Content', p['content'], 'content'),
				new PropertyItemSpecs('Color', p['color'], 'color'),
				new PropertyItemSpecs('Text style', p['text-style'], 'text-style'),
				new PropertyItemSpecs('Margin', p['margin'], 'rectangle'),
				new PropertyItemSpecs('Padding', p['padding'], 'rectangle'),
				new PropertyItemSpecs('Border', p['border'], 'border'),
				new PropertyItemSpecs('Opacity', p['opacity'], 'number-100')
			]
		}
	}
}

class PropertyItemSpecs {
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
