<template>
	<div class="tab-content-preview-root">
		<div class="tab-content-preview-scrollable-area" :style="containerCss">
			<div ref="baseArea" class="tab-content-preview-area" :style="containerCss">
				<label ref="baseLabel"><span>Base Style</span></label>
				<Panel ref="basePanel" :obj="obj"></Panel>
				<transition name="highlight">
					<div v-show="highlightItem" class="highlight"></div>
				</transition>
			</div>
			<div ref="activeArea" class="tab-content-preview-area" :style="containerCss">
				<label ref="activeLabel"><span>Active Style</span></label>
				<Panel ref="activePanel" :obj="obj" :forceActive="true"></Panel>
				<transition name="highlight">
					<div v-show="highlightItem" class="highlight"></div>
				</transition>
			</div>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex'
import Panel from './Panel'
export default {
	name: 'TabContentTest',
	components: {
		Panel
	},
	data() {
		return {
			keyCount: 0,
			isTouchTriggered: false,
			isMouseOver: false,
			isMouseOverMover: false,
			keyRegistry: {},
			keys: []
		}
	},
	computed: {
		...mapState('layout', {
			obj: 'layout'
		}),
		...mapState('ui', ['highlightItem']),
		containerCss() {
			return {
				flexDirection: this.obj.value.orientation.value === 'vertical' ? 'row' : 'column'
			};
		}
	},
	methods: {
		onActiveCommand(ev) {

			let { command } = ev;
			let keys = command.split(' ');
			for (let k of keys)
				this.activateKey({ key: k });
		},
		onInactiveCommand(ev) {

			let { command } = ev;
			let keys = command.split(' ');
			for (let k of keys)
				this.activateKey({ key: k, value: false });
		},
		activateKey({key, value = true}) {

			let { keyRegistry } = this;

			// Activate/create
			if (!(key in keyRegistry)) {

				keyRegistry[key] = {
					id: key
				}
			}
			let k = keyRegistry[key];
			k.active = value;

			// Push up
			let { keys } = this;
			for (let i = keys.length - 1; i >= 0; --i) {

				let k2 = keys[i];
				if (k2 !== k)
					continue;
				keys.splice(i, 1);
			}
			keys.unshift(k);

			// Overflow limit
			while (keys.length > 5)
				keys.pop();
		},
		onHighlightAfterEnter() {

			this.highlightActive = false;
		},
		initHighlight(activeItem, panel, area) {

			let target = panel.$refs.items.find(i => { return i.obj === activeItem; });
			if (!target)
				return;

			let targetRect = target.$el.getBoundingClientRect();
			let containerRect = area.getBoundingClientRect();
			let top = targetRect.top - containerRect.top;
			let left = targetRect.left - containerRect.left;

			let highlight = area.getElementsByClassName('highlight')[0];
			highlight.style.top = top + 'px';
			highlight.style.left = left + 'px';
			highlight.style.width = targetRect.width + 'px';
			highlight.style.height = targetRect.height + 'px';
		}
	},
	watch: {
		highlightItem(value) {

			this.initHighlight(value, this.$refs.basePanel, this.$refs.baseArea);
			this.initHighlight(value, this.$refs.activePanel, this.$refs.activeArea);
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.tab-content-preview-root {
	display: flex;
}

.tab-content-preview-scrollable-area {
	flex: 1 1 auto;
	display: flex;
	overflow: auto;
}

.tab-content-preview-area {
	flex: 1 1 auto;
	display: flex;
	position: relative;
}

.tab-content-preview-area > .panel {
	margin: auto;
}

label {
	position: fixed;
	border: 1px solid #1B283855;
	display: inline-block;
	padding: 2px 6px;
	margin: 5px;
	font-size: 10px;
	background-color: #1B2838;
	color: #FFFFFF;
	border-radius: 3px;
	transition: opacity 0.2s ease-out;
}

label:hover {
	opacity: 0;
}

.highlight {
	position: absolute;
	top: 0;
	left: 0;
	background-color: #09B0EB;
	opacity: 0.4;
	transition: all 0.2s ease-out;
	animation: flash linear 1s infinite;
}

@keyframes flash {
	0% { opacity: 0.4; }
	50% { opacity: 0; }
	100% { opacity: 0.4; }
}

.highlight-enter-active, .highlight-leave-active {
	animation: none;
}

.highlight-enter, .highlight-leave-to {
	opacity: 0;
	border-width: 0;
	transform: scale(1.5);
}

</style>
