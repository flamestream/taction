import ComplexType from './ComplexType';
import NumberType from './NumberType';
import StringType from './StringType';
import RectangleType from './RectangleType';
import SolidColorType from './SolidColorType';

class ContentType extends ComplexType {

}

ContentType.prototype.$typedDefinition = {
	text: {
		value: { cls: StringType }
	},
	image: {
		opacity: { cls: NumberType, data: { min: 0, max: 1, defaultValue: 1 } },
		stretch: { cls: StringType, data: { options: ['uniform', 'none', 'fill', 'uniform-fill'] } },
		margin: { cls: RectangleType },
		colorize: { cls: SolidColorType },
		source: { cls: StringType, data: { defaultValue: '' } }
	}
}

export default ContentType;
