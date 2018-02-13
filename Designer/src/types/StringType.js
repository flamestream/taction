import Type from './Type';

class StringType extends Type {

	constructor({options}) {

		super(...arguments);
		let choices = this.options = options;

		// Init with first choice if there are some
		if (this.notDefined && choices) {
			this._value = choices[0];
		}
	}

	init(v) {

		if (typeof v !== 'string')
			v = this.defaultValue || '';

		let { options } = this;
		/* eslint no-mixed-operators: 0 */
		if (Array.isArray(options) && !options.includes(v))
			v = options.includes(this.defaultValue) && this.defaultValue || options[0];

		this.value = v;
	}
}

export default StringType;
