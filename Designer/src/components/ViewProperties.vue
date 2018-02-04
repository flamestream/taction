<template>
	<div>
		ViewProperties
		<PropertiesGlobal />
	</div>
</template>

<script>
import PropertiesGlobal from './PropertiesGlobal'
export default {
	name: 'ViewProperties',
	components: {
		PropertiesGlobal
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
