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
import { mapActions } from 'vuex'
export default {
	name: 'ViewHeader',
	computed: {
		title() {

			return `${config.name}`
		}
	},
	methods: {
		...mapActions({
			setErrorMsg: 'ui/setErrorMsg',
			setActiveOverlay: 'ui/setActiveOverlay'
		}),
		handleResetButtonClick(ev) {

			this.setActiveOverlay({id: 'reset'});

			// this.$store.dispatch({
			// 	type: 'reset'
			// });
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

			if (error) {

				this.setErrorMsg({msg: error});
			}
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
	box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.2);
	position: relative;
	z-index: 10;
}

.title {
	flex: 1 1 auto;
	padding-left: 24px;
	font-size: 24px
}

.button {
	cursor: pointer;
	margin-right: 16px;
	padding: 8px 12px;
	padding-right: 16px;
	color: #FFFFFFDD;
	border-radius: 3px;
	background-color: inherit;
	transition: all 0.2s ease-out;
}
.button:hover {
	color: #FFFFFF;
	background-color: #2D3B4B;
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
