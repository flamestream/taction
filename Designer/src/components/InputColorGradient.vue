<template>
	<div>
		<div class="preview" ref="preview" :style="previewStyle" @click="handlePreviewClick"></div>
		<div class="stoppers">
			<div v-for="stop in stops" :style="getStopStyle(stop)" :class="getStopClassName(stop)" draggable="true" @mousedown="handleStopMouseDown(stop)" @dragstart="handleStopDragStart" @drag="(ev) => { handleStopDrag(ev, stop) }" :key="stop.id">
				<div class="color" :style="getStopColorStyle(stop)"></div>
			</div>
		</div>
		<input type="number" min="0" max="1" step="0.1" v-model="getActiveStop().position" @change="handleInputNumberChange">
		<input type="range" min="0" max="1" step="0.001" v-model="getActiveStop().position">
		<InputColorSolid :value="getActiveStop().color" @change="handleColorChange"/>
	</div>
</template>

<script>
import Color from '../helpers/Color';
import debounce from '../helpers/debounce';
import InputColorSolid from './InputColorSolid';
export default {
	name: 'InputColorGradient',
	components: {
		InputColorSolid
	},
	props: {
		value: {
			type: Array,
			default: () => []
		}
	},
	data() {
		return {
			dragImage: new Image(),
			activeStop: undefined,
			stops: []
		}
	},
	computed: {
		previewStyle() {

			/* eslint no-mixed-operators: 0 */
			if (!this.stops.length)
				return;

			let out = '';
			let sortedStops = this.stops.slice(0);
			if (sortedStops.length === 1)
				sortedStops.push(sortedStops[0]);
			else
				sortedStops.sort((a, b) => a.position - b.position);
			out += sortedStops.map(s => { return (Color.hexWpf2Web(s.color) || '#fff') + ' ' + ((s.position || 0) * 100) + '%' }).join(', ');
			out = `linear-gradient(90deg, ${out})`;
			return { background: out };
		}
	},
	watch: {
		value(newVal, oldVal) {
			this.syncStops();
		}
	},
	methods: {
		syncStops() {

			let { value, stops } = this;
			stops = value.map(el => new Stop(el));

			// Force minimum
			if (!stops.length)
				stops = [new Stop()];

			this.stops = stops;
		},
		getActiveStop() {
			return this.activeStop || this.stops.length && this.stops[0];
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
			this.activeStop = new Stop(`${ratio}`);
			this.stops.push(this.activeStop);
		},
		handleStopMouseDown(stop) {
			this.activeStop = stop;
		},
		handleStopDragStart(ev) {
			ev.dataTransfer.setDragImage(this.dragImage, 0, 0);
		},
		handleStopDrag: debounce(function(ev, stop) {

			ev.preventDefault();
			let rect = this.$refs.preview.getBoundingClientRect();
			let x = ev.pageX - rect.left;
			let y = ev.pageY - rect.top;

			// Remove check
			if (this.stops.length > 1 && y > rect.height + 50) {
				let idx = this.stops.indexOf(this.activeStop);
				if (idx !== -1) {
					this.stops.splice(idx, 1);
					this.activeStop = undefined;
				}
			}

			x = Math.max(0, Math.min(rect.width, x));
			let ratio = x / rect.width;
			stop.position = ratio;
		}, 2, true),
		getOffsetFromRatio(ratio) {

			return (this.$refs.preview.clientWidth * ratio) >> 0;
		},
		getStopStyle(stop) {

			let offset = this.getOffsetFromRatio(stop.position) + 'px';
			return { 'left': offset };
		},
		getStopColorStyle(stop) {

			return { 'background-color': Color.hexWpf2Web(stop.color) };
		},
		getStopClassName(stop) {

			return {
				stop: true,
				arrow_box: true,
				active: stop === this.getActiveStop()
			}
		},
		handleColorChange(color) {
			this.getActiveStop().color = color.getWpfHex();
		}
	},
	mounted() {
		this.syncStops();
	}
}

class Stop {
	constructor(v) {

		if (typeof v !== 'string')
			v = '';

		let values = v.split(' ');

		this.position = 0;
		try {
			this.position = Number.parseFloat(values[0]);
		} catch (e) { }

		this.color = values[1] || '#fff';

		this.id = ++Stop.currentId;
	}
}
Stop.currentId = -1;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.preview {
	height: 2em;
	border: 1px solid #00000055;
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

</style>
