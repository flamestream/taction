<template>
	<div :class="{ 'tree-node': true, active: activeMenu === 'assets' }" @click.stop="handleClick">
		<div class="label">
			<span class="text">Assets</span>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex';
export default {
	name: 'ViewAssets',
	computed: {
		...mapState(['activeMenu'])
	},
	methods: {
		handleClick(ev) {

			this.$store.dispatch({
				type: 'setActiveMenu',
				id: 'assets'
			});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
