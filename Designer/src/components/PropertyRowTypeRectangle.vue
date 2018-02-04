<template>
	<div>
		<input :class="{error: hasError}" type="text" v-model="raw" @input="updateFromRaw()">
		<table>
			<tr>
				<th>top</th>
				<td><input type="number" min="0" :value="top" @input="ev => { handleSingleInputChange(ev, 'top') }"/></td>
			</tr>
			<tr>
				<th>right</th>
				<td><input type="number" min="0" :value="right" @input="ev => { handleSingleInputChange(ev, 'right') }"/></td>
			</tr>
			<tr>
				<th>bottom</th>
				<td><input type="number" min="0" :value="bottom" @input="ev => { handleSingleInputChange(ev, 'bottom') }"/></td>
			</tr>
			<tr>
				<th>left</th>
				<td><input type="number" min="0" :value="left" @input="ev => { handleSingleInputChange(ev, 'left') }"/></td>
			</tr>
		</table>
	</div>
</template>

<script>
import RectangleType from '../layout/RectangleType';
export default {
	name: 'PropertyRowTypeRectangle',
	props: {
		obj: { type: RectangleType }
	},
	data() {
		return {
			hasError: false,
			raw: undefined,
			top: undefined,
			right: undefined,
			bottom: undefined,
			left: undefined
		}
	},
	computed: {
		value() {
			let obj = this.obj || {};
			return obj.value;
		}
	},
	watch: {
		obj(newVal, oldVal) {
			this.syncObj();
		}
	},
	methods: {
		syncObj() {
			this.raw = this.value;
			this.updateFromRaw(true);
		},
		commitChange() {

			this.$store.commit({
				type: 'setValue',
				obj: this.obj,
				value: this.raw
			});
		},
		updateFromRaw(ignoreSignal) {
			/* eslint no-mixed-operators: 0 */
			// Raw format check
			let { raw } = this;
			let top, right, bottom, left, hasError;
			if (raw) {

				hasError = !raw.match(/^\d+(\.\d+)?( \d+(\.\d+)?){0,3}$/);
				if (!hasError) {

					let individualValues = raw.split(' ');
					top = individualValues[0];
					right = individualValues.length >= 1 && individualValues[1] || top;
					bottom = individualValues.length >= 2 && individualValues[2] || top;
					left = individualValues.length >= 3 && individualValues[3] || right;
				}

			} else {

				hasError = false;
			}

			if (!hasError && !ignoreSignal)
				this.commitChange();

			this.hasError = hasError;
			this.top = top
			this.right = right
			this.bottom = bottom;
			this.left = left;
		},
		handleSingleInputChange(ev, name) {

			let { currentTarget } = ev;
			let { value } = currentTarget;
			let validatedValue = Number.parseFloat(value) || 0;
			this[name] = validatedValue;

			this.updateFromIndividualValues();
		},
		updateFromIndividualValues() {

			let { top, right, bottom, left } = this;
			if (!top) top = 0;
			if (!right) right = 0;
			if (!bottom) bottom = 0;
			if (!left) left = 0;

			this.raw = `${top} ${right} ${bottom} ${left}`;
			this.commitChange();
		}
	},
	mounted() {

		this.syncObj();
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

th {
	text-align: left;
}

input[type=number] {
	width: 4em;
}

input.error {
	background-color: #FAA;
}

</style>
