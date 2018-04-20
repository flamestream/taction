import ComplexType from './ComplexType';
import NumberType from './NumberType';
import RectangleType from './RectangleType';
import ContentType from './ContentType';
import TextStyleType from './TextStyleType';
import ColorType from './ColorType';
import BorderType from './BorderType';

class RadialMenuCentralItemStyleType extends ComplexType {

}

RadialMenuCentralItemStyleType.prototype.$definition = {
	size: { cls: NumberType, data: { min: 0 } },
	content: { cls: ContentType },
	'text-style': { cls: TextStyleType },
	color: { cls: ColorType },
	border: { cls: BorderType },
	padding: { cls: RectangleType }
};

export default RadialMenuCentralItemStyleType;
