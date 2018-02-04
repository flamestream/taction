import Type from './Type';
import SolidColorType from './SolidColorType';
import GradientColorStopType from './GradientColorStopType';
import StringType from './StringType';
import NumberType from './NumberType';

class ColorType extends Type {

}

ColorType.prototype.$typedDefinition = {
	solid: {
		type: { cls: StringType, data: { required: true } },
		value: { cls: SolidColorType, data: { required: true } }
	},
	gradient: {
		type: { cls: StringType, data: { required: true } },
		angle: { cls: NumberType },
		values: { cls: GradientColorStopType, data: { required: true } }
	},
	image: {
		type: { cls: StringType, data: { required: true } },
		tile: { cls: StringType, data: { options: ['none', 'normal', 'flip-x', 'flip-y', 'flip-xy'], defaultValue: 'none' } },
		stretch: { cls: StringType, data: { options: ['none', 'fill', 'uniform', 'uniform-fill'], defaultValue: 'uniform' } },
		colorize: { cls: SolidColorType },
		source: { cls: StringType, data: { required: true } }
	}
}

export default ColorType;
