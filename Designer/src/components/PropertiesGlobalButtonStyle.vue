<template>
	<div class="section">
		<div class="label">▼ Global Button Style</div>
		<div class="content">
			<PropertyButtonStyleSet :obj="layout['default-button-style']"></PropertyButtonStyleSet>
		</div>
	</div>
</template>

<script>
import PropertyButtonStyleSet from './PropertyButtonStyleSet'
import PropertyItem from './PropertyButtonStyleSet'
export default {
	name: 'PropertiesGlobalButtonStyle',
	components: {
		PropertyButtonStyleSet,
		PropertyItem
	},
	computed: {
		layout() {

			let { layout } = this.$store.state;
			return layout && layout.value;
		}
	}
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
