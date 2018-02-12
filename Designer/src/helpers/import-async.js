import JSZip from 'jszip'
import filesize from 'filesize'
import fileTypeAsync from '@/helpers/file-type-async'
import fileReaderAsync from '@/helpers/file-reader-async'
import config from '@/config'

async function execute({file}) {

	// Type check
	if (!(file instanceof File))
		throw new Error('Invalid file object');

	// File size check
	let { maximumImportFileSize } = config;
	if (file.size > maximumImportFileSize)
		throw new Error(`File size exceeds maximum supported size of ${filesize(maximumImportFileSize)}`)

	let layout, zip;

	// Type check
	let type = await fileTypeAsync(file) || {};
	if (type.mime === 'application/zip') {

		[layout, zip] = await extractAssetsAsync(file);

	} else if (file.name.endsWith('.json')) {

		layout = await getLayoutContentAsync(file);

	} else {

		throw new Error(`Unsupported file format`);
	}

	let { supportedLayoutVersion } = config;
	if (layout.version !== supportedLayoutVersion)
		throw new Error(`Unsupported layout version (Input ${layout.version} vs. supported ${supportedLayoutVersion})`);

	return {
		layout,
		zip
	};
}

async function getLayoutContentAsync(file) {

	let data = await fileReaderAsync(file, {read: 'text'});
	let layout = parseData(data);
	return layout;
}

async function extractAssetsAsync(file) {

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
		let f = zip.file('layout.json');
		if (!f)
			throw new Error('Could not find layout.json');

		data = await f.async('string');

	} catch (e) {
		throw new Error(`Error reading layout file: ${e.message}`);
	}

	// Read layout
	let layout;
	try {
		layout = parseData(data);
	} catch (e) {
		throw new Error(`Error parsing layout: ${e.message}`);
	}

	return [layout, zip];
}

function parseData(data) {

	data = sanitizeJson(data);
	data = JSON.parse(data);
	// @TODO Convert/schema test
	return data;
}

function sanitizeJson(str) {

	return str.replace(/,(\s*\n(\s*))(}|])/g, '\n$2$3');
}

export default execute;
