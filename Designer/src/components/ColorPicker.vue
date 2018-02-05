<template>
	<span>
		<input ref="input" type="color" :value="hexColor" @click="handleColorInputClick"/>
		<div class="extra" ref="extra">
			<div class="alpha">
				<input type="text" v-model="alpha" @click="handleClickToSelect"/>
				<input type="range" min="0" max="1" step="0.001" v-model="alpha"/>
			</div>
			<div class="name">
				<input type="text" :value="name" @change="handleInputNameChange" @click="handleClickToSelect"/>
			</div>
		</div>
	</span>
</template>

<script>
import CP from '../vendor/color-picker';
import Color from '../helpers/Color';
import scrollparent from '../helpers/scrollparent';
export default {
	name: 'ColorPicker',
	props: {
		value: {
			type: Color,
			default: () => new Color()
		}
	},
	data() {
		return {
			color: new Color(),
			picker: undefined
		}
	},
	computed: {
		hexColor() {
			return this.color.getSimpleHex();
		},
		alpha: {
			get() {
				let v = (this.color.alpha / 255).toFixed(3);
				return v;
			},
			init(v) {

				let value = parseFloat(v);
				if (Number.isNaN(value)) value = 1;
				value *= 255;
				this.color.alpha = value >> 0;
			}
		},
		name: {
			get() {
				return this.color.getHex();
			},
			init(v) {

				let color = Color.fromHex(v, false) || Color.fromName(v);
				this.picker.set(color.getSimpleHex());
			}
		}
	},
	watch: {
		value(newValue) {
			this.syncInputValue();
		}
	},
	methods: {
		handleColorChange() {
			console.log('colro chaneg!', this.color);
			this.$emit('colorChange', this.color);
		},
		handleColorInputClick(ev) {
			ev.preventDefault();
		},
		handleInputNameChange(ev) {

			let {currentTarget} = ev;
			console.log(currentTarget.value);
			this.name = currentTarget.value;
		},
		handleClickToSelect(ev) {

			let {currentTarget} = ev;
			currentTarget.select();
		},
		setColorFromHexDigits(hex) {

			let newColor = Color.fromHex(`#${hex}`);
			newColor.alpha = this.color.alpha;
			this.color = newColor;
		},
		syncInputValue() {

			let { value } = this;
			let hex = value.getSimpleHex();

			this.color = value;
			this.picker.set(hex);
		}
	},
	mounted() {

		this.picker = new CP(this.$refs.input);

		let { picker } = this;
		picker.picker.appendChild(this.$refs.extra);
		picker.on('change', h => this.setColorFromHexDigits(h));
		picker.on('exit', () => this.handleColorChange());

		let panel = scrollparent(this.$el);
		if (panel) {

			picker.on('enter', function() {

				this._initScrollPosition = {
					x: panel.scrollTop,
					y: panel.scrollLeft
				};

				this.picker.style.marginTop = '0px';
				this.picker.style.marginLeft = '0px';
			});

			panel.addEventListener('scroll', function() {

				if (!picker._initScrollPosition) return;
				picker.picker.style.marginTop = picker._initScrollPosition.x - this.scrollTop + 'px';
				picker.picker.style.marginLeft = picker._initScrollPosition.y - this.scrollLeft + 'px';

			}, false);
		}
	}
}
</script>

<style>

.color-picker {

	width: 184px !important;
	border: 6px solid #111;
	border-radius: 4px;
}

.color-picker .extra > div {

	display: flex;
	background-color: #111;
	padding-top: 3px
}

.color-picker .alpha input[type=text] {

	width: 50px;
	margin-right: 4px;
	flex: 0;
	text-align: right;
	padding-left: 3px;
	padding-right: 3px;
}

.color-picker .alpha input[type=range] {

	flex: 1 1;
	width: 30px;
}

.color-picker .name input {

	flex: 1 1;
	width: 30px;
}

/* Library CSS */

.color-picker,
.color-picker:before,
.color-picker:after,
.color-picker *,
.color-picker *:before,
.color-picker *:after {
	-webkit-box-sizing:border-box;
	-moz-box-sizing:border-box;
	box-sizing:border-box;
}
.color-picker {
	position:absolute;
	top:0;
	left:0;
	z-index:9999;
	width: 172px;
}
.color-picker-control {
	border:1px solid #000;
	-webkit-box-shadow:1px 5px 10px rgba(0,0,0,.5);
	-moz-box-shadow:1px 5px 10px rgba(0,0,0,.5);
	box-shadow:1px 5px 10px rgba(0,0,0,.5);
	white-space:nowrap;
	font-size:0px;
}
.color-picker-control *,
.color-picker-control *:before,
.color-picker-control *:after {border-color:inherit}

.color-picker i {font:inherit}
.color-picker-h {
	position:relative;
	width:20px;
	height:150px;
	display:inline-block;
	border-left:1px solid;
	border-left-color:inherit;
	cursor:ns-resize;
	background:transparent url('../assets/color-picker-h.png') no-repeat 50% 50%;
	background-image:-webkit-linear-gradient(to top,#f00 0%,#ff0 17%,#0f0 33%,#0ff 50%,#00f 67%,#f0f 83%,#f00 100%);
	background-image:-moz-linear-gradient(to top,#f00 0%,#ff0 17%,#0f0 33%,#0ff 50%,#00f 67%,#f0f 83%,#f00 100%);
	background-image:linear-gradient(to top,#f00 0%,#ff0 17%,#0f0 33%,#0ff 50%,#00f 67%,#f0f 83%,#f00 100%);
	-webkit-background-size:100% 100%;
	-moz-background-size:100% 100%;
	background-size:100% 100%;
	overflow:hidden;
}
.color-picker-h i {
	position:absolute;
	top:-3px;
	right:0;
	left:0;
	z-index:3;
	display:block;
	height:6px;
}
.color-picker-h i:before {
	content:"";
	position:absolute;
	top:0;
	right:0;
	bottom:0;
	left:0;
	display:block;
	border:3px solid;
	border-color:inherit;
	border-top-color:transparent;
	border-bottom-color:transparent;
}
.color-picker-sv {
	position:relative;
	width:150px;
	height:150px;
	display:inline-block;
	background:transparent url('../assets/color-picker-sv.png') no-repeat 50% 50%;
	background-image:-webkit-linear-gradient(to top,#000,rgba(0,0,0,0)),linear-gradient(to right,#fff,rgba(255,255,255,0));
	background-image:-moz-linear-gradient(to top,#000,rgba(0,0,0,0)),linear-gradient(to right,#fff,rgba(255,255,255,0));
	background-image:linear-gradient(to top,#000,rgba(0,0,0,0)),linear-gradient(to right,#fff,rgba(255,255,255,0));
	-webkit-background-size:100% 100%;
	-moz-background-size:100% 100%;
	background-size:100% 100%;
	cursor:crosshair;
}
.color-picker-sv i {
	position:absolute;
	top:-4px;
	right:-4px;
	z-index:3;
	display:block;
	width:8px;
	height:8px;
}
.color-picker-sv i:before,
.color-picker-sv i:after {
	content:"";
	position:absolute;
	top:0;
	right:0;
	bottom:0;
	left:0;
	display:block;
	border:1px solid;
	border-color:inherit;
	-webkit-border-radius:100%;
	-moz-border-radius:100%;
	border-radius:100%;
}
.color-picker-sv i:before {
	top:-1px;
	right:-1px;
	bottom:-1px;
	left:-1px;
	border-color:#fff;
}
.color-picker-h,
.color-picker-sv {
	-webkit-touch-callout:none;
	-webkit-user-select:none;
	-moz-user-select:none;
	-ms-user-select:none;
	user-select:none;
	-webkit-tap-highlight-color:rgba(0,0,0,0);
	-webkit-tap-highlight-color:transparent;
}

</style>
