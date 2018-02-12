<template>
	<div class="view-previewer">
		<div class="asset-previewer" v-if="activeMenu === 'assets'">
			<img v-if="activeAsset && activeAsset.ext === 'png'" :src="activeAsset.url"/>
			<span v-else-if="activeAsset && activeAsset.ext === 'ttf'">The quick brown fox jumps over the lazy dog</span>
		</div>
		<div v-else class="previewer-layout">
			<div class="tabs">
				<div class="tab" @click="setActiveTab('ui')">UI</div>
				<div class="tab" @click="setActiveTab('code')">Code</div>
			</div>
			<div v-if="activeTab === 'ui'" class="content" @click="handleClick">
				<PreviewUI></PreviewUI>
			</div>
			<pre v-if="!activeTab || activeTab === 'code'">{{ layoutJson }}</pre>
		</div>
	</div>
</template>

<script>
import { mapState, mapGetters, mapActions } from 'vuex';
import PreviewUI from './PreviewUI';
export default {
	name: 'ViewPreview',
	components: {
		PreviewUI
	},
	data() {
		return {
			activeTab: undefined
		};
	},
	computed: {
		...mapGetters('layout', {
			layoutJson: 'json'
		}),
		...mapState('ui', ['activeMenu', 'activeItem', 'activeAsset'])
	},
	methods: {
		...mapActions({
			setActiveItem: 'ui/setActiveItem'
		}),
		handleClick(ev) {

			this.setActiveItem();
		},
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

.previewer-layout {
	background-color: yellow;
	flex: 1 1 auto;
	display: flex;
	flex-direction: column;
}

.content {
	flex: 1 1 auto;
	display: flex;
	align-items: center;
	justify-content: center;
	background-color: white;
}

</style>
