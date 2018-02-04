import Type from './Type';

class StringType extends Type {

	constructor({options}) {

		super(...arguments);
		this.options = options;
	}

	init(v) {

		if (v === undefined && !this.required)
			return;

		if (typeof v !== 'string')
			v = this.defaultValue || '';

		let value;
		let { options } = this;
		/* eslint no-mixed-operators: 0 */
		if (Array.isArray(options) && !options.includes(v))
			value = options.includes(this.defaultValue) && this.defaultValue || options[0];
		else
			value = v;

		this.value = value;
	}

	toJSON() {

		return this.value;
	}
}

export default StringType;
