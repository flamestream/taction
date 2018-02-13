import ComplexType from './ComplexType';
import ButtonStyleType from './ButtonStyleType';

class ButtonStyleSetType extends ComplexType {

}

ButtonStyleSetType.prototype.$definition = {
	base: { cls: ButtonStyleType },
	active: { cls: ButtonStyleType }
};

export default ButtonStyleSetType;
