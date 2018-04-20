import ComplexType from './ComplexType';
import RadialMenuCentralItemStyleType from './RadialMenuCentralItemStyleType';

class RadialMenuCentralItemStyleSetType extends ComplexType {

}

RadialMenuCentralItemStyleSetType.prototype.$definition = {
	base: { cls: RadialMenuCentralItemStyleType },
	active: { cls: RadialMenuCentralItemStyleType }
};

export default RadialMenuCentralItemStyleSetType;
