import JSZip from 'jszip'
import FileSaver from 'file-saver'
import config from '@/config'

async function execute({layout, assets}) {

	let layoutData = JSON.stringify(layout, 4, 4);
	let layoutObj = JSON.parse(layoutData);

	// @TODO check if asset validation/filtering

	let ext, blob;
	if (assets && Object.keys(assets).length) {

		let zip = new JSZip();
		for (let name in assets) {

			let file = assets[name];
			await zip.file(name, file);
		}

		ext = 'taction-bundle';
		await zip.file('layout.json', layoutData);
		blob = await zip.generateAsync({type: 'blob'});

	} else {

		ext = 'json';
		blob = new Blob([layoutData], {type: 'application/json;charset=utf-8'});
	}

	let name = layoutObj.name || 'Untitled';
	name = name.substr(0, config.maximumExportFileNameLength - 1 - ext.length) + '.' + ext;
	FileSaver.saveAs(blob, name);
}

export default execute;
