<template>
	<div>
		<select :value="type">
			<option>solid</option>
			<option>gradient</option>
			<option>image</option>
		</select>
		<InputColorSolid v-if="type === 'solid'" :obj="colorObj"/>
		<InputColorGradient v-if="type === 'gradient'" :parent="obj" :obj="colorObj"/>
		<InputLoadedFile v-if="type === 'image'" filter="png"/>
	</div>
</template>

<script>
import ColorType from '../layout/ColorType';
import InputColorSolid from './InputColorSolid';
import InputColorGradient from './InputColorGradient';
import InputLoadedFile from './InputLoadedFile';
export default {
	name: 'PropertyItemColor',
	components: {
		InputColorSolid,
		InputColorGradient,
		InputLoadedFile
	},
	props: {
		obj: { type: ColorType }
	},
	computed: {
		value() {
			let obj = this.obj || {};
			return obj.value;
		},
		typeObj() {
			let value = this.value || {};
			return value.type;
		},
		type() {
			let typeObj = this.typeObj || {};
			return typeObj.value;
		},
		colorObj() {
			let value = this.value || {};
			return value.value || value.values;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
