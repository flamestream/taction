import ComplexType from './ComplexType';
import StringType from './StringType';
import RadialMenuItemStyleSetType from './RadialMenuItemStyleSetType';

class RadialMenuItemType extends ComplexType {

}

RadialMenuItemType.prototype.$definition = {
	command: { cls: StringType, data: { defaultValue: '' } },
	style: { cls: RadialMenuItemStyleSetType }
};

export default RadialMenuItemType;
