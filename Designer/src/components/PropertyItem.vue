<template>
	<div v-if="obj" :class="rootClassNames">
		<div class="label">
			<span @click="handlePointerClick" class="pointer">▼</span>
			<span @click="handlePointerClick" class="text">{{ label }}</span>
			<input v-if="required" type="checkbox" v-model="defined"/>
		</div>
		<div class="value" v-if="defined && !hidden">
			<div v-if="type === 'number'">
				<InputNumber :obj="obj" :options="options" :parent="parent"></InputNumber>
			</div>
			<div v-else-if="type === 'asset'">
				<InputAsset :obj="obj" :options="options" :parent="parent"></InputAsset>
			</div>
			<div v-else-if="type === 'checkbox'">
				<InputBoolean :obj="obj" :options="options" :parent="parent"></InputBoolean>
			</div>
			<div v-else-if="type === 'option'">
				<InputOption :obj="obj" :options="options" :parent="parent"></InputOption>
			</div>
			<div v-else-if="type === 'rectangle'">
				<PropertyItemRectangle :obj="obj" :options="options" :parent="parent"></PropertyItemRectangle>
			</div>
			<div v-else-if="type === 'border'">
				<PropertyItemBorder :obj="obj" :options="options" :parent="parent"></PropertyItemBorder>
			</div>
			<div v-else-if="type === 'color'">
				<PropertyItemColor :obj="obj" :options="options" :parent="parent"></PropertyItemColor>
			</div>
			<div v-else-if="type === 'content'">
				<PropertyItemContent :obj="obj" :options="options" :parent="parent"></PropertyItemContent>
			</div>
			<div v-else-if="type === 'button-style-set'">
				<PropertyItemButtonStyleSet :obj="obj" :options="options" :parent="parent"></PropertyItemButtonStyleSet>
			</div>
			<div v-else-if="type === 'button-style'">
				<PropertyItemButtonStyle :obj="obj" :options="options" :parent="parent"></PropertyItemButtonStyle>
			</div>
			<div v-else-if="type === 'text-style'">
				<PropertyItemTextStyle :obj="obj" :options="options" :parent="parent"></PropertyItemTextStyle>
			</div>
			<div v-else-if="type === 'global'">
				<PropertyItemGlobal :obj="obj" :options="options" :parent="parent"></PropertyItemGlobal>
			</div>
			<div v-else-if="type === 'item'">
				<PropertyItemItem :obj="obj" :options="options" :parent="parent"></PropertyItemItem>
			</div>
			<div v-else-if="type === 'type'">
				<PropertyItemType :obj="obj" :options="options" :parent="parent"></PropertyItemType>
			</div>
			<div v-else>
				<InputText :obj="obj" :options="options" :parent="parent"></InputText>
			</div>
		</div>
	</div>
</template>

<script>
import InputText from './InputText';
import InputNumber from './InputNumber';
import InputBoolean from './InputBoolean';
import InputOption from './InputOption';
import InputAsset from './InputAsset';
export default {
	name: 'PropertyItem',
	components: {
		InputText,
		InputNumber,
		InputBoolean,
		InputOption,
		InputAsset
	},
	props: {
		obj: { type: Object },
		parent: { type: Object },
		label: { type: String },
		type: { type: String },
		options: { type: Object }
	},
	data() {
		return {
			hidden: false
		};
	},
	beforeCreate() {
		// Recursive dependency avoidance
		this.$options.components.PropertyItemRectangle = require('./PropertyItemRectangle').default;
		this.$options.components.PropertyItemBorder = require('./PropertyItemBorder').default;
		this.$options.components.PropertyItemColor = require('./PropertyItemColor').default;
		this.$options.components.PropertyItemContent = require('./PropertyItemContent').default;
		this.$options.components.PropertyItemButtonStyle = require('./PropertyItemButtonStyle').default;
		this.$options.components.PropertyItemButtonStyleSet = require('./PropertyItemButtonStyleSet').default;
		this.$options.components.PropertyItemTextStyle = require('./PropertyItemTextStyle').default;
		this.$options.components.PropertyItemGlobal = require('./PropertyItemGlobal').default;
		this.$options.components.PropertyItemItem = require('./PropertyItemItem').default;
		this.$options.components.PropertyItemType = require('./PropertyItemType').default;
	},
	computed: {
		defined: {
			get() {
				let { obj } = this;
				return !obj.notDefined;
			},
			set(value) {
				this.$store.commit({
					type: 'layout/setDefined',
					obj: this.obj,
					value
				});
			}
		},
		required() {
			let { obj } = this;
			return !obj.required;
		},
		rootClassNames() {

			return {
				item: true,
				hidden: this.hidden,
				undefined: !this.defined
			}
		}
	},
	methods: {
		handlePointerClick(ev) {

			if (!this.defined)
				return;

			this.hidden = !this.hidden;
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

.hidden .pointer {
	transform: rotate(-90deg);
}

.label > .text {
	flex: 1 1;
	margin-left: 4px;
}

.label > input[type=checkbox] {
	vertical-align: middle;
}

.value {
	margin-top: 5px;
}

</style>
