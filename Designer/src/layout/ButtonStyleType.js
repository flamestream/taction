import ComplexType from './ComplexType';
import NumberType from './NumberType';
import RectangleType from './RectangleType';
import ContentType from './ContentType';
import TextStyleType from './TextStyleType';
import ColorType from './ColorType';
import BorderType from './BorderType';

class ButtonStyleType extends ComplexType {

}

ButtonStyleType.prototype.$definition = {
	content: { cls: ContentType },
	'text-style': { cls: TextStyleType },
	color: { cls: ColorType },
	margin: { cls: RectangleType },
	padding: { cls: RectangleType },
	border: { cls: BorderType },
	opacity: { cls: NumberType, data: { min: 0, max: 1, defaultValue: 1 } }
};

export default ButtonStyleType;
