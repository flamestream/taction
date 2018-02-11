<template>
	<div id="view-header">
		<span class="title">{{ title }}</span>
		<form>
			<button @click.prevent="handleResetButtonClick">Reset</button>
			<button @click.prevent="handleExportButtonClick">Export</button>
			<input type="file" accept=".json, .taction-bundle" @click="handleFileClick" @change="handleFileChange"/>
			{{status}}
		</form>
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

			return `${config.name} ${config.version}`
		}
	},
	methods: {
		handleResetButtonClick(ev) {

			this.$store.dispatch({
				type: 'reset',
				layout: config.defaultLayout
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
</style>
