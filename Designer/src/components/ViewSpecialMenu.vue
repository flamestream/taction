<template>
	<div>
		<div :class="{ 'menu-item': true, active: activeMenu === 'assets' }" @click.stop="handleAssetsClick">
			<div class="label">
				<i class="material-icons">playlist_add</i>
				<span class="text">Assets</span>
			</div>
		</div>
		<div class="menu-item" @click.stop="handleChangelogClick">
			<div class="label">
				<i class="material-icons">new_releases</i>
				<span class="text">Change Log</span>
			</div>
		</div>
		<a class="menu-item" href="https://github.com/flamestream/taction/releases">
			<div class="label github">
				<i class="material-icons">get_app</i>
				<span class="text">Download app</span>
			</div>
		</a>
		<a class="menu-item" href="https://github.com/flamestream/taction">
			<div class="label github">
				<i class="zmdi zmdi-github"></i>
				<span class="text">View on Github</span>
			</div>
		</a>
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
		handleChangelogClick(ev) {

			this.setActiveOverlay({id: 'changelog'});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.zmdi {
	font-size: 24px;
	padding: 2px;
}

</style>
