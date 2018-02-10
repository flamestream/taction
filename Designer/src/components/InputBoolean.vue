<template>
	<div class="container">
		<span v-if="label">{{ label }}</span>
		<div class="checkbox" @click.stop="handleClick">
			<input type="checkbox" :id="id" v-model="value" />
			<label :for="id"></label>
		</div>
	</div>
</template>

<script>
import BooleanType from '../types/BooleanType';
export default {
	name: 'InputBoolean',
	props: {
		obj: { type: BooleanType },
		options: { type: Object }
	},
	computed: {
		value: {
			get() {

				let obj = this.obj || {};
				let value = obj.value;

				if (this.options.invert)
					value = !value;

				return value;
			},
			set(value) {

				if (this.options.invert)
					value = !value;

				this.$store.commit({
					type: 'layout/setValue',
					obj: this.obj,
					value
				});
			}
		},
		id() {

			return 'boolean' + this.obj.id;
		},
		label() {

			let options = this.options || {};
			return options.label;
		}
	},
	methods: {
		handleClick(ev) {

			this.value = !this.value;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.container {
	display: flex;
	justify-content: center;
}

.checkbox {
	width: 80px;
	height: 26px;
	background: #1B2838;
	margin: 20px auto;
	position: relative;
	border-radius: 50px;
	-webkit-box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.5), 0px 1px 0px rgba(255, 255, 255, 0.2);
	box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.5), 0px 1px 0px rgba(255, 255, 255, 0.2);
	cursor: pointer;
}
.checkbox:after {
	content: 'OFF';
	color: #000;
	position: absolute;
	right: 10px;
	z-index: 0;
	font: 12px/26px Arial, sans-serif;
	font-weight: bold;
	text-shadow: 1px 1px 0px rgba(255, 255, 255, 0.15);
}
.checkbox:before {
	content: 'ON';
	color: #27ae60;
	position: absolute;
	left: 10px;
	z-index: 0;
	font: 12px/26px Arial, sans-serif;
	font-weight: bold;
}
.checkbox label {
	display: block;
	width: 34px;
	height: 20px;
	cursor: pointer;
	position: absolute;
	top: 3px;
	left: 3px;
	z-index: 1;
	background: #fcfff4;
	background: -webkit-gradient(linear, left top, left bottom, from(#fcfff4), color-stop(40%, #dfe5d7), to(#b3bead));
	background: linear-gradient(to bottom, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
	border-radius: 50px;
	-webkit-transition: all 0.4s ease;
	transition: all 0.4s ease;
	-webkit-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.3);
	box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.3);
}
.checkbox input[type=checkbox] {
	visibility: hidden;
}
.checkbox input[type=checkbox]:checked + label {
	left: 43px;
}

</style>
