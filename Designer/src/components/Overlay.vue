<template>
	<div :class="classNames" @click.stop="handleOverlayClick">
		<div v-if="errorMsg" class="error upper">
			<span>{{ errorMsg }}</span>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex'
export default {
	name: 'Overlay',
	props: {
		obj: { type: Object },
		options: { type: Object }
	},
	computed: {
		...mapState('ui', {
			errorMsg: 'errorMsg'
		}),
		classNames() {

			return {
				overlay: true,
				hidden: !this.errorMsg
			}
		}
	},
	methods: {
		handleOverlayClick(ev) {

			this.$store.dispatch({
				type: 'setErrorMsg'
			});
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

</style>
