import ComplexType from './ComplexType';
import RadialMenuItemStyleType from './RadialMenuItemStyleType';

class RadialMenuItemStyleSetType extends ComplexType {

}

RadialMenuItemStyleSetType.prototype.$definition = {
	base: { cls: RadialMenuItemStyleType },
	active: { cls: RadialMenuItemStyleType }
};

export default RadialMenuItemStyleSetType;
