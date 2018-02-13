<template>
	<div>
		<PropertyItem v-for="(property, label) in properties" :label="label" :obj="property.obj" :type="property.type" :options="property.options" :parent="obj" :key="label"></PropertyItem>
	</div>
</template>

<script>
import PropertyItem from './PropertyItem'
import ButtonStyleSetType from '../types/ButtonStyleSetType'
export default {
	name: 'PropertyItemButtonStyleSet',
	components: {
		PropertyItem
	},
	props: {
		obj: { type: ButtonStyleSetType }
	},
	computed: {
		value() {

			let obj = this.obj || {};
			return obj.value;
		},
		properties() {

			let item = this.value || {};

			return {
				'Base': {
					obj: item.base,
					type: 'button-style'
				},
				'Active': {
					obj: item.active,
					type: 'button-style'
				}
			};
		}
	}
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
