<template>
	<div id="app">
		<ViewHeader></ViewHeader>
		<div id="workspace">
			<div id="view-selectors">
				<ViewTree id="view_tree"/>
				<ViewAssets id="view_assets"></ViewAssets>
			</div>
			<ViewProperties id="view-properties"></ViewProperties>
			<ViewPreview id="view_preview"></ViewPreview>
		</div>
	</div>
</template>

<script>
import ViewPreview from './components/ViewPreview'
import ViewTree from './components/ViewTree'
import ViewAssets from './components/ViewAssets'
import ViewProperties from './components/ViewProperties'
import ViewHeader from './components/ViewHeader'

export default {
	name: 'App',
	components: {
		ViewPreview,
		ViewTree,
		ViewAssets,
		ViewProperties,
		ViewHeader
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

.subtle-scroll {
	overflow-y: overlay !important;
}

.subtle-scroll::-webkit-scrollbar-track {
	background-color: transparent;
}

.subtle-scroll::-webkit-scrollbar {
	width: 8px;
	background-color: transparent;
}

.subtle-scroll::-webkit-scrollbar-thumb {
	border-radius: 10px;
	background-color: #FFFFFF33;
}

#app {
	height: 100%;
	font-family: 'Avenir', Helvetica, Arial, sans-serif;
	color: #2c3e50;
	display: flex;
	flex-direction: column;
	user-select: none;
}

#view-header {
	background-color: #1B2838;
	color: #FFF;
}

#view-header > .title {

	line-height: 30px;
	padding-left: 32px;
}

#workspace {
	display: flex;
	flex-direction: row;
	justify-content: center;
	flex-grow: 1;
}

#view-selectors {
	width: 200px;
	overflow-y: auto;
	display: flex;
	flex-direction: column;
	text-overflow: ellipsis;
	flex-shrink: 0;
	background-color: #2D3B4B;
	background: linear-gradient(to bottom, #2D3B4B 0%,#273D4E 100%);
	color: #EEE;
}

#view_preview {
	flex-grow: 1;
	overflow: auto;
}

#view_tree {
	padding-top: 10px;
	flex-grow: 2;
}

.tree-node {
	line-height: 40px;
}

.tree-node .spacer {
	width: 10px;
}

.tree-node > .label {
	border-left: 3px transparent solid;
	padding-left: 16px;
}

.tree-node.active {
	box-shadow: 4px 0px 10px 0px rgba(0, 0, 0, 0.5);
	background-color: #1B283877;
}

.tree-node.active > .label {
	border-left-color: inherit;
	background-color: #1B2838;
}

#view_assets {
	flex-grow: 1;
	overflow-y: auto;
}

#view-properties {
	display: inline-block;
	min-width: 300px;
	overflow-y: auto;
	flex-shrink: 0;
	background-color: #1B2838;
	color: #ddd;
	padding: 12px 0;
}
#view-properties > * {
	margin-left: 16px;
	margin-right: 16px;
}

input[type=number] {
	text-align: end;
}

input[type=number]::-webkit-outer-spin-button,
input[type=number]::-webkit-inner-spin-button {
	-webkit-appearance: none;
	margin: 0;
}

</style>
