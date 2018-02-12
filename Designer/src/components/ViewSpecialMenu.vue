<template>
	<div>
		<div :class="{ 'menu-item': true, active: activeMenu === 'assets' }" @click.stop="handleAssetsClick">
			<div class="label">
				<i class="material-icons">playlist_add</i>
				<span class="text">Assets</span>
			</div>
		</div>
		<div class="menu-item" @click.stop="handleAboutClick">
			<div class="label">
				<i class="material-icons">new_releases</i>
				<span class="text">Change Log</span>
			</div>
		</div>
	</div>
</template>

<script>
import { mapState, mapActions } from 'vuex';
export default {
	name: 'ViewAssets',
	computed: {
		...mapState('ui', ['activeMenu'])
	},
	methods: {
		...mapActions({
			setActiveOverlay: 'ui/setActiveOverlay',
			setActiveMenu: 'ui/setActiveMenu'
		}),
		handleAssetsClick(ev) {

			this.setActiveMenu({id: 'assets'});
		},
		handleAboutClick(ev) {

			this.setActiveOverlay({id: 'changelog'});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
