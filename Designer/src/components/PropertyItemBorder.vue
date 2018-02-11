<template>
	<div>
		<div class="item">
			<div class="label"><span>Thickness</span><input v-if="!required(value.thickness)" type="checkbox" v-model="thicknessDefined"/></div>
			<div class="value" v-if="thicknessDefined">
				<PropertyItemRectangle :obj="value.thickness"/>
			</div>
		</div>
		<div class="item">
			<div class="label"><span>Roundness</span><input v-if="!required(value.radius)" type="checkbox" v-model="radiusDefined"/></div>
			<div class="value" v-if="radiusDefined">
				<PropertyItemRectangle :obj="value.radius" :options="{corner: true}"/>
			</div>
		</div>
		<div class="item">
			<div class="label"><span>Color</span><input v-if="!required(value.color)" type="checkbox" v-model="colorDefined"/></div>
			<div class="value" v-if="colorDefined">
				<PropertyItemColor :obj="value.color"/>
			</div>
		</div>
	</div>
</template>

<script>
import BorderType from '../types/BorderType';
import PropertyItemRectangle from './PropertyItemRectangle'
import PropertyItemColor from './PropertyItemColor'
export default {
	name: 'PropertyItemBorder',
	components: {
		PropertyItemRectangle,
		PropertyItemColor
	},
	props: {
		obj: { type: BorderType }
	},
	computed: {
		value() {
			let { obj } = this;
			return obj.value;
		},
		thicknessDefined: {
			get() {
				return this.getDefined(this.value.thickness);
			},
			set(value) {
				return this.setDefined(this.value.thickness, value);
			}
		},
		radiusDefined: {
			get() {
				return this.getDefined(this.value.radius);
			},
			set(value) {
				return this.setDefined(this.value.radius, value);
			}
		},
		colorDefined: {
			get() {
				return this.getDefined(this.value.color);
			},
			set(value) {
				return this.setDefined(this.value.color, value);
			}
		}
	},
	methods: {
		required(obj) {
			return obj.required;
		},
		getDefined(obj) {
			return !obj.notDefined;
		},
		setDefined(obj, value) {
			this.$store.commit({
				type: 'layout/setDefined',
				obj,
				value
			});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.item {
	background-color: #00000033;
	border-radius: 3px;
	margin-bottom: 5px;
	padding: 5px;
}

.label {
	display: flex;
}

.label > span {
	display: inline-block;
	flex: 1 1;
}

.label > input[type=checkbox] {
	vertical-align: middle;
}

.value {
	margin-top: 5px;
}

</style>
