<template>
	<div id="app">
		<UploadPane @layoutLoad="onLayoutLoad"/>
		<div id="workspace">
			<Properties id="view_properties" :loadedLayout="state && state.loadedLayout"/>
			<div id="view_selectors">
				<ViewTree id="view_tree" :state="state"/>
				<ViewAssets id="view_assets" :assets="state.assets"/>
			</div>
			<ViewPreview id="previewer" :state="state"/>
		</div>
	</div>
</template>

<script>
import ViewPreview from './components/ViewPreview'
import ViewTree from './components/ViewTree'
import ViewAssets from './components/ViewAssets'
import Properties from './components/Properties'
import UploadPane from './components/UploadPane'

export default {
	name: 'App',
	components: {
		ViewPreview,
		ViewTree,
		ViewAssets,
		Properties,
		UploadPane
	},
	data () {
		return {
			state: {
				loadedLayout: {},
				assets: {}
			}
		}
	},
	methods: {
		onLayoutLoad: function(data) {
			this.state.loadedLayout = data.layout;
			this.state.assets = data.zip.files;
		}
	}
}

</script>

<style>

html, body {
	height: 100%;
	width: 100%;
	margin: 0;
	padding: 0;
}

#app {
	height: 100%;
	font-family: 'Avenir', Helvetica, Arial, sans-serif;
	color: #2c3e50;
	display: flex;
	flex-direction: column;
}
#workspace {
	display: flex;
	flex-direction: row;
	justify-content: center;
	flex-grow: 1;
}

#previewer {
	flex-grow: 1;
	overflow: auto;
}

#view_selectors {
	min-width: 200px;
	overflow-y: auto;
	display: flex;
	flex-direction: column;
}

#view_tree {
	flex-grow: 2;
	overflow-y: auto;
}

#view_assets {
	flex-grow: 1;
	overflow-y: auto;
}

#view_properties {
	min-width: 300px;
	overflow-y: auto;
}
</style>
