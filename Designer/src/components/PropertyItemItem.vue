<template>
	<div>
		<PropertyItem v-for="(property, label) in properties"
			:label="label"
			:obj="property.obj"
			:type="property.type"
			:options="property.options"
			:parent="obj"
			:key="label">
		</PropertyItem>
	</div>
</template>

<script>
import PropertyItem from './PropertyItem'
import ItemType from '../types/ItemType'
export default {
	name: 'PropertyItemItem',
	components: {
		PropertyItem
	},
	props: {
		obj: { type: ItemType }
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
					type: 'type'
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
