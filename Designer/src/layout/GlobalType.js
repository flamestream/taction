import Type from './Type';
import NumberType from './NumberType';
import IntegerType from './IntegerType';
import StringType from './StringType';
import BooleanType from './BooleanType';
import RectangleType from './RectangleType';
import ColorType from './ColorType';
import ButtonStyleSetType from './ButtonStyleSetType';
import ItemType from './ItemType';
import BorderType from './BorderType';

class GlobalType extends Type {

}

GlobalType.prototype.$definition = {
	name: { cls: StringType },
	version: { cls: IntegerType, data: { required: true, value: 4 } },
	opacity: { cls: NumberType, data: { min: 0, max: 1, defaultValue: 1 } },
	'opacity-hide': { cls: NumberType, data: {min: 0, max: 1, defaultValue: 0} },
	'fade-animation-time': { cls: IntegerType, data: { min: 0, defaultValue: 500 } },
	size: { cls: NumberType, data: {required: true, min: 0.001} },
	'disable-fade-animation': { cls: BooleanType },
	'disable-hide': { cls: BooleanType },
	'disable-radial-menu-animation': { cls: BooleanType },
	margin: { cls: RectangleType },
	border: { cls: BorderType },
	orientation: { cls: StringType, data: { required: true, options: ['vertical', 'horizontal'] } },
	color: { cls: ColorType },
	'default-button-style': { cls: ButtonStyleSetType },
	items: { cls: ItemType, data: { required: true } }
};

export default GlobalType;
