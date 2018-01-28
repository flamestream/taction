<template>
	<div>
		<form>
			<input type="file" accept=".json, .taction-bundle" @change="handleFileChange"/>
			{{status}}
		</form>
	</div>
</template>

<script>
import JSZip from 'jszip'
export default {
	name: 'UploadPane',
	data () {
		return {
			status: 'Please load a file'
		}
	},
	methods: {
		handleFileChange: async function (evt) {
			let files = evt.target.files; // FileList object
			if (!files.length) return;
			let file = files[0];
			console.log(file);

			this.status = '';
			this.$emit('layoutLoad', {});
			try {
				let layout;
				let zip;
				if (file.name.endsWith('.taction-bundle')) {
					[layout, zip] = await extractLayoutFileContent(file);
				} else {
					layout = await getLayoutContent(file);
				}
				this.$emit('layoutLoad', {
					layout,
					zip
				});
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

async function extractLayoutFileContent(file) {

	var zip = new JSZip();
	await zip.loadAsync(file);
	let data = await zip.file('layout.json').async('string');
	data = sanitizeJson(data);
	let layout = JSON.parse(data);
	// @TODO Convert/schema test
	console.log(zip);
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
