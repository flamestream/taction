import Type from './Type';
import { isFinite } from 'lodash';

class NumberType extends Type {

	constructor({min, max, value}) {

		super(...arguments);
		this.min = min;
		this.max = max;
	}

	init(v) {
		if (v !== undefined) {

			if (typeof v === 'string')
				v = Number.parseFloat(v);

			if (!isFinite(v))
				v = this.defaultValue || 0;

			let { min, max } = this;
			if (min !== undefined && v < min)
				v = min;
			else if (max !== undefined && v > max)
				v = max;

		}

		this.value = v;
	}
}

export default NumberType;
