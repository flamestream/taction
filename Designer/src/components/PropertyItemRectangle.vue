<template>
	<div class="container">
		<div class="raw">
			<span>Value</span><input :class="{error: hasError}" type="text" v-model="raw" @input="updateFromRaw()"/>
		</div>
		<div class="units">
			<div class="unit">
				<span :style="getArrowStyle(0)">▲</span>
				<input type="number" min="0" :value="top" @input="ev => { handleSingleInputChange(ev, 'top') }"/>
			</div>
			<div class="unit">
				<span :style="getArrowStyle(1)">▲</span>
				<input type="number" min="0" :value="right" @input="ev => { handleSingleInputChange(ev, 'right') }"/>
			</div>
			<div class="unit">
				<span :style="getArrowStyle(2)">▲</span>
				<input type="number" min="0" :value="bottom" @input="ev => { handleSingleInputChange(ev, 'bottom') }"/>
			</div>
			<div class="unit">
				<span :style="getArrowStyle(3)">▲</span>
				<input type="number" min="0" :value="left" @input="ev => { handleSingleInputChange(ev, 'left') }"/>
			</div>
		</div>
	</div>
</template>

<script>
import RectangleType from '../types/RectangleType';
export default {
	name: 'PropertyItemRectangle',
	props: {
		obj: { type: RectangleType },
		options: { type: Object }
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
				type: 'layout/setValue',
				obj: this.obj,
				value: this.raw
			});
		},
		getArrowStyle(position) {

			let options = this.options || {};
			let value = options.corner ? -45 : 0;
			value += 90 * position;

			return {
				transform: `rotate(${value}deg)`
			}
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

.container {
/*	margin: 5px;
	background-color: #00000055;
	padding: 10px;
	border-radius: 5px*/
}

.raw {
	display: flex;
	justify-content: space-between;
	line-height: 20px;
}

.raw > span {
	display: inline-block;
}

.units {
	margin-top: 5px;
	display: flex;
	justify-content: space-between;
}

.unit {
	display: flex;
	justify-content: space-between;
	flex: 1 1 auto;
	white-space: nowrap;
	line-height: 20px;
	margin: 0 3px;
}
.unit:first-child {
	margin-left: 0;
}
.unit:last-child {
	margin-right: 0;
}

.unit > input[type=number] {
	width: 40px;
}

</style>
