<template>
	<div>
		<span v-if="label">{{ label }}</span>
		<input type="number" v-model.lazy="value" @change="handleNumberChange"/>
		<input ref="range" type="range" v-model="value" @change="handleRangeChange" :step="step" />
	</div>
</template>

<script>
import NumberType from '../types/NumberType';
import IntegerType from '../types/IntegerType';
export default {
	name: 'InputNumber',
	props: {
		obj: { type: [NumberType, IntegerType] },
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
		min() {

			let options = this.options || {};
			return options.min;
		},
		max() {

			let options = this.options || {};
			return options.max;
		},
		step() {

			let options = this.options || {};
			return options.step || 1;
		}
	},
	methods: {
		handleRangeChange(ev) {

			let {min, max} = this;
			if (min === undefined) min = this.value - this.step * 100;
			if (max === undefined) max = this.value + this.step * 100;
			this.changeRangeBounds(min, max);
		},
		handleNumberChange(ev) {

			let {currentTarget} = ev;
			let {value} = currentTarget;
			let {min, max} = this;
			if (min !== undefined) value = Math.max(min, value);
			if (max !== undefined) value = Math.min(max, value);

			currentTarget.value = value;
		},
		changeRangeBounds(min, max) {

			this.$refs.range.setAttribute('min', min);
			this.$refs.range.setAttribute('max', max);
		}
	},
	mounted() {

		this.changeRangeBounds(this.min, this.max);
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

div {
	display: flex;
}

input[type=number] {
	width: 40px;
}

input[type=range] {
	flex: 1 1 auto;
	margin-left: 8px;
}

</style>
