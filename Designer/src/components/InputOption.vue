<template>
	<div>
		<span v-if="label">{{ label }}</span>
		<select v-model="value">
			<option v-for="(value, label) in formattedOptions" :value="value" :key="value">{{ label }}</option>
		</select>
	</div>
</template>

<script>
export default {
	name: 'InputOption',
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
		},
		formattedOptions() {

			let options = this.options || {};
			let choices = options.options || {};

			let out = {};
			if (Array.isArray(choices)) {
				for (let c of choices) {
					out[c] = c;
				}
			} else {
				out = choices;
			}

			return out;
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
