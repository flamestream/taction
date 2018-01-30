class Color {

	constructor(r, g, b, a) {

		if (arguments.length === 1) {

			let color;
			if (typeof r === 'string')
				color = Color.fromHex(r) || Color.fromName(r);
			else if (r instanceof Object)
				color = r;


			if (color) {

				r = color.red;
				g = color.green;
				b = color.blue;
				a = color.alpha;
			}
		}

		// Range: 0-255
		this.red = toValidNumber(r);
		this.green = toValidNumber(g);
		this.blue = toValidNumber(b);
		this.alpha = toValidNumber(a, 255);
	}

	_computeHex() {

		let { red, green, blue, alpha } = this;
		red = toValidNumber(red);
		green = toValidNumber(green);
		blue = toValidNumber(blue);
		alpha = toValidNumber(alpha, 255);

		let hexRed = '00' + red.toString(16);
		let hexGreen = '00' + green.toString(16);
		let hexBlue = '00' + blue.toString(16);
		let hexAlpha = '00' + alpha.toString(16);

		hexRed = hexRed.slice(-2);
		hexGreen = hexGreen.slice(-2);
		hexBlue = hexBlue.slice(-2);
		hexAlpha = hexAlpha.slice(-2);

		return { hexRed, hexGreen, hexBlue, hexAlpha };
	}

	getHex() {

		let { hexRed, hexGreen, hexBlue, hexAlpha } = this._computeHex();
		return `#${hexRed}${hexGreen}${hexBlue}${hexAlpha}`.toUpperCase();
	}

	getSimpleHex() {

		let { hexRed, hexGreen, hexBlue } = this._computeHex();
		return `#${hexRed}${hexGreen}${hexBlue}`.toUpperCase();
	}

	getWpfHex() {

		let { hexRed, hexGreen, hexBlue, hexAlpha } = this._computeHex();
		return `#${hexAlpha}${hexRed}${hexGreen}${hexBlue}`.toUpperCase();
	}
};

Color.fromHex = function(hex) {

	hex = sanitizeHex(hex);
	if (!hex)
		return;

	if (hex.length !== 8)
		hex = hex + 'FF';

	this.red = parseInt(hex[0] + hex[1], 16);
	this.green = parseInt(hex[2] + hex[3], 16);
	this.blue = parseInt(hex[4] + hex[5], 16);

	return new Color(
		parseInt(hex[0] + hex[1], 16),
		parseInt(hex[2] + hex[3], 16),
		parseInt(hex[4] + hex[5], 16),
		parseInt(hex[6] + hex[7], 16)
	);
};

Color.fromWpfHex = function(hex) {

	hex = sanitizeHex(hex);
	if (!hex)
		return;

	if (hex.length !== 8)
		hex = 'FF' + hex;

	return new Color(
		parseInt(hex[2] + hex[3], 16),
		parseInt(hex[4] + hex[5], 16),
		parseInt(hex[6] + hex[7], 16),
		parseInt(hex[0] + hex[1], 16)
	);
};

Color.fromName = function(name) {

	name = name.toLowerCase();
	let hex = namedColor[name];
	return hex && Color.fromHex(hex);
}

function toValidNumber(n, defaultValue = 0) {

	return Number.isInteger(n)
		? Math.max(0, Math.min(255, n))
		: defaultValue;
}

function sanitizeHex(hex) {

	if (typeof hex !== 'string')
		return;

	if (!hex.startsWith('#'))
		return;

	hex = hex.substr(1);

	if (hex.length === 3 || hex.length === 4)
		hex = hex.replace(/./g, '$&$&');

	if (hex.length !== 6 && hex.length !== 8)
		return;

	return hex;
}

let namedColor = {
	aliceblue: '#F0F8FF',
	antiquewhite: '#FAEBD7',
	aqua: '#00FFFF',
	aquamarine: '#7FFFD4',
	azure: '#F0FFFF',
	beige: '#F5F5DC',
	bisque: '#FFE4C4',
	black: '#000000',
	blanchedalmond: '#FFEBCD',
	blue: '#0000FF',
	blueviolet: '#8A2BE2',
	brown: '#A52A2A',
	burlywood: '#DEB887',
	cadetblue: '#5F9EA0',
	chartreuse: '#7FFF00',
	chocolate: '#D2691E',
	coral: '#FF7F50',
	cornflowerblue: '#6495ED',
	cornsilk: '#FFF8DC',
	crimson: '#DC143C',
	cyan: '#00FFFF',
	darkblue: '#00008B',
	darkcyan: '#008B8B',
	darkgoldenrod: '#B8860B',
	darkgray: '#A9A9A9',
	darkgreen: '#006400',
	darkkhaki: '#BDB76B',
	darkmagenta: '#8B008B',
	darkolivegreen: '#556B2F',
	darkorange: '#FF8C00',
	darkorchid: '#9932CC',
	darkred: '#8B0000',
	darksalmon: '#E9967A',
	darkseagreen: '#8FBC8B',
	darkslateblue: '#483D8B',
	darkslategray: '#2F4F4F',
	darkturquoise: '#00CED1',
	darkviolet: '#9400D3',
	deeppink: '#FF1493',
	deepskyblue: '#00BFFF',
	dimgray: '#696969',
	dodgerblue: '#1E90FF',
	firebrick: '#B22222',
	floralwhite: '#FFFAF0',
	forestgreen: '#228B22',
	fuchsia: '#FF00FF',
	gainsboro: '#DCDCDC',
	ghostwhite: '#F8F8FF',
	gold: '#FFD700',
	goldenrod: '#DAA520',
	gray: '#808080',
	green: '#008000',
	greenyellow: '#ADFF2F',
	honeydew: '#F0FFF0',
	hotpink: '#FF69B4',
	indianred: '#CD5C5C',
	indigo: '#4B0082',
	ivory: '#FFFFF0',
	khaki: '#F0E68C',
	lavender: '#E6E6FA',
	lavenderblush: '#FFF0F5',
	lawngreen: '#7CFC00',
	lemonchiffon: '#FFFACD',
	lightblue: '#ADD8E6',
	lightcoral: '#F08080',
	lightcyan: '#E0FFFF',
	lightgoldenrodyellow: '#FAFAD2',
	lightgray: '#D3D3D3',
	lightgreen: '#90EE90',
	lightpink: '#FFB6C1',
	lightsalmon: '#FFA07A',
	lightseagreen: '#20B2AA',
	lightskyblue: '#87CEFA',
	lightslategray: '#778899',
	lightsteelblue: '#B0C4DE',
	lightyellow: '#FFFFE0',
	lime: '#00FF00',
	limegreen: '#32CD32',
	linen: '#FAF0E6',
	magenta: '#FF00FF',
	maroon: '#800000',
	mediumaquamarine: '#66CDAA',
	mediumblue: '#0000CD',
	mediumorchid: '#BA55D3',
	mediumpurple: '#9370DB',
	mediumseagreen: '#3CB371',
	mediumslateblue: '#7B68EE',
	mediumspringgreen: '#00FA9A',
	mediumturquoise: '#48D1CC',
	mediumvioletred: '#C71585',
	midnightblue: '#191970',
	mintcream: '#F5FFFA',
	mistyrose: '#FFE4E1',
	moccasin: '#FFE4B5',
	navajowhite: '#FFDEAD',
	navy: '#000080',
	oldlace: '#FDF5E6',
	olive: '#808000',
	olivedrab: '#6B8E23',
	orange: '#FFA500',
	orangered: '#FF4500',
	orchid: '#DA70D6',
	palegoldenrod: '#EEE8AA',
	palegreen: '#98FB98',
	paleturquoise: '#AFEEEE',
	palevioletred: '#DB7093',
	papayawhip: '#FFEFD5',
	peachpuff: '#FFDAB9',
	peru: '#CD853F',
	pink: '#FFC0CB',
	plum: '#DDA0DD',
	powderblue: '#B0E0E6',
	purple: '#800080',
	red: '#FF0000',
	rosybrown: '#BC8F8F',
	royalblue: '#4169E1',
	saddlebrown: '#8B4513',
	salmon: '#FA8072',
	sandybrown: '#F4A460',
	seagreen: '#2E8B57',
	seashell: '#FFF5EE',
	sienna: '#A0522D',
	silver: '#C0C0C0',
	skyblue: '#87CEEB',
	slateblue: '#6A5ACD',
	slategray: '#708090',
	snow: '#FFFAFA',
	springgreen: '#00FF7F',
	steelblue: '#4682B4',
	tan: '#D2B48C',
	teal: '#008080',
	thistle: '#D8BFD8',
	tomato: '#FF6347',
	turquoise: '#40E0D0',
	violet: '#EE82EE',
	wheat: '#F5DEB3',
	white: '#FFFFFF',
	whitesmoke: '#F5F5F5',
	yellow: '#FFFF00',
	yellowgreen: '#9ACD32'
};

export default Color;
