<template>
	<div>
		<input :class="{error: hasError}" type="text" v-model="raw" @input="updateFromRaw">
		<table>
			<tr>
				<th>top</th>
				<td><input type="number" v-model="top" min="0" @change="updateFromIndividualValues"/></td>
			</tr>
			<tr>
				<th>right</th>
				<td><input type="number" v-model="right" min="0" @change="updateFromIndividualValues"/></td>
			</tr>
			<tr>
				<th>bottom</th>
				<td><input type="number" v-model="bottom" min="0" @change="updateFromIndividualValues"/></td>
			</tr>
			<tr>
				<th>left</th>
				<td><input type="number" v-model="left" min="0" @change="updateFromIndividualValues"/></td>
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

			if (typeof raw !== 'string')
				return;

			this.hasError = !raw.match(/^\d+(\.\d+)?( \d+(\.\d+)?){0,3}$/);
			if (this.hasError)
				return;

			let individualValues = raw.split(' ');
			this.top = individualValues[0];
			this.right = individualValues.length >= 1 && individualValues[1] || this.top;
			this.bottom = individualValues.length >= 2 && individualValues[2] || this.top;
			this.left = individualValues.length >= 3 && individualValues[3] || this.right;
		},
		updateFromIndividualValues() {

			if (this.top === undefined) this.top = 0;
			if (this.right === undefined) this.right = 0;
			if (this.bottom === undefined) this.bottom = 0;
			if (this.left === undefined) this.left = 0;

			this.raw = `${this.top} ${this.right} ${this.bottom} ${this.left}`;
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
