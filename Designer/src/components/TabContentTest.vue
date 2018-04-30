<template>
	<div class="tab-content-test-root">
		<transition-group name="keycodes-container" class="keycodes-container">
			<div v-for="k in keys" class="keycode" :inactive="!k.active" :key="k.id">{{ k.id }}</div>
		</transition-group>
		<div class="tab-main-container" :style="containerCss">
			<Panel :interactive="true" :obj="obj" @activecommand="onActiveCommand" @inactivecommand="onInactiveCommand"></Panel>
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
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.tab-content-test-root {
	display: flex;
	position: relative;
	flex-direction: column;
}

.tab-main-container {
	flex: 1 1 auto;
	display: flex;
	overflow: auto;
	/*padding: 16px;*/
}

.tab-main-container > .panel {
	margin: auto;
}

.keycodes-container {
	position: absolute;
	top: 10px;
	left: 10px;
}

.keycode {
	border-radius: 3px;
	background-color: #111;
	color: #FFF;
	font-size: 12px;
	font-family: 'Space Mono', monospace;
	padding: 3px 5px;
	margin-bottom: 3px;
	transition: all 0.3s ease-out, opacity 0s ease-out;
}

.keycode[inactive=true] {
	opacity: 0.2;
	transition: all 0.3s ease-out, opacity 0.5s ease-out;
}

.keycodes-container-leave-active {
	position: absolute;
}
.keycodes-container-enter {
	opacity: 0;
	transform: translateY(-30px);
}

.keycodes-container-leave-to {
	opacity: 0;
	transform: translateX(-60px);
}

</style>
