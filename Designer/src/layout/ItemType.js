import ComplexType from './ComplexType';
import StringType from './StringType';
import NumberType from './NumberType';
import ButtonStyleSetType from './ButtonStyleSetType';

class ItemType extends ComplexType {

}

ItemType.prototype.$typedDefinition = {
	pivot: {
		type: { cls: StringType, data: { defaultValue: 'pivot' } },
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		items: { cls: ItemType, data: { defaultValue: [] } }
	},
	hold: {
		type: { cls: StringType, data: { defaultValue: 'hold' } },
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	tap: {
		type: { cls: StringType, data: { defaultValue: 'tap' } },
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	toggle: {
		type: { cls: StringType, data: { defaultValue: 'toggle' } },
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		command: { cls: StringType, data: { defaultValue: '' } },
		style: { cls: ButtonStyleSetType }
	},
	move: {
		type: { cls: StringType, data: { defaultValue: 'move' } },
		size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
		style: { cls: ButtonStyleSetType }
	}
	// 'radial-menu': {
	// 	type: { cls: StringType, data: { defaultValue: 'radial-menu' } },
	// 	size: { cls: NumberType, data: { min: 0.001, defaultValue: 10 } },
	// 	'radial-menu': { cls: RadialMenuType, data: { required: true } },
	// 	style: { cls: ButtonStyleSetType }
	// }
}

export default ItemType;
