import ComplexType from './ComplexType';
import SolidColorType from './SolidColorType';
import GradientColorStopType from './GradientColorStopType';
import StringType from './StringType';
import NumberType from './NumberType';

class ColorType extends ComplexType {

}

ColorType.prototype.$typedDefinition = {
	solid: {
		value: { cls: SolidColorType, data: { defaultValue: 'white' } }
	},
	gradient: {
		angle: { cls: NumberType },
		values: { cls: GradientColorStopType, array: true }
	},
	image: {
		tile: { cls: StringType, data: { options: ['none', 'normal', 'flip-x', 'flip-y', 'flip-xy'] } },
		stretch: { cls: StringType, data: { options: ['uniform', 'none', 'fill', 'uniform-fill'] } },
		colorize: { cls: SolidColorType },
		source: { cls: StringType, data: { defaultValue: '' } }
	}
}

export default ColorType;
