import Type from './Type';
import Color from '../helpers/Color';

class SolidColorType extends Type {

	init(v) {

		v = Color.fromWpfHex(v, false) || Color.fromName(v);

		this.value = v;
	}

	toJSON() {

		let { value } = this;
		return value && value.getWpfHex();
	}
}

export default SolidColorType;
