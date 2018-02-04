import NumberType from './NumberType'
import IntegerType from './IntegerType'
import StringType from './StringType'
import BooleanType from './BooleanType'
import RectangleType from './RectangleType'
import ColorType from './ColorType'
import TextStyleType from './TextStyleType'
import ContentType from './ContentType'

function index(layout, registry) {

	if (registry._maxId === undefined) registry._maxId = -1;

	process(layout, definitionGlobal);

	let defaultButtonStyle = layout['default-button-style'];
	if (defaultButtonStyle) {

		let { base, active } = defaultButtonStyle;
		process(base, definitionButtonStyle);
		process(active, definitionButtonStyle);
	}

	processItems(layout)
}

function process(obj, definition) {

	for (let key in definition) {

		let def = definition[key];
		let value = obj[key];
		if (value === undefined) continue;

		let { cls, data } = def;
		/* eslint new-cap: 0 */
		obj[key] = new (cls)({...data, value});
	}
}

function processItems(obj) {

	let { items } = obj || {};
	if (!items || !Array.isArray(items))
		return;

	for (let i of items) {

		let { type } = i;
		if (type === 'hold') {

		}
	}
}

export default {
	index
}

const definitionGlobal = {
	'opacity': { cls: NumberType, data: {min: 0, max: 1} },
	'opacity-hide': { cls: NumberType, data: {min: 0, max: 1} },
	'fade-animation-time': { cls: IntegerType, data: {min: 0} },
	'size': { cls: NumberType, data: {min: 0.001} },
	'name': { cls: StringType },
	'disable-fade-animation': { cls: BooleanType },
	'disable-hide': { cls: BooleanType },
	'disable-radial-menu-animation': { cls: BooleanType },
	'margin': { cls: RectangleType },
	'border': { cls: RectangleType },
	'orientation': { cls: StringType, required: true, data: { options: ['vertical', 'horizontal'] } },
	'color': { cls: ColorType }
};

const definitionButtonStyle = {
	'content': { cls: ContentType },
	'text-style': { cls: TextStyleType },
	'color': { cls: ColorType },
	'margin': { cls: RectangleType },
	'padding': { cls: RectangleType },
	'border': { cls: RectangleType },
	'opacity': { cls: NumberType, data: {min: 0, max: 1} }
};
