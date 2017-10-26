const fs = require("fs-extra");

const converterRegistry = {};

function main(args) {

	const file = args[2];
	let config = fs.readJsonSync(file);
	if (convert(config)) {
		fs.outputJsonSync(file, config, {
			spaces: '\t'
		});
	} else {
		console.log("Nothing to convert");
	}
}

function convert(config) {

	let fnConvert = getConverter(config);
	while (fnConvert) {

		let currentVersion = config.version || 1;
		fnConvert(config);
		config.version = fnConvert.to;
		console.log(`Converted from version ${currentVersion} to ${config.version}`);
		fnConvert = getConverter(config) || false;
	}

	return fnConvert === false;
}

function registerConverter(from, to, fn) {

	if (!(from in converterRegistry))
		converterRegistry[from] = {};

	fn.from = from;
	fn.to = to;
	converterRegistry[from][to] = fn;
}

function getConverter(config) {

	let version = config.version || 1;
	let candidateConverters = converterRegistry[version]
	if (!candidateConverters)
		return;

	let toVersions = Object.keys(candidateConverters);
	let maxVersion = Math.max(...toVersions);

	return candidateConverters[maxVersion];
}

registerConverter(2, 3, function(config) {

	convertItems(config.items);

	function convertItems(items) {

		if (!items)
			return;

		for (let k in items) {
			let item = items[k];

			if (item.type === 'pivot')
				continue;

			let baseStyle = item['base-style'] || {};
			for (let k in item) {

				if (~['size', 'type', 'command', 'base-style'].indexOf(k))
					continue;

				let v = item[k];
				delete item[k];
				baseStyle[k] = v;
			}

			let textStyle = item['text-style']
			if (textStyle) {

				if (textStyle['size']) {

					textStyle['font-size'] = textStyle['size'];
					delete textStyle['size'];
				}
			}

			if (Object.keys(baseStyle).length)
				item['base-style'] = baseStyle;
		}

		convertItems(items.items);
	}
});

registerConverter(3, 4, function(config) {

	config['default-button-style'] = {}
	if (config['default-base-style']) {

		config['default-button-style']['base'] = config['default-base-style'];
		delete config['default-base-style'];
	}

	if (config['default-active-style']) {

		config['default-button-style']['active'] = config['default-active-style'];
		delete config['default-active-style'];
	}

	if (!Object.keys(config['default-button-style']).length)
		delete config['default-button-style'];

	convertItems(config.items);

	function convertItems(items) {

		if (!items)
			return;

		for (let k in items) {

			let item = items[k];

			if (item.type === 'pivot')
				continue;

			let style = {}
			if (item['base-style']) {

				style['base'] = item['base-style'];
				delete item['base-style'];
			}

			if (item['active-style']) {

				style['active'] = item['active-style'];
				delete item['active-style'];
			}

			if (Object.keys(style).length)
				item['style'] = style;

			convertItems(item.items);
		}
	}
});

main(process.argv);
