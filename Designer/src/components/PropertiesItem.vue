<template>
	<div class="section">
		<div class="label">▼ Button Properties</div>
		<div v-if="defined">
			<PropertyItem v-for="(property, label) in properties" :label="label" :obj="property.obj" :type="property.type" :options="property.options" :key="label"></PropertyItem>
		</div>
	</div>
</template>

<script>
import PropertyItem from './PropertyItem'
// import PropertyButtonStyleSet from './PropertyButtonStyleSet'
import ItemType from '../layout/ItemType'
export default {
	name: 'PropertiesItem',
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
		defined: {
			get() {
				let { obj } = this;
				return !obj.notDefined;
			},
			set(value) {
				value = !value;
				this.$store.commit({
					type: 'setDefined',
					obj: this.obj,
					value
				});
			}
		},
		required() {
			let { obj } = this;
			return !obj.required;
		},
		properties() {

			let item = this.value || {};

			return {
				'Type': {
					obj: item.type,
					type: 'option',
					options: {
						options: ['tap', 'hold', 'move', 'pivot']
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
