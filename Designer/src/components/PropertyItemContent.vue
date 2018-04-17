<template>
	<div class="root">
		<div class="types">
			<label v-for="type in types" :key="type" :class="typeClassNames(type)" @click="onTypeClick(type)">{{ label(type) }}</label>
		</div>
		<InputText v-if="type === 'text'" :obj="value.value"></InputText>
		<InputAsset v-if="type === 'image'" :obj="value.source" :options="{exts: 'png'}"></InputAsset>
	</div>
</template>

<script>
import ContentType from '../types/ContentType';
import InputAsset from './InputAsset';
import InputText from './InputText';

const labels = {
	'text': 'Text',
	'image': 'Image'
}

export default {
	name: 'PropertyItemContent',
	components: {
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
.root {
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
