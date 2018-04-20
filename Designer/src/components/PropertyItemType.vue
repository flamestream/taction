<template>
	<select v-model="type">
		<option v-for="t in types" :key="t">{{ t }}</option>
	</select>
</template>

<script>
import PropertyItem from './PropertyItem'
import StringType from '../types/StringType'
export default {
	name: 'PropertyItemType',
	components: {
		PropertyItem
	},
	props: {
		obj: { type: StringType }
	},
	computed: {
		value() {

			let obj = this.obj || {};
			return obj.value;
		},
		types() {

			return this.obj.options;
		},
		type: {
			get() {

				return this.value;
			},
			set(value) {
				this.$store.commit({
					type: 'layout/changeType',
					obj: this.obj.parent,
					value
				});
				this.$emit('change', { value });
			}
		}
	}
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
