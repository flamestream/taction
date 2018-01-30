<template>
	<div>
		<form>
			<button @click="handleExportButtonClick">Export</button>
			<input type="file" accept=".json, .taction-bundle" @change="handleFileChange"/>
			{{status}}
		</form>
	</div>
</template>

<script>
import JSZip from 'jszip'
export default {
	name: 'UploadPane',
	data() {
		return {
			status: 'Please load a file'
		}
	},
	methods: {
		handleExportButtonClick(ev) {
			ev.preventDefault();
			this.$emit('exportButtonClick');
		},
		async handleFileChange(evt) {
			let files = evt.target.files; // FileList object
			if (!files.length) return;
			let file = files[0];
			console.log(file);
			// @TODO max file fize

			this.status = '';
			this.$emit('fileLoad', {});
			try {
				let layout;
				let zip;
				if (file.name.endsWith('.taction-bundle')) {
					[layout, zip] = await extractAssets(file);
				} else {
					layout = await getLayoutContent(file);
				}
				this.$emit('fileLoad', { layout, zip });
				this.status = 'Loaded file';
			} catch (e) {
				this.status = `Error loading file: ${e.message}`;
			}
		}
	}
}

async function getLayoutContent (file) {

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
div {
	background-color: #00FFFF55;
}
</style>
