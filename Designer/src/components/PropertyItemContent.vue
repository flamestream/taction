<template>
	<div class="property-item-content-root">
		<div class="types">
			<label v-for="type in types" :key="type" :class="typeClassNames(type)" @click="onTypeClick(type)">{{ label(type) }}</label>
		</div>
		<InputText v-if="type === 'text'" :obj="value.value"></InputText>
		<div v-if="type === 'image'">
			<InputAsset :obj="value.source" :options="{exts: 'png'}"></InputAsset>
			<fieldset>
				<legend>Opacity</legend>
				<InputNumber :obj="value.opacity" :options="{ min: 0, max: 1, step: 0.001 }"></InputNumber>
			</fieldset>
			<fieldset>
				<legend>Margin</legend>
				<InputRectangle :obj="value.margin"></InputRectangle>
			</fieldset>
			<fieldset>
				<legend>Colorize</legend>
				<InputColorSolid :obj="value.colorize"></InputColorSolid>
			</fieldset>
		</div>
	</div>
</template>

<script>
import ContentType from '../types/ContentType';
import InputAsset from './InputAsset';
import InputText from './InputText';
import InputNumber from './InputNumber';
import InputRectangle from './PropertyItemRectangle';
import InputColorSolid from './InputColorSolid';

const labels = {
	'text': 'Text',
	'image': 'Image'
}

export default {
	name: 'PropertyItemContent',
	components: {
		InputAsset,
		InputText,
		InputNumber,
		InputRectangle,
		InputColorSolid
	},
	props: {
		obj: { type: ContentType }
	},
	computed: {
		value() {
			let obj = this.obj || {};
			return obj.value;
		},
		types() {
			return this.value.type.options;
		},
		type() {
			return this.value.type.value;
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
.property-item-content-root {
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

fieldset {
	border-width: 0;
	padding: 8px 0 0;
	margin: 8px 0 0;
	border-top: 1px solid #FFFFFF77;
}

legend {
	margin-left: 10px;
}
</style>
