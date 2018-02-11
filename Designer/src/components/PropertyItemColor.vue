<template>
	<div>
		<select v-model="type">
			<option>solid</option>
			<option>gradient</option>
			<option>image</option>
		</select>
		<InputColorSolid v-if="type === 'solid'" :obj="value.value"/>
		<InputColorGradient v-if="type === 'gradient'" :parent="obj" :obj="value.values"></InputColorGradient>
		<InputAsset v-if="type === 'image'" :obj="value.source" :options="{exts: 'png'}"></InputAsset>
	</div>
</template>

<script>
import ColorType from '../types/ColorType';
import InputColorSolid from './InputColorSolid';
import InputColorGradient from './InputColorGradient';
import InputAsset from './InputAsset';
export default {
	name: 'PropertyItemColor',
	components: {
		InputColorSolid,
		InputColorGradient,
		InputAsset
	},
	props: {
		obj: { type: ColorType }
	},
	computed: {
		value() {
			let obj = this.obj || {};
			return obj.value;
		},
		type: {
			get() {
				let { value } = this;
				return value && value.type.value;
			},
			set(value) {
				this.$store.commit({
					type: 'layout/changeType',
					obj: this.obj,
					value
				});
			}
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
