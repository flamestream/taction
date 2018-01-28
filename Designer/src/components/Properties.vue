<template>
	<div>
		Properties
		<PropertiesGlobal :loadedLayout="loadedLayout"/>
		<PropertiesButton :caption="'Button base style'" :buttonProps="loadedLayout['default-button-style'] && loadedLayout['default-button-style'].base"/>
		<PropertiesButton :caption="'Button active style'" :buttonProps="loadedLayout['default-button-style'] && loadedLayout['default-button-style'].active"/>
	</div>
</template>

<script>
import PropertiesGlobal from './PropertiesGlobal'
import PropertiesButton from './PropertiesButton'
export default {
	name: 'Properties',
	components: {
		PropertiesGlobal,
		PropertiesButton
	},
	props: {
		loadedLayout: {
			type: Object,
			default: {}
		},
	},
	data () {
		return {
			msg: 'Welcome to Your Vue.js App'
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
</style>
