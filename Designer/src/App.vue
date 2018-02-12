<template>
	<div id="app">
		<ViewHeader></ViewHeader>
		<div id="workspace">
			<div id="section-menu">
				<ViewTree id="view-tree-menu"></ViewTree>
				<ViewSpecialMenu id="view-special-menu"></ViewSpecialMenu>
			</div>
			<div id="section-properties" data-simplebar>
				<ViewProperties></ViewProperties>
			</div>
			<div id="section-preview">
				<ViewPreview></ViewPreview>
			</div>
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

.simplebar-scrollbar {
	background-color: #fff;
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

#workspace {
	display: flex;
	flex-direction: row;
	justify-content: center;
	flex-grow: 1;
}

#section-menu {
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
	overflow: auto;
	display: flex;
	background-color: #FFF;
}

.menu-item {
	cursor: pointer;
}

/*.menu-item .icon {
	display: inline-block;
	border: 1px solid red;
	width: 24px;
	height: 24px;
	vertical-align: middle;
	margin-right: 0.5em;
}*/

#view-tree-menu {
	padding-top: 10px;
	flex-grow: 1;
}

#section-menu .menu-item {
	line-height: 40px;
}

#section-menu .menu-item .spacer {
	width: 10px;
}

#section-menu .menu-item > .label {
	border-left: 3px transparent solid;
	padding-left: 16px;
	display: flex;
	align-items: center;
}

#section-menu .menu-item > .label > i {
	margin-right: 8px;
}

#section-menu .menu-item > .label > .command {
	font-size: 16px;
	font-weight: 600;
}

#section-menu .menu-item.active {
	box-shadow: 4px 0px 10px 0px rgba(0, 0, 0, 0.5);
	background-color: #1B283877;
}

#section-menu .menu-item > .label:hover {
	background-color: #1B283899;
}
#section-menu .menu-item.active > .label {
	color: #09B0EB;
	border-left-color: inherit;
	background-color: #1B2838;
}
#section-menu .menu-item.active > .label:hover {
	color: #5bc6ea;
}

#view-special-menu {
	overflow-y: auto;
	padding-bottom: 20px;
}

#section-properties {
	display: flex;
	width: 300px;
	flex-shrink: 0;
	background-color: #1B2838;
	color: #ddd;
}

.section-properties-content {
	margin: 12px 0;
}

.view-properties {
	margin: 0 10px;
}

.view-previewer {
	flex: 1 1 auto;
	display: flex;
}

input[type=number] {
	text-align: end;
}

input[type=number]::-webkit-outer-spin-button,
input[type=number]::-webkit-inner-spin-button {
	-webkit-appearance: none;
	appearance: none;
	margin: 0;
}

</style>
