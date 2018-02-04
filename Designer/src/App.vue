<template>
	<div id="app">
		<ViewFilePane/>
		<div id="workspace">
			<ViewProperties id="view_properties"/>
			<div id="view_selectors">
				<ViewTree id="view_tree"/>
				<ViewAssets id="view_assets"/>
			</div>
			<ViewPreview id="view_preview"/>
		</div>
	</div>
</template>

<script>
import ViewPreview from './components/ViewPreview'
import ViewTree from './components/ViewTree'
import ViewAssets from './components/ViewAssets'
import ViewProperties from './components/ViewProperties'
import ViewFilePane from './components/ViewFilePane'

export default {
	name: 'App',
	components: {
		ViewPreview,
		ViewTree,
		ViewAssets,
		ViewProperties,
		ViewFilePane
	},
	data() {
		return {
			activeItemId: undefined
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

#view_preview {
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
