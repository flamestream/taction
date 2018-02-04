import Type from './Type';
import Color from '../helpers/Color';

class SolidColorType extends Type {

	init(v) {

		if (v === undefined && !this.required)
			return;

		this.value = Color.fromWpfHex(v, false) || Color.fromName(v);
	}

	toJSON() {

		let { value } = this;
		return value && value.getWpfHex();
	}
}

export default SolidColorType;
