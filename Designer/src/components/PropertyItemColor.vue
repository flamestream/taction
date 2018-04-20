<template>
	<div class="property-item-color-root">
		<div class="types">
			<label v-for="type in types" :key="type" :class="typeClassNames(type)" @click="onTypeClick(type)">{{ label(type) }}</label>
		</div>
		<InputColorSolid v-if="type === 'solid'" :obj="value.value"/>
		<InputColorGradient v-if="type === 'gradient'" :parent="obj" :obj="value.values"></InputColorGradient>
		<InputAsset v-if="type === 'image'" :obj="value.source" :options="{exts: 'png'}"></InputAsset>
	</div>
</template>

<script>
import { get } from 'lodash';
import ColorType from '../types/ColorType';
import InputColorSolid from './InputColorSolid';
import InputColorGradient from './InputColorGradient';
import InputAsset from './InputAsset';

const labels = {
	'solid': 'Solid',
	'gradient': 'Gradient',
	'image': 'Image'
}

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
			let value = get(this, ['obj', 'value']);
			return value;
		},
		types() {
			return this.obj.types;
		},
		type() {
			return get(this, 'value.type.value');
		}
	},
	methods: {
		label(type) {
			return labels[type]
		},
		typeClassNames(type) {
			return {
				button: true,
				inactive: type !== this.type
			}
		},
		onTypeClick(value) {
			this.$store.commit({
				type: 'layout/changeType',
				obj: this.obj,
				value
			});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.property-item-color-root {
	display: flex;
	flex-direction: column;
}

.types {
	display: flex;
	flex-wrap: wrap;
	justify-content: center;
	margin-bottom: 8px;
}

.types label {
	margin: 2px;
	font-size: 16px;
}

.types .button.inactive {
	opacity: 0.3;
}

</style>
