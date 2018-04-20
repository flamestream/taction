import ComplexType from './ComplexType';
import NumberType from './NumberType';
import ContentType from './ContentType';
import TextStyleType from './TextStyleType';
import ColorType from './ColorType';
import BorderType from './BorderType';

class RadialMenuItemEdgeStyleType extends ComplexType {

}

RadialMenuItemEdgeStyleType.prototype.$definition = {
	size: { cls: NumberType, data: { min: 0 } },
	'start-distance': { cls: NumberType, data: { min: 0 } },
	content: { cls: ContentType },
	'text-style': { cls: TextStyleType },
	color: { cls: ColorType },
	border: { cls: BorderType }
};

export default RadialMenuItemEdgeStyleType;
