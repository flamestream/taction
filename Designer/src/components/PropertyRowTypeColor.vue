<template>
	<div>
		<select v-model="type">
			<option>solid</option>
			<option>gradient</option>
			<option>image</option>
		</select>
		<InputColorSolid v-if="type === 'solid'" :value="colorValue"/>
		<InputColorGradient v-if="type === 'gradient'" :value="value && value.values"/>
	</div>
</template>

<script>
import Color from '../helpers/Color';
import InputColorSolid from './InputColorSolid';
import InputColorGradient from './InputColorGradient';
export default {
	name: 'PropertyRowTypeColor',
	components: {
		InputColorSolid,
		InputColorGradient
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
		colorValue() {
			return this.value && this.value.value;
		}
	},
	watch: {
		value(newVal, oldVal) {
			this.syncInputValue();
		}
	},
	methods: {
		syncInputValue() {
			this.type = this.value && this.value.type;
		}
	},
	mounted() {
		this.syncInputValue();
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
