<template>
	<div>
		<div>
			<div class="tab" @click="setActiveTab('ui')">UI</div>
			<div class="tab" @click="setActiveTab('code')">Code</div>
		</div>
		<div v-if="activeTab === 'ui'">{{ layout }}</div>
		<pre v-if="!activeTab || activeTab === 'code'">{{ layout }}</pre>
	</div>
</template>

<script>
import { mapState } from 'vuex'
export default {
	name: 'ViewPreview',
	data() {
		return {
			activeTab: undefined
		};
	},
	computed: {
		...mapState(['layout'])
	},
	methods: {
		setActiveTab(name) {
			this.activeTab = name;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.tab {
	display: inline-block;
}
pre {
	user-select: text;
}
</style>
