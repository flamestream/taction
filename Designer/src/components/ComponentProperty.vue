<template>
	<div :class="rootClassNames">
		<div class="label">
			<span @click="handlePointerClick" class="pointer">▼</span>
			<span @click="handlePointerClick" class="text">{{ typeMeta.label }}</span>
			<span @click="handleRemoverClick" class="remover">－</span>
		</div>
		<component :is="typeMeta.type" :options="typeMeta.options" :obj="activeItem.getObj(typeMeta.path)" class="value"></component>
	</div>
</template>

<script>
export default {
	name: 'ComponentProperty',
	props: {
		activeItem: { type: Object },
		typeMeta: { type: Object }
	},
	data() {
		return {
			hidden: false
		};
	},
	computed: {
		rootClassNames() {
			return {
				item: true,
				hidden: this.hidden
			}
		}
	},
	methods: {
		handlePointerClick(ev) {
			this.hidden = !this.hidden;
		},
		handleRemoverClick(ev) {
			let obj = this.activeItem.getObj(this.typeMeta.path);
			this.$store.commit({
				type: 'layout/setDefined',
				obj,
				value: false
			});
			this.$emit('inactive', {
				component: this.typeMeta
			});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.item {
	background-color: #00000088;
	border-radius: 3px;
	margin-bottom: 5px;
	padding: 5px;
}
.item:last-child {
	margin-bottom: 0;
}

.label {
	display: flex;
}

.label > span {
	display: inline-block;
	line-height: 24px;
	transition: all 0.1s ease-out;
}
.item.undefined .label > span {
	opacity: 0.4;
}

.value {
	overflow: hidden;
}

.hidden > .value {
	max-height: 0px;
}

.hidden .pointer {
	transform: rotate(-90deg);
}

.label > .text {
	flex: 1 1;
	margin-left: 4px;
}

.label > .remover {
	vertical-align: middle;
	cursor: pointer;
}

.label > .remover:hover {
	color: #C00;
	font-weight: bold;
}

.value {
	margin-top: 5px;
}

</style>
