import ComplexType from './ComplexType';
import RectangleType from './RectangleType';
import ColorType from './ColorType';

class BorderType extends ComplexType {

}

BorderType.prototype.$definition = {
	thickness: { cls: RectangleType },
	radius: { cls: RectangleType },
	color: { cls: ColorType }
};

export default BorderType;
