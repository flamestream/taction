<template>
	<div id="view-header">
		<span class="title">Taction Designer</span>
		<form>
			<button @click.prevent="handleResetButtonClick">Reset</button>
			<button @click.prevent="handleExportButtonClick">Export</button>
			<input type="file" accept=".json, .taction-bundle" @click="handleFileClick" @change="handleFileChange"/>
			{{status}}
		</form>
	</div>
</template>

<script>
import JSZip from 'jszip'
import FileSaver from 'file-saver'
export default {
	name: 'ViewHeader',
	data() {
		return {
			status: 'Please load a file'
		}
	},
	methods: {
		handleResetButtonClick(ev) {
			this.$store.dispatch({
				type: 'reset'
			});
		},
		async handleExportButtonClick(ev) {

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
		},
		handleFileClick(ev) {
			ev.target.value = null;
		},
		async handleFileChange(ev) {

			let files = ev.target.files; // FileList object
			if (!files.length) return;
			let file = files[0];
			// @TODO max file fize

			this.status = '';
			try {

				let layout;
				let zip;
				if (file.name.endsWith('.taction-bundle')) {
					[layout, zip] = await extractAssets(file);
				} else {
					layout = await getLayoutContent(file);
				}

				this.$store.dispatch({
					type: 'reset',
					data: { layout, zip }
				});
				this.status = 'Loaded file';

			} catch (e) {

				this.$store.dispatch({type: 'reset'});
				this.status = `Error loading file: ${e.message}`;
				console.error(e.stack);
			}
		}
	}
}

async function getLayoutContent(file) {

	let fnResolve;
	let promise = new Promise((resolve, reject) => { fnResolve = resolve });
	let reader = new FileReader();
	reader.onload = e => fnResolve(e.target.result);
	reader.readAsText(file);
	let data = await promise;
	data = sanitizeJson(data);
	let layout = JSON.parse(data);
	// @TODO Convert/schema test
	return layout;
}

async function extractAssets(file) {

	// Open zip
	let zip;
	try {
		zip = new JSZip();
		await zip.loadAsync(file);
	} catch (e) {
		throw new Error(`Error opening zip file: ${e.message}`);
	}

	// Open zip
	let data;
	try {
		data = await zip.file('layout.json').async('string');
	} catch (e) {
		throw new Error(`Error reading layout file: ${e.message}`);
	}

	// Read layout
	let layout;
	try {
		layout = sanitizeJson(data);
		layout = JSON.parse(layout);
		// @TODO Convert/schema test
	} catch (e) {
		throw new Error(`Error parsing layout: ${e.message}`);
	}

	// Sanitize
	for (let name in zip.files) {
		let file = zip.files[name];
		if (file.dir || !name.match(/\.(ttf|png)$/))
			zip.remove(name);
	}

	return [layout, zip];
}

function sanitizeJson(str) {

	return str.replace(/,(\s*\n(\s*))(}|])/g, '\n$2$3');
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
