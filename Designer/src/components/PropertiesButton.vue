<template>
	<table>
		<caption>{{ caption }}</caption>
		<PropertiesRowText :label="'Content'" :value="buttonProps['content']"/>
		<PropertiesRowText :label="'Text style'" :value="buttonProps['text-style']"/>
		<PropertiesRowText :label="'Color'" :value="buttonProps['color']"/>
		<PropertiesRowText :label="'Margin'" :value="buttonProps['margin']"/>
		<PropertiesRowText :label="'Padding'" :value="buttonProps['padding']"/>
		<PropertiesRowText :label="'Border'" :value="buttonProps['border']"/>
		<PropertiesRowText :label="'Opacity'" :value="buttonProps['opacity']"/>
	</table>
</template>

<script>
import PropertiesRowText from './PropertiesRowText'
export default {
	name: 'PropertyButton',
	components: {
		PropertiesRowText
	},
	props: {
		buttonProps: {
			type: Object,
			default: {}
		},
		caption: {
			type: String,
			default: 'Button style'
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
table {
	background-color: #FF00FF55;
}
</style>
