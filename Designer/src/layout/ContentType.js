import Type from './Type';
import NumberType from './NumberType';
import StringType from './StringType';
import RectangleType from './RectangleType';
import SolidColorType from './SolidColorType';

class ContentType extends Type {

}

ContentType.prototype.$typedDefinition = {
	text: {
		type: { cls: StringType, data: { required: true } },
		value: { cls: StringType }
	},
	image: {
		type: { cls: StringType, data: { required: true } },
		opacity: { cls: NumberType, data: { min: 0, max: 1, defaultValue: 1 } },
		stretch: { cls: StringType, data: { options: ['none', 'fill', 'uniform', 'uniform-fill'], defaultValue: 'uniform' } },
		margin: { cls: RectangleType },
		colorize: { cls: SolidColorType },
		source: { cls: StringType, data: { required: true } }
	}
}

export default ContentType;
