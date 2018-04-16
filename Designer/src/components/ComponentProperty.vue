<template>
	<div :class="rootClassNames">
		<div class="label">
			<span @click="handlePointerClick" class="pointer">▼</span>
			<span @click="handlePointerClick" class="text">{{ typeMeta.label }}</span>
			<span v-if="!typeMeta.required" @click="handleRemoverClick" class="remover">－</span>
		</div>
		<component v-show="!hidden" :is="typeMeta.type"
			class="value"
			:options="typeMeta.options"
			:obj="rootObj.getObj(typeMeta.path)"
			@change="onValueChange">
		</component>
	</div>
</template>

<script>
export default {
	name: 'ComponentProperty',
	props: {
		rootObj: { type: Object },
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
			let obj = this.rootObj.getObj(this.typeMeta.path);
			this.$store.commit({
				type: 'layout/setDefined',
				obj,
				value: false
			});
			this.$emit('inactive', {
				component: this.typeMeta
			});
		},
		onValueChange(ev) {
			this.$emit('change', {
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
	margin-top: 5px;
	font-size: 14px;
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

</style>
