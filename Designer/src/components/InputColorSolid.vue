<template>
	<div>
		<input type="text" :value="hexColor" @change="handleInputChange"/>
		<ColorPicker :value="color" @colorChange="handleColorChange"/>
	</div>
</template>

<script>
import SolidColorType from '../types/SolidColorType';
import Color from '../helpers/Color';
import ColorPicker from './ColorPicker';
export default {
	name: 'InputColorSolid',
	components: {
		ColorPicker
	},
	props: {
		obj: { type: SolidColorType }
	},
	data() {
		return {
			// Cached color
			color: new Color()
		}
	},
	computed: {
		value() {
			let obj = this.obj || {};
			return obj.value;
		},
		hexColor() {
			return this.value.getHex();
		}
	},
	watch: {
		obj(newVal, oldVal) {
			this.syncObj();
		}
	},
	methods: {
		syncObj() {
			this.color = this.value;
		},
		handleColorChange(color) {
			this.commitChange(color);
		},
		handleInputChange(ev) {
			let { value } = ev.target;
			var color = Color.fromHex(value, false) || Color.fromName(value);
			this.color = color;
			this.commitChange(color);
		},
		commitChange(color) {

			this.$store.commit({
				type: 'layout/setValue',
				obj: this.obj,
				value: color.getWpfHex()
			});
		}
	},
	mounted() {
		this.syncObj();
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
