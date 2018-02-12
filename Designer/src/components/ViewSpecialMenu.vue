<template>
	<div>
		<div :class="{ 'menu-item': true, active: activeMenu === 'assets' }" @click.stop="handleClick">
			<div class="label">
				<i class="material-icons">playlist_add</i>
				<span class="text">Assets</span>
			</div>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex';
export default {
	name: 'ViewAssets',
	computed: {
		...mapState('ui', ['activeMenu'])
	},
	methods: {
		handleClick(ev) {

			this.$store.dispatch({
				type: 'ui/setActiveMenu',
				id: 'assets'
			});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
