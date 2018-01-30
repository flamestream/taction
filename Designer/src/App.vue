<template>
	<div id="app">
		<ViewFilePane @fileLoad="onFileLoad" @exportButtonClick="onExportButtonClick"/>
		<div id="workspace">
			<ViewProperties id="view_properties" :layout="layout"/>
			<div id="view_selectors">
				<ViewTree id="view_tree" :layout="layout"/>
				<ViewAssets id="view_assets" :assets="assets"/>
			</div>
			<ViewPreview id="view_preview" :state="state"/>
		</div>
	</div>
</template>

<script>
import FileSaver from 'file-saver'

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
			layout: {},
			zip: undefined
		}
	},
	computed: {
		assets() {
			let zip = this.zip;
			return zip && zip.files;
		},
		state() {
			let { layout, zip } = this;
			return { layout, zip };
		}
	},
	methods: {
		resetState(layout, zip) {

			// Reset layout
			this.layout = layout || {};

			// Clean up memory
			if (this.zip) {
				this.zip.remove('/');
			}

			// Update zip
			this.zip = zip;
		},
		onFileLoad(data) {

			this.resetState(data.layout, data.zip);
		},
		async onExportButtonClick() {

			let zip = this.state.zip;
			let layout = this.state.layout || {};
			let data = JSON.stringify(layout, 4, 4);

			let ext, blob;
			if (zip && Object.keys(zip).length) {

				ext = 'taction-bundle';
				await zip.file('layout.json', data);
				blob = await zip.generateAsync({type: 'blob'});

			} else {

				ext = 'json';
				blob = new Blob([data], {type: 'application/json;charset=utf-8'});
			}

			let name = layout.name || 'Untitled';
			name = name.substr(0, 63 - ext.length) + '.' + ext;
			FileSaver.saveAs(blob, name);
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
