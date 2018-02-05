<template>
	<div>
		<div class="item">
			<div class="label">Thickness</div>
			<PropertyItemRectangle :obj="thicknessObj"/>
		</div>
		<div class="item">
			<div class="label">Roundness</div>
			<PropertyItemRectangle :obj="radiusObj"/>
		</div>
		<div class="item">
			<div class="label">Color</div>
			<PropertyItemColor :obj="colorObj"/>
		</div>
	</div>
</template>

<script>
import BorderType from '../layout/BorderType';
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
			let obj = this.obj || {};
			return obj.value;
		},
		colorObj() {
			let value = this.value || {};
			return value && value.color;
		},
		radiusObj() {
			let value = this.value || {};
			return value && value.radius;
		},
		thicknessObj() {
			let value = this.value || {};
			return value && value.thickness;
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
