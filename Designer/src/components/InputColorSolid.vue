<template>
	<div>
		<input type="text" :value="hexColor" @change="handleInputChange"/>
		<ColorPicker :value="color" @colorChange="handleColorChange"/>
	</div>
</template>

<script>
import Color from '../helpers/Color';
import ColorPicker from './ColorPicker';
export default {
	name: 'InputColorSolid',
	components: {
		ColorPicker
	},
	props: {
		value: {
			type: String,
			default: () => ''
		}
	},
	data() {
		return {
			color: new Color()
		}
	},
	computed: {
		hexColor() {
			return this.color.getWpfHex();
		}
	},
	watch: {
		value(newVal, oldVal) {
			this.syncColor();
		}
	},
	methods: {
		handleColorChange(color) {
			this.color = new Color(color);
			this.$emit('change', this.color);
		},
		handleInputChange(ev) {
			let { value } = ev.target;
			this.color = Color.fromWpfHex(value, false) || Color.fromName(value);
		},
		setColorFromHex(hex) {

			if (hex.length === 3 || hex.length === 4)
				hex = hex.replace(/./g, '$&$&');

			this.red = parseInt(hex[0] + hex[1], 16);
			this.green = parseInt(hex[2] + hex[3], 16);
			this.blue = parseInt(hex[4] + hex[5], 16);
		},
		syncColor() {
			let { value } = this;
			this.color = Color.fromWpfHex(value, false) || Color.fromName(value);
		}
	},
	mounted() {
		this.syncColor();
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
