<template>
	<table>
		<caption>Global</caption>
		<PropertiesRowText :label="'Opacity'" :value="loadedLayout['opacity']"/>
		<PropertiesRowText :label="'Opacity (hidden)'" :value="loadedLayout['opacity-hide']"/>
		<PropertiesRowText :label="'Fade animation time'" :value="loadedLayout['fade-animation-time']"/>
		<PropertiesRowText :label="'Size'" :value="loadedLayout['size']"/>
		<PropertiesRowText :label="'Name'" :value="loadedLayout['name']"/>
		<PropertiesRowText :label="'Disable fade animation'" :value="loadedLayout['disable-fade-animation']"/>
		<PropertiesRowText :label="'Disable hide'" :value="loadedLayout['disable-hide']"/>
		<PropertiesRowText :label="'Margin'" :value="loadedLayout['margin']"/>
		<PropertiesRowText :label="'Border'" :value="loadedLayout['border']"/>
		<PropertiesRowText :label="'Background color'" :value="loadedLayout['color']"/>
		<PropertiesRowText :label="'Orientation'" :value="loadedLayout['orientation']"/>
	</table>
</template>

<script>
import PropertiesRowText from './PropertiesRowText'
export default {
	name: 'PropertyGlobal',
	components: {
		PropertiesRowText
	},
	props: [ 'loadedLayout' ]
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
table {
	background-color: #FF00FF55;
}
</style>
