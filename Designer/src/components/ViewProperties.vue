<template>
	<div>
		ViewProperties
		<PropertiesGlobal :layout="layout"/>
		<PropertiesButton :caption="'Base'" :buttonProps="baseStyle"/>
		<PropertiesButton :caption="'Active'" :buttonProps="activeStyle"/>
	</div>
</template>

<script>
import PropertiesGlobal from './PropertiesGlobal'
import PropertiesButton from './PropertiesButton'
export default {
	name: 'ViewProperties',
	components: {
		PropertiesGlobal,
		PropertiesButton
	},
	props: {
		layout: {
			type: Object,
			default: () => {}
		}
	},
	computed: {
		baseStyle() {
			let { layout } = this;
			return layout && layout['default-button-style'] && layout['default-button-style'].base;
		},
		activeStyle() {
			let { layout } = this;
			return layout && layout['default-button-style'] && layout['default-button-style'].active;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
	background-color: #00FF0055;
	flex-basis: 200px;
}

div > table {
	display: block;
	background-color: #FF00FF55;
}
</style>
