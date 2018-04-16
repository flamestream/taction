<template>
	<div class="root">
		<div class="types">
			<label v-for="type in types" :key="type" :class="typeClassNames(type)" @click="onTypeClick(type)">{{ label(type) }}</label>
		</div>
		<div class="description">{{ description }}</div>
	</div>
</template>

<script>
import StringType from '@/types/StringType';

const descriptions = {
	'move': 'This item lets you move the panel around by dragging it. It also collapses the panel with a click.',
	'pivot': 'This item holds other items, changing their position orientation.',
	'radial-menu': 'This item invokes a radial menu when tapped. Radial menu items send key commands.',
	'tap': 'This item sends a key command. The specified key command is only active for as short as possible on item activation.',
	'hold': 'This item sends a key command. The specified key command is active as long as the panel item is held down.',
	'toggle': 'This item sends a key command. The specified key command is active when the item is toggled on by taping it.'
};

const labels = {
	'move': 'Mover',
	'pivot': 'Pivot',
	'radial-menu': 'Radial Menu',
	'tap': 'Tap',
	'hold': 'Hold',
	'toggle': 'Toggle'
}

export default {
	name: 'InputItemType',
	props: {
		obj: { type: StringType }
	},
	computed: {
		value() {
			let { obj } = this;
			return obj.value;
		},
		types() {
			return this.obj.options;
		},
		description() {
			return descriptions[this.value];
		}
	},
	methods: {
		label(type) {
			return labels[type]
		},
		typeClassNames(type) {
			return {
				button: true,
				inactive: type !== this.value
			}
		},
		onTypeClick(value) {
			this.$store.commit({
				type: 'layout/changeType',
				obj: this.obj.parent,
				value
			});
			this.$emit('change', { value });
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
}

.types .button.inactive {
	opacity: 0.3;
}
.description {
	font-size: 14px;
}
</style>
