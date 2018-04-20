import ComplexType from './ComplexType';
import BooleanType from './BooleanType';
import RadialMenuItemStyleSetType from './RadialMenuItemStyleSetType';
import RadialMenuCentralItemStyleSetType from './RadialMenuCentralItemStyleSetType';
import RadialMenuItemType from './RadialMenuItemType';

class RadialMenuType extends ComplexType {

}

RadialMenuType.prototype.$definition = {
	'half-shifted-items': { cls: BooleanType },
	'default-item-style': { cls: RadialMenuItemStyleSetType },
	'default-central-item-style': { cls: RadialMenuCentralItemStyleSetType },
	items: { cls: RadialMenuItemType, array: true }
};

export default RadialMenuType;
