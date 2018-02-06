import ComplexType from './ComplexType';
import StringType from './StringType';
import NumberType from './NumberType';
import ButtonStyleSetType from './ButtonStyleSetType';

class ItemType extends ComplexType {

}

ItemType.prototype.$typedDefinition = {
	pivot: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		items: { cls: ItemType, array: true }
	},
	hold: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	tap: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	toggle: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	move: {
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		style: { cls: ButtonStyleSetType }
	}
	// 'radial-menu': {
	// 	size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
	// 	'radial-menu': { cls: RadialMenuType, data: { required: true } },
	// 	style: { cls: ButtonStyleSetType }
	// }
}

export default ItemType;
