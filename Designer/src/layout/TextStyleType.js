import ComplexType from './ComplexType';
import ColorType from './ColorType';
import NumberType from './NumberType';
import StringType from './StringType';

class TextStyleType extends ComplexType {

}

TextStyleType.prototype.$definition = {
	'font-size': { cls: NumberType, data: { min: 0.001 } },
	'font-family': { cls: StringType },
	'font-weight': { cls: StringType },
	color: { cls: ColorType }
};

export default TextStyleType;
