<template>
	<div class="area-workspace">
		<div class="asset-previewer" v-if="activeMenu === 'assets'">
			<div class="asset-container">
				<img v-if="activeAsset && activeAsset.ext === 'png'" :src="activeAsset.url"/>
				<span v-else-if="activeAsset && activeAsset.ext === 'ttf'" :style="assetFontStyle">The quick brown fox jumps over the lazy dog</span>
			</div>
		</div>
		<div v-else class="area-tabbed-workspace">
			<div class="tabs">
				<div :class="getTabClassNames('preview', true)" @click="setActiveTab('preview')">
					<i class="material-icons">dashboard</i>
					Preview
				</div>
				<div :class="getTabClassNames('test')" @click="setActiveTab('test')">
					<i class="material-icons">view_stream</i>
					Test
				</div>
				<div :class="getTabClassNames('code')" @click="setActiveTab('code')">
					<i class="material-icons">code</i>
					Code
				</div>
			</div>
			<div class="area-tabbed-workspace-content">
				<code v-if="activeTab === 'code'" ref="code">{{ layoutJson }}</code>
				<TabContentTest v-if="activeTab === 'test'" class="tab-content"></TabContentTest>
				<TabContentPreview v-if="!activeTab || activeTab === 'preview'" class="tab-content"></TabContentPreview>
			</div>
		</div>
	</div>
</template>

<script>
import { mapState, mapGetters, mapActions } from 'vuex';
import TabContentPreview from './TabContentPreview';
import TabContentTest from './TabContentTest';
export default {
	name: 'ViewPreview',
	components: {
		TabContentPreview,
		TabContentTest
	},
	data() {
		return {
			activeTab: undefined,
			assetFontStyle: { fontFamily: '"./Active Font"' }
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
		getTabClassNames(name, isDefault) {

			/* eslint no-mixed-operators: 0 */
			return {
				tab: true,
				active: !this.activeTab && isDefault || this.activeTab === name
			}
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
	flex: 1 1 auto;
	overflow: auto;
	background-color: #fff;
	vertical-align: middle;
	display: flex;
	align-items: center;
	font-size: 32px;
}

.asset-container {
	margin: auto;
}

.asset-container > * {
	margin: 10px;
}

.asset-previewer img {
	box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.3);
	background-color: red;
	background: url('../assets/transparent-bg.jpg');
}

.asset-previewer span {
	padding: 0 1em;
	display: inline-block;
}

.tabs {
	background-color: #2D3B4B;
	padding: 8px 12px 0;
	display: flex;
	flex-shrink: 0;
}

.tab {
	padding: 4px 12px;
	display: inline-block;
	border-bottom: 1px solid #2D3B4B;
	display: flex;
	align-items: center;
	margin-right: 8px;
	background-color: #1B2838;
	color: #FFFFFFDD;
	font-size: 14px;
	border-radius: 2px 2px 0 0;
	cursor: pointer;
	transition: none;
}
.tab:hover {
	color: #FFF;
}

.tab.active {
	background-color: #FFF;
	border-bottom-color: transparent;
	color: #38BEEA;
}

.tab i {
	margin-right: 4px;
	transition: none;
}

code {
	margin: 12px;
	flex: 1 1 auto;
	user-select: text;
	white-space: pre;
}

.area-tabbed-workspace {
	flex: 1 1 auto;
	display: flex;
	flex-direction: column;
	overflow: auto;
}

.area-tabbed-workspace-content {
	flex: 1 1 auto;
	display: flex;
	overflow: auto;
}

.tab-content {
	flex: 1 1 auto;
	overflow: auto;
}

</style>
