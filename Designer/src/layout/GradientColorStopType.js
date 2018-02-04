import Type from './Type';
import SolidColorType from './SolidColorType';
import NumberType from './NumberType';

class GradientColorStopType extends Type {

	init(v) {

		if (v === undefined && !this.required)
			return;

		if (typeof v !== 'string')
			v = '';

		let data = v.split(' ');
		let position = new NumberType({required: true, min: 0, max: 1, value: data[0]});
		let color = new SolidColorType({required: true, value: data[1]});

		this.value = {
			position,
			color
		};
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
