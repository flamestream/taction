<template>
	<div>
		<select v-model="type">
			<option>text</option>
			<option>image</option>
		</select>
		<InputText v-if="type === 'text'" :obj="value.value" :options="{label: 'Value: '}"></InputText>
		<InputAsset v-if="type === 'image'" :obj="value.source" :options="{exts: 'png'}"></InputAsset>
	</div>
</template>

<script>
import ContentType from '../types/ContentType';
import InputColorSolid from './InputColorSolid';
import InputColorGradient from './InputColorGradient';
import InputAsset from './InputAsset';
import InputText from './InputText';
export default {
	name: 'PropertyItemContent',
	components: {
		InputColorSolid,
		InputColorGradient,
		InputAsset,
		InputText
	},
	props: {
		obj: { type: ContentType }
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
