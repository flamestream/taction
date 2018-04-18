<template>
	<div>
		<div>
			<div class="preview" ref="preview" @click="handlePreviewClick"><div :style="previewStyle"></div></div>
			<div class="stoppers">
				<div v-for="stopObj in stopObjs" :style="getStopStyle(stopObj)"
						:class="getStopClassName(stopObj)"
						draggable="true"
						@mousedown="activeStop = stopObj"
						@dragstart="handleStopDragStart"
						@drag.prevent="(ev) => { handleStopDrag(ev, stopObj) }"
						:key="stopObj.id">
					<div class="color" :style="getStopColorStyle(stopObj)"></div>
				</div>
			</div>
		</div>
		<div v-if="activeStop">
			<fieldset>
				<legend>Active Stop<i class="material-icons" @click="removeStop(activeStop)">clear</i></legend>
				<div class="active-stop-position">
					<input type="number" min="0" max="1" step="0.1" v-model="activeStopPosition" @change="handleInputNumberChange">
					<input type="range" min="0" max="1" step="0.001" v-model="activeStopPosition">
				</div>
				<InputColorSolid :obj="activeStopColorObj"/>
			</fieldset>
		</div>
	</div>
</template>

<script>
import ColorType from '../types/ColorType';
import { debounce } from 'lodash';
import InputColorSolid from './InputColorSolid';
export default {
	name: 'InputColorGradient',
	components: {
		InputColorSolid
	},
	props: {
		obj: { type: Array },
		parent: { type: ColorType }
	},
	data() {
		return {
			stopObjs: [],
			dragImage: new Image(),
			rawActiveStop: undefined,
			cachedPreviewRect: undefined
		}
	},
	computed: {
		previewStyle() {

			/* eslint no-mixed-operators: 0 */
			if (!this.stopObjs || !this.stopObjs.length)
				return;

			let out = '';
			let sortedStopObjs = this.stopObjs.slice(0);
			if (sortedStopObjs.length === 1)
				sortedStopObjs.push(sortedStopObjs[0]);
			else
				sortedStopObjs.sort((a, b) => a.value.position.value - b.value.position.value);

			out += sortedStopObjs.map(s => { return s.value.color.value.getHex() + ' ' + ((s.value.position.value || 0) * 100) + '%' }).join(', ');
			out = `linear-gradient(90deg, ${out})`;
			return { background: out };
		},
		activeStop: {
			get() {
				return this.rawActiveStop || this.stopObjs && this.stopObjs[0];
			},
			set(v) {
				this.rawActiveStop = v;
			}
		},
		activeStopPosition: {
			get() {

				let { activeStop } = this;
				if (!activeStop) return;
				return activeStop.value.position.value;
			},
			set(value) {

				let { activeStop } = this;
				if (!activeStop) return;

				this.$store.commit({
					type: 'layout/setValue',
					obj: activeStop.value.position,
					value
				});
			}
		},
		activeStopColorObj() {

			let { activeStop } = this;
			if (!activeStop) return;
			return activeStop.value.color;
		}
	},
	watch: {
		obj(newVal, oldVal) {
			this.syncObj();
		}
	},
	methods: {
		syncObj() {
			this.stopObjs = this.obj;
			this.activeStop = this.stopObjs && this.stopObjs[this.stopObjs.length - 1];
		},
		setActiveStop(stopObj) {
			this.activeStop = stopObj;
		},
		handleInputNumberChange(ev) {

			let { currentTarget } = ev;
			let { value } = currentTarget;
			currentTarget.value = Math.max(currentTarget.min, Math.min(currentTarget.max, value));
		},
		handlePreviewClick(ev) {

			// Calculate position
			let rect = ev.currentTarget.getBoundingClientRect();
			let x = ev.pageX - rect.left;
			let ratio = x / rect.width;

			this.$store.commit({
				type: 'layout/addValueElement',
				obj: this.parent,
				key: 'values',
				value: ratio.toString()
			});
		},
		handleStopDragStart(ev) {
			this.cachedPreviewRect = this.$refs.preview.getBoundingClientRect();
			ev.dataTransfer.setDragImage(this.dragImage, 0, 0);
		},
		handleStopDrag: debounce(function(ev, stopObj) {

			let rect = this.cachedPreviewRect;
			let x = ev.pageX - rect.left;

			x = Math.max(0, Math.min(rect.width, x));
			let ratio = x / rect.width;

			this.$store.commit({
				type: 'layout/setValue',
				obj: stopObj.value.position,
				value: ratio.toFixed(3)
			});
		}, 2),
		removeStop(stopObj) {

			this.$store.commit({
				type: 'layout/removeValueElement',
				obj: this.parent,
				key: 'values',
				value: stopObj
			});
		},
		getOffsetFromRatio(ratio) {

			return (this.$refs.preview.clientWidth * ratio) >> 0;
		},
		getStopStyle(stopObj) {

			let offset = this.getOffsetFromRatio(stopObj.value.position.value) + 'px';
			return { 'left': offset };
		},
		getStopColorStyle(stopObj) {

			return { 'background-color': stopObj.value.color.value.getHex() };
		},
		getStopClassName(stopObj) {

			return {
				stop: true,
				arrow_box: true,
				active: stopObj === this.activeStop
			}
		}
	},
	mounted() {
		this.syncObj();
	}
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.preview {
	height: 2em;
	border: 1px solid black;
	background-color: #fff;
	background: url('../assets/transparent-bg.jpg');
	background-size: 20px;
	display: flex;
}

.preview > div {
	flex: 1 1 auto;
}

input[type=number] {
	width: 4em;
}

.stoppers {
	position: relative;
	margin: 0 -4px;
	height: 12px;
}

.stop {
	width: 6px;
	height: 10px;
	padding: 1px;
}

.color {
	height: 100%;
}

.arrow_box {
	position: absolute;
	background: #fff;
	border: 1px solid #222;
	border-top-width: 0;
	opacity: 0.4;
}
.arrow_box:after, .arrow_box:before {
	bottom: 100%;
	left: 50%;
	border: solid transparent;
	content: " ";
	height: 0;
	width: 0;
	position: absolute;
	pointer-events: none;
}

.arrow_box:after {
	border-color: rgba(255, 255, 255, 0);
	border-bottom-color: #fff;
	border-width: 4px;
	margin-left: -4px;
}
.arrow_box:before {
	border-color: rgba(0, 0, 0, 0);
	border-bottom-color: #222;
	border-width: 5px;
	margin-left: -5px;
}

.arrow_box.active {
	opacity: 1;
}

fieldset {
	padding: 0;
	margin: 0;
	border: 0;
	border-top: 1px solid #FFFFFF77;
}

legend {
	margin-left: 10px;
	line-height: 20px;
}

i {
	vertical-align: text-top;
	font-size: 20px;
	margin-left: 4px;
	transition: color 0.2s ease-out;
	cursor: pointer;
}

i:hover {
	color: #CC0000;
}

.active-stop-position {
	margin: 6px 0;
	display: flex;
	flex-direction: row;
}

.active-stop-position input[type=number] {
	margin-right: 4px;
}

.active-stop-position input[type=range] {
	flex: 1 1 auto;
}

</style>
