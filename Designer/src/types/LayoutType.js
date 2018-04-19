import ComplexType from './ComplexType';
import NumberType from './NumberType';
import IntegerType from './IntegerType';
import StringType from './StringType';
import BooleanType from './BooleanType';
import RectangleType from './RectangleType';
import ColorType from './ColorType';
import ButtonStyleSetType from './ButtonStyleSetType';
import ItemType from './ItemType';
import BorderType from './BorderType';

class LayoutType extends ComplexType {

	constructor() {

		super(...arguments);
		// Layout is root element so it's forced-defined even if inited with nothing
		this.notDefined = false;
	}

	get required() {

		return true;
	}
}

LayoutType.prototype.$definition = {
	name: { cls: StringType },
	version: { cls: IntegerType, data: { defaultValue: 4, value: 4 } },
	opacity: { cls: NumberType, data: { min: 0, max: 1, initValue: 1 } },
	'opacity-hide': { cls: NumberType, data: { min: 0, max: 1, initValue: 0 } },
	'fade-animation-time': { cls: IntegerType, data: { min: 0, initValue: 500 } },
	size: { cls: NumberType, data: { defaultValue: 80, min: 0.001 } },
	'disable-fade-animation': { cls: BooleanType },
	'disable-hide': { cls: BooleanType },
	'disable-radial-menu-animation': { cls: BooleanType },
	margin: { cls: RectangleType },
	border: { cls: BorderType },
	orientation: { cls: StringType, data: { defaultValue: 'vertical', options: ['vertical', 'horizontal'] } },
	color: { cls: ColorType },
	'default-button-style': { cls: ButtonStyleSetType },
	items: { cls: ItemType, array: true }
};

export default LayoutType;
