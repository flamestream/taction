import Type from './Type';
import ButtonStyleType from './ButtonStyleType';

class ButtonStyleSetType extends Type {

}

ButtonStyleSetType.prototype.$definition = {
	base: { cls: ButtonStyleType },
	active: { cls: ButtonStyleType }
};

export default ButtonStyleSetType;
