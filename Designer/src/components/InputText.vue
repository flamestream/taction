<template>
	<div>
		<span>{{ label }}</span>
		<input type="text" v-model="value" />
	</div>
</template>

<script>
export default {
	name: 'InputText',
	props: {
		obj: { type: Object },
		options: { type: Object }
	},
	computed: {
		value: {
			get() {

				let obj = this.obj || {};
				return obj.value;
			},
			set(value) {

				this.$store.commit({
					type: 'layout/setValue',
					obj: this.obj,
					value
				});
			}
		},
		label() {

			let options = this.options || {};
			return options.label;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

div {
	display: flex;
}

input {
	flex: 1 1 auto;
}

</style>
