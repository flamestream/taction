import Type from './Type';
import StringType from './StringType';
import NumberType from './NumberType';
import ButtonStyleSetType from './ButtonStyleSetType';

class ItemType extends Type {

}

ItemType.prototype.$typedDefinition = {
	pivot: {
		type: { cls: StringType, data: { required: true } },
		size: { cls: NumberType, data: { min: 0.001, required: true } },
		items: { cls: ItemType, data: { required: true } }
	},
	hold: {
		type: { cls: StringType, data: { required: true } },
		size: { cls: NumberType, data: { min: 0.001, required: true } },
		command: { cls: StringType, data: { required: true } },
		style: { cls: ButtonStyleSetType }
	},
	tap: {
		type: { cls: StringType, data: { required: true } },
		size: { cls: NumberType, data: { min: 0.001, required: true } },
		command: { cls: StringType, data: { required: true } },
		style: { cls: ButtonStyleSetType }
	},
	toggle: {
		type: { cls: StringType, data: { required: true } },
		size: { cls: NumberType, data: { min: 0.001, required: true } },
		command: { cls: StringType, data: { required: true } },
		style: { cls: ButtonStyleSetType }
	},
	move: {
		type: { cls: StringType, data: { required: true } },
		size: { cls: NumberType, data: { min: 0.001, required: true } },
		style: { cls: ButtonStyleSetType }
	}
	// 'radial-menu': {
	// 	type: { cls: StringType, data: { required: true } },
	// 	size: { cls: NumberType, data: { min: 0.001, required: true } },
	// 	'radial-menu': { cls: RadialMenuType, data: { required: true } },
	// 	style: { cls: ButtonStyleSetType }
	// }
}

export default ItemType;
