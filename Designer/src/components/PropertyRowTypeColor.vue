<template>
	<div>
		<select v-model="type">
			<option>solid</option>
			<option>gradient</option>
			<option>image</option>
		</select>
		<input type="text" :value="hexColor" @change="handleInputChange"/>
		<ColorPicker :value="color" @colorChange="handleColorChange"/>
	</div>
</template>

<script>
import Color from '../helpers/Color';
import ColorPicker from './ColorPicker';
export default {
	name: 'PropertyRowTypeColor',
	components: {
		ColorPicker
	},
	props: {
		value: {
			type: Object,
			default: () => {}
		}
	},
	data() {
		return {
			type: undefined,
			color: new Color()
		}
	},
	computed: {
		hexColor() {
			let color = this.color || new Color();
			return color.getWpfHex();
		}
	},
	watch: {
		value(newVal, oldVal) {
			this.type = newVal && newVal.type;
			this.color = newVal && newVal.value;
		}
	},
	methods: {
		handleColorChange(color) {
			this.color = new Color(color);
		},
		handleInputChange(ev) {
			let { target } = ev;
			this.color = Color.fromWpfHex(target.value);
		},
		setColorFromHex(hex) {

			if (hex.length === 3 || hex.length === 4)
				hex = hex.replace(/./g, '$&$&');

			this.red = parseInt(hex[0] + hex[1], 16);
			this.green = parseInt(hex[2] + hex[3], 16);
			this.blue = parseInt(hex[4] + hex[5], 16);
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
