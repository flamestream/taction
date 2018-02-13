<template>
	<div class="view-previewer">
		<div class="asset-previewer" v-if="activeMenu === 'assets'">
			<img v-if="activeAsset && activeAsset.ext === 'png'" :src="activeAsset.url"/>
			<span v-else-if="activeAsset && activeAsset.ext === 'ttf'">The quick brown fox jumps over the lazy dog</span>
		</div>
		<div v-else class="previewer-layout">
			<div class="tabs">
				<div :class="getTabClassNames('ui', true)" @click="setActiveTab('ui')">
					<i class="material-icons">dashboard</i>
					Visual Interface
				</div>
				<div :class="getTabClassNames('code')" @click="setActiveTab('code')">
					<i class="material-icons">code</i>
					Code
				</div>
			</div>
			<pre v-if="activeTab === 'code'" ref="code" class="line-numbers language-javascript"><code>{{ layoutJson }}</code></pre>
			<div v-if="!activeTab || activeTab === 'ui'" class="tab-content" @click="handleClick">
				<PreviewUI></PreviewUI>
			</div>
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
	/*justify-content: safe center;*/
	font-family: './Active Font';
	font-size: 32px;
}

.asset-previewer > * {
	margin: auto;
}

.asset-previewer img {
	box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.3);
	background-color: red;
	background: url('../assets/transparent-bg.jpg');
}

.asset-previewer span {
	padding: 0 1em;
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
}

pre {
	overflow: auto;
	margin: 0;
	padding: 1em;
}

code {
	user-select: text;
}

.previewer-layout {
	flex: 1 1 auto;
	display: flex;
	flex-direction: column;
}

.tab-content {
	flex: 1 1 auto;
	display: flex;
	/*align-items: center;*/
	/*justify-content: safe center;*/
	padding: 10px;
	overflow: auto;
}

</style>
