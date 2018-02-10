<template>
	<div class="asset-previewer" v-if="activeMenu === 'assets'">
		<img v-if="activeAsset && activeAsset.ext === 'png'" :src="activeAsset.url"/>
		<span v-else-if="activeAsset && activeAsset.ext === 'ttf'">The quick brown fox jumps over the lazy dog</span>
	</div>
	<div v-else>
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
		...mapState('layout', ['layout']),
		...mapState('ui', ['activeMenu', 'activeItem', 'activeAsset'])
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

.asset-previewer {
	overflow: auto;
	background-color: #fff;
	vertical-align: middle;
	display:flex;
	align-items: center;
	justify-content: center;
	font-size: 32px;
}

.asset-previewer img {
	box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.3);
	background-color: red;
	background: url('../assets/transparent-bg.jpg');
}

.asset-previewer span {
	margin: 0 1em;
}

.tab {
	display: inline-block;
}

pre {
	user-select: text;
}

</style>
