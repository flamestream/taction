<template>
	<div id="view-header">
		<span class="title">{{ title }}</span>
		<span class="button" @click.prevent="handleResetButtonClick">
			<i class="material-icons">clear</i>
			Reset
		</span>
		<span class="input-file button">
			<input type="file" accept=".json, .taction-bundle" id="file-layout" @click="handleFileClick" @change="handleFileChange"/>
			<label for="file-layout">
				<i class="material-icons">file_upload</i>
				Import
			</label>
		</span>
		<span class="button" @click.prevent="handleExportButtonClick">
			<i class="material-icons">file_download</i>
			Export
		</span>
	</div>
</template>

<script>
import config from '../config'
import importAsync from '@/helpers/import-async'
import exportAsync from '@/helpers/export-async'
export default {
	name: 'ViewHeader',
	data() {
		return {
			status: 'Please load a file'
		}
	},
	computed: {
		title() {

			return `${config.name}`
		}
	},
	methods: {
		handleResetButtonClick(ev) {

			this.$store.dispatch({
				type: 'reset'
			});
		},
		async handleExportButtonClick(ev) {

			await exportAsync({
				layout: this.$store.state.layout.layout,
				assets: this.$store.state.assets.registry
			});
		},
		handleFileClick(ev) {
			ev.target.value = null;
		},
		async handleFileChange(ev) {

			let files = ev.target.files; // FileList object
			if (!files.length) return;

			let layout, zip, error;
			this.status = '';
			try {

				let file = files[0];
				let imported = await importAsync({file});
				layout = imported.layout;
				zip = imported.zip;

			} catch (e) {

				error = `Error loading file: ${e.message}`;
				console.error(e.stack);
			}

			this.$store.dispatch({
				type: 'reset',
				layout,
				zip
			});
			this.status = error || 'Loaded file';
		}
	}
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

#view-header {
	height: 64px;
	flex-shrink: 0;
	display: flex;
	align-items: center;
}

.title {
	flex: 1 1 auto;
	padding-left: 24px;
	font-size: 24px
}

.button {
	cursor: pointer;
	margin-right: 16px;
	padding: 4px 8px;
	color: #FFFFFFDD;
}
.button:hover {
	color: #FFFFFF;
}
.button:active {
	position: relative;
	left: 1px;
	top: 1px;
}

.button, .button > label {
	display: flex;
	align-items: center;
}

.button i {
	margin-right: 4px;
}

.input-file input {
	display: none;
}

</style>
