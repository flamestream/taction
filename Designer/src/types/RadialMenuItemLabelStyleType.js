import ComplexType from './ComplexType';
import NumberType from './NumberType';
import RectangleType from './RectangleType';
import ContentType from './ContentType';
import TextStyleType from './TextStyleType';
import ColorType from './ColorType';
import BorderType from './BorderType';

class RadialMenuItemLabelStyleType extends ComplexType {

}

RadialMenuItemLabelStyleType.prototype.$definition = {
	size: { cls: NumberType, data: { min: 0 } },
	'start-distance': { cls: NumberType, data: { min: 0 } },
	content: { cls: ContentType },
	'text-style': { cls: TextStyleType },
	color: { cls: ColorType },
	margin: { cls: RectangleType },
	padding: { cls: RectangleType },
	border: { cls: BorderType },
	opacity: { cls: NumberType, data: { min: 0, max: 1, initValue: 1 } }
};

export default RadialMenuItemLabelStyleType;
