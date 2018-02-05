import Type from './Type';

class ComplexType extends Type {

	init(input) {

		// Sanity
		if (!input || typeof input !== 'object')
			input = {};

		let value;

		// Expand value based on definition
		let definition;
		let { prototype } = this.constructor;
		if (prototype.$typedDefinition) {

			let type = input.type;
			definition = prototype.$typedDefinition[type];

		} else if (prototype.$definition) {

			definition = prototype.$definition;
		}

		if (definition) {

			value = this._expandValue({
				input,
				definition
			});
		}

		this.value = value;
	}

	_expandValue({input, definition}) {

		if (!definition)
			return;

		let out = {};

		for (let key in definition) {

			let value = input[key];
			out[key] = this._spawnField({key, value, definition});
		}

		return out;
	}

	_spawnField({key, value, definition}) {

		let def = definition[key];
		if (!def) return;

		let { cls, data, array } = def;

		/* eslint new-cap: 0 */
		if (array) {

			if (!value) return [];

			// Sanity
			if (!Array.isArray(value)) value = [value];

			return value.map(i => new (cls)({value: i, ...data}));

		} else {

			return new (cls)({value, ...data});
		}
	}
}

export default ComplexType;
