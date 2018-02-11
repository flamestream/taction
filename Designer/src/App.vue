<template>
	<div id="app">
		<ViewHeader></ViewHeader>
		<div id="workspace">
			<div id="view-menu">
				<ViewTree id="view-tree-menu"/>
				<ViewSpecialMenu id="view-special-menu"></ViewSpecialMenu>
			</div>
			<ViewProperties id="view-properties"></ViewProperties>
			<ViewPreview id="section-preview"></ViewPreview>
		</div>
	</div>
</template>

<script>
import ViewPreview from './components/ViewPreview'
import ViewTree from './components/ViewTree'
import ViewSpecialMenu from './components/ViewSpecialMenu'
import ViewProperties from './components/ViewProperties'
import ViewHeader from './components/ViewHeader'

export default {
	name: 'App',
	components: {
		ViewPreview,
		ViewTree,
		ViewSpecialMenu,
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
	font-family: 'Open Sans', sans-serif;
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

#view-menu {
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

#section-preview {
	flex-grow: 1;
	font-family: 'Active Font';
	overflow: auto;
}

.menu-item {
	cursor: pointer;
}

#view-tree-menu {
	padding-top: 10px;
	flex-grow: 1;
}

#view-menu .menu-item {
	line-height: 40px;
}

#view-menu .menu-item .spacer {
	width: 10px;
}

#view-menu .menu-item > .label {
	border-left: 3px transparent solid;
	padding-left: 16px;
}

#view-menu .menu-item > .label > .command {
	font-size: 16px;
	font-weight: 600;
}

#view-menu .menu-item.active {
	box-shadow: 4px 0px 10px 0px rgba(0, 0, 0, 0.5);
	background-color: #1B283877;
}

#view-menu .menu-item:hover > .label {
	background-color: #1B283899;
}
#view-menu .menu-item.active > .label {
	color: #09B0EB;
	border-left-color: inherit;
	background-color: #1B2838;
}
#view-menu .menu-item.active:hover > .label {
	color: #5bc6ea;
}

#view-special-menu {
	overflow-y: auto;
	padding-bottom: 20px;
}

#view-properties {
	display: inline-block;
	width: 300px;
	overflow-y: auto;
	flex-shrink: 0;
	background-color: #1B2838;
	color: #ddd;
	padding: 12px 0;
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
