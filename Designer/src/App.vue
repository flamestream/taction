<template>
	<div id="top">
		<Overlay></Overlay>
		<div id="app" :class="classNames">
			<ViewHeader></ViewHeader>
			<div id="workspace">
				<div id="section-menu">
					<ViewTree id="view-tree-menu"></ViewTree>
					<ViewSpecialMenu id="view-special-menu"></ViewSpecialMenu>
				</div>
				<div id="section-details">
					<ViewDetails></ViewDetails>
				</div>
				<div id="section-preview">
					<ViewPreview></ViewPreview>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import ViewPreview from './components/ViewPreview'
import ViewTree from './components/ViewTree'
import ViewSpecialMenu from './components/ViewSpecialMenu'
import ViewDetails from './components/ViewDetails'
import ViewHeader from './components/ViewHeader'
import Overlay from './components/Overlay'
import { mapState, mapActions } from 'vuex'
import firstTimerLayout from '@/layouts/first-timer'
export default {
	name: 'App',
	components: {
		ViewPreview,
		ViewTree,
		ViewSpecialMenu,
		ViewDetails,
		ViewHeader,
		Overlay
	},
	data() {
		return {
			activeItemId: undefined
		}
	},
	computed: {
		...mapState('ui', {
			activeOverlay: 'activeOverlay'
		}),
		classNames() {

			return {
				overlaid: !!this.activeOverlay
			}
		}
	},
	methods: {
		...mapActions({
			setActiveOverlay: 'ui/setActiveOverlay',
			reset: 'layout/reset'
		})
	},
	mounted() {

		let lastVisit = localStorage.lastVisit;

		if (!lastVisit) {
			this.setActiveOverlay({id: 'changelog'});
			this.reset({layout: firstTimerLayout});
		}

		// @TODO use async storage
		localStorage.lastVisit = Date.now();
	}
}
</script>

<style>

html, body {
	height: 100%;
	width: 100%;
	margin: 0;
	padding: 0;
	background-color: #1B2838;
}

a {
	color: inherit;
}
a:link {
	color: inherit;
	text-decoration: none;
}

input[type=text], input[type=number], select {
	border: 0;
	border-radius: 3px;
	padding: 2px 4px;
}

.button {
	background-color: #DDD;
	border: 0;
	border-radius: 3px;
	padding: 3px 6px;
	cursor: pointer;
	text-align: center;
	color: #111;
	transition: all 0.2s ease-out, left 0, top 0;
}

.button:hover {
	background-color: #EEE;
}

.button:active {
	position: relative;
	left: 1px;
	top: 1px;
}

.simplebar-scrollbar {
	background-color: #fff;
}

#top {
	height: 100%;
	font-family: 'Open Sans', sans-serif;
	user-select: none;
}

#app {
	height: 100%;
	color: #2c3e50;
	display: flex;
	flex-direction: column;
	transition: filter 0.5s;
}
#app.overlaid {
	filter: blur(4px);
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
	transition: all 0.2s ease-out;
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
	height: 120px;
	padding-bottom: 20px;
}

#section-details {
	display: flex;
	width: 300px;
	flex-shrink: 0;
	background-color: #1B2838;
	color: #ddd;
}

.section-details-scrollable::-webkit-scrollbar
{
	width: 5px;
	background-color: #FFFFFF33;
}

.section-details-scrollable::-webkit-scrollbar-thumb
{
	background-color: #FFFFFFAA;
	border-radius: 10px;
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

.add-button {
	background-color: #09B0EB;
	text-align: center;
	font-weight: 600;
	display: block;
	cursor: pointer;
	padding: 4px;
	margin: 0 10px 10px;
	border-radius: 3px;
	border: 0;
	color: #FFF;
}
.add-button:hover {
	background-color: #38beea;
}
.add-button:active {
	position: relative;
	top: 1px;
	left: 1px;
}

</style>
