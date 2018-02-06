<template>
	<div>
		<PropertyItem v-for="(property, label) in properties" :label="label" :obj="property.obj" :type="property.type" :options="property.options" :key="label"></PropertyItem>
	</div>
</template>

<script>
import PropertyItem from './PropertyItem'
import LayoutType from '../layout/LayoutType'
export default {
	name: 'PropertyItemGlobal',
	components: {
		PropertyItem
	},
	props: {
		obj: { type: LayoutType }
	},
	computed: {
		value() {

			let obj = this.obj || {};
			return obj.value;
		},
		properties() {

			let item = this.value || {};

			return {
				'Type': {
					obj: item.type,
					type: 'option',
					options: {
						options: item.type.options
					}
				},
				'Size': {
					obj: item.size,
					type: 'number',
					options: {
						min: 0
					}
				},
				'Command': {
					obj: item.command
				}
			};
		}
	}
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
