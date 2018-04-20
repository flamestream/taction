import ComplexType from './ComplexType';
import RadialMenuItemLabelStyleType from './RadialMenuItemLabelStyleType';
import RadialMenuItemEdgeStyleType from './RadialMenuItemEdgeStyleType';

class RadialMenuItemStyleType extends ComplexType {

}

RadialMenuItemStyleType.prototype.$definition = {
	label: { cls: RadialMenuItemLabelStyleType },
	'inner-edge': { cls: RadialMenuItemEdgeStyleType },
	'outer-edge': { cls: RadialMenuItemEdgeStyleType }
};

export default RadialMenuItemStyleType;
