<template>
	<div class="item">
		<div class="label">{{ label }}</div>
		<div v-if="type === 'number'">
			<input type="number" v-model="value" :min="options.min" :max="options.max" :step="options.step"/>
		</div>
		<div v-else-if="type === 'checkbox'">
			<input type="checkbox" v-model="value"/>
		</div>
		<div v-else-if="type === 'range'">
			<input type="range" v-model="value" :min="options.min" :max="options.max" :step="options.step"/>
		</div>
		<div v-else-if="type === 'rectangle'">
			<PropertyRowTypeRectangle :obj="obj"/>
		</div>
		<div v-else-if="type === 'border'">
			<PropertyRowTypeBorder :obj="obj"/>
		</div>
		<div v-else-if="type === 'color'">
			<PropertyRowTypeColor :obj="obj"/>
		</div>
		<div v-else-if="type === 'option'">
			<select v-model="value">
				<option v-for="v in options.options" :key="v">{{ v }}</option>
			</select>
		</div>
		<div v-else><input type="text" v-model="value"/></div>
	</div>
</template>

<script>
import PropertyRowTypeRectangle from './PropertyRowTypeRectangle'
import PropertyRowTypeBorder from './PropertyRowTypeBorder'
import PropertyRowTypeColor from './PropertyRowTypeColor'
export default {
	name: 'PropertyRow',
	components: {
		PropertyRowTypeRectangle,
		PropertyRowTypeBorder,
		PropertyRowTypeColor
	},
	props: ['label', 'obj', 'type', 'options'],
	computed: {
		value: {
			get() {
				let obj = this.obj || {};
				return obj.value;
			},
			set(value) {

				this.$store.commit({
					type: 'setValue',
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

.item {

	background-color: #00000033;
	border-radius: 3px;
	margin-bottom: 5px;
	padding: 5px;
}
</style>
