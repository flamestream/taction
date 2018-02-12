<template>
	<div :class="classNames" @click.stop="handleOverlayClick">
		<div v-if="activeOverlay === 'error'" class="error upper">
			<span>{{ errorMsg }}</span>
		</div>
		<div v-else-if="activeOverlay === 'changelog'" class="center">
			<div id="changelog"><span v-html="formattedAbout" @click.stop></span></div>
		</div>
	</div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import changelog from '@/assets/changelog.md'
export default {
	name: 'Overlay',
	props: {
		obj: { type: Object },
		options: { type: Object }
	},
	computed: {
		...mapState('ui', {
			activeOverlay: 'activeOverlay',
			errorMsg: 'errorMsg'
		}),
		hidden() {

			return !this.activeOverlay
		},
		classNames() {

			return {
				overlay: true,
				hidden: this.hidden
			}
		},
		formattedAbout() {

			/* eslint no-undef: 0 */
			return marked(changelog, { sanitize: true })
		}
	},
	methods: {
		...mapActions({
			setErrorMsg: 'ui/setErrorMsg',
			setActiveOverlay: 'ui/setActiveOverlay'
		}),
		handleOverlayClick(ev) {

			this.$el.classList.add('hidden');
			setTimeout(() => this.setActiveOverlay(), 500);
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.overlay {
	position: absolute;
	width: 100%;
	height: 100%;
	background-color: #000000AA;
	z-index: 100;
	color: #FFF;
	transition: all 0.5s;
}

.overlay.hidden {
	background-color: transparent;
	visibility: hidden;
}

.upper {
	margin-top: 128px;
}

.center {
	display: flex;
	height: 100%;
}

.center > div {
	margin: auto;
}

.error {
	width: 100%;
	background-color: #FF000022;
	border: 1px solid #FF000055;
	border-width: 1px 0;
	text-align: center;
}

.error > span {
	text-shadow: 0px 0px 8px #000000;
	display: inline-block;
	margin: 10px 20px;
}

#changelog {
	color: #0D141A;
	background-color: #FFF;
	width: 650px;
	height: 400px;
	border-radius: 3px;
	transition: all 0.5s;
	user-select: text;
	padding: 1em;
	display: flex;
}
.hidden #changelog {
	height: 0;
	opacity: 0;
}

#changelog > span {
	flex: 1 1 auto;
	display: inline-block;
	overflow: auto;
	padding: 10px;
}

#changelog > span > :first-child {
	margin-top: 0;
}

#changelog > span > :last-child {
	margin-bottom: 0;
}

</style>
