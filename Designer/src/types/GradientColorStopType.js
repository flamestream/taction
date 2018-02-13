import Type from './Type';
import SolidColorType from './SolidColorType';
import NumberType from './NumberType';

class GradientColorStopType extends Type {

	init(v) {

		if (typeof v !== 'string')
			v = '';

		let data = v.split(' ');
		let position = new NumberType({defaultValue: 0, min: 0, max: 1, value: data[0]});
		let color = new SolidColorType({defaultValue: 'white', value: data[1]});

		v = {
			position,
			color
		};

		this.value = v;
	}

	toJSON() {

		let { value } = this;
		let { position, color } = value || {};

		if (!position) position = 0;
		if (!color) color = '#FFFFFF';

		return `${position.toJSON()} ${color.toJSON()}`;
	}
}

export default GradientColorStopType;
