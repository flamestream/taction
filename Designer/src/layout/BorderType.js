import Type from './Type';
import RectangleType from './RectangleType';
import ColorType from './ColorType';

class BorderType extends Type {

}

BorderType.prototype.$definition = {
	thickness: { cls: RectangleType },
	radius: { cls: RectangleType },
	color: { cls: ColorType }
};

export default BorderType;
