<template>
	<div>
		<span>{{ label }}</span>
		<input type="text" v-model="value" />
	</div>
</template>

<script>
import StringType from '../layout/StringType';
export default {
	name: 'InputText',
	props: {
		obj: { type: StringType },
		label: { type: String }
	},
	computed: {
		value: {
			get() {

				let obj = this.obj || {};
				return obj.value;
			},
			set(value) {

				this.$store.commit({
					type: 'setValue',
					obj: this.obj,
					value
				});
			}
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
