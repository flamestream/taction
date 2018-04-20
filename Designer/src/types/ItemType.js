import ComplexType from './ComplexType';
import StringType from './StringType';
import NumberType from './NumberType';
import ButtonStyleSetType from './ButtonStyleSetType';
import RadialMenuType from './RadialMenuType';

class ItemType extends ComplexType {

	get required() {

		return true;
	}
}

ItemType.prototype.$typedDefinition = {
	hold: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 80 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	tap: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 80 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	toggle: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 80 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	move: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 40 } },
		style: { cls: ButtonStyleSetType }
	},
	// pivot: {
	// 	size: { cls: NumberType, data: { min: 0.001, defaultValue: 80 } },
	// 	items: { cls: ItemType, array: true }
	// }
	'radial-menu': {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 80 } },
		'radial-menu': { cls: RadialMenuType, required: true },
		style: { cls: ButtonStyleSetType }
	}
}

export default ItemType;
