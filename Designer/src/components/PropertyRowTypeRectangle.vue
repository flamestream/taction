<template>
	<div>
		<input :class="{error: hasError}" type="text" v-model="raw" @input="updateFromRaw">
		<table>
			<tr>
				<th>top</th>
				<td><input type="number" min="0" :value="top" @change="ev => { handleSingleInputChange(ev, 'top') }"/></td>
			</tr>
			<tr>
				<th>right</th>
				<td><input type="number" min="0" :value="right" @change="ev => { handleSingleInputChange(ev, 'right') }"/></td>
			</tr>
			<tr>
				<th>bottom</th>
				<td><input type="number" min="0" :value="bottom" @change="ev => { handleSingleInputChange(ev, 'bottom') }"/></td>
			</tr>
			<tr>
				<th>left</th>
				<td><input type="number" min="0" :value="left" @change="ev => { handleSingleInputChange(ev, 'left') }"/></td>
			</tr>
		</table>
	</div>
</template>

<script>
export default {
	name: 'PropertyRowTypeRectangle',
	props: ['value'],
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
	watch: {
		value(newVal, oldVal) {

			this.raw = newVal;
			this.updateFromRaw();
		}
	},
	methods: {
		updateFromRaw() {

			/* eslint no-mixed-operators: 0 */
			// Raw format check
			let { raw } = this;

			let top, right, bottom, left;
			if (raw) {

				this.hasError = !raw.match(/^\d+(\.\d+)?( \d+(\.\d+)?){0,3}$/);
				if (!this.hasError) {

					let individualValues = raw.split(' ');
					top = individualValues[0];
					right = individualValues.length >= 1 && individualValues[1] || top;
					bottom = individualValues.length >= 2 && individualValues[2] || top;
					left = individualValues.length >= 3 && individualValues[3] || right;
				}
			}

			this.top = top
			this.right = right
			this.bottom = bottom;
			this.left = left;
		},
		handleSingleInputChange(ev, name) {

			console.log('faousghkjasdf');
			let { currentTarget } = ev;
			let { value } = currentTarget;
			console.log(value);
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
		}
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
