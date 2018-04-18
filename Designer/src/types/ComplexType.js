import Type from './Type';
import StringType from './StringType';

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

			// Auto-generate type field
			let types = Object.keys(prototype.$typedDefinition);
			if (definition && !definition.type) {

				definition['type'] = { cls: StringType, data: { options: types, defaultValue: type } };
			}
			this.types = types;

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

	get definition() {

		let o;
		let { prototype } = this.constructor;
		if (prototype.$typedDefinition) {

			let type = this._value.type.value;
			o = prototype.$typedDefinition[type];

		} else if (prototype.$definition) {

			o = prototype.$definition;
		}

		return o;
	}

	changeType(type) {

		// Type check
		let { prototype } = this.constructor;
		let definition = prototype.$typedDefinition;
		if (!definition)
			return;

		// @TODO Optimize replacement
		let value = JSON.parse(JSON.stringify({...this._value, type}));
		this.init(value);
	}

	pushElement(key, value) {

		// cls fetch
		let { definition } = this;
		if (!definition)
			return;

		// Instantiate
		/* eslint new-cap: 0 */
		let { cls } = definition[key];
		let el = new (cls)({value});
		this._value[key].push(el);

		return el;
	}

	pullElement(key, value) {

		this._value[key] = this._value[key].filter(i => i !== value);
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

			return value.map(i => new (cls)({value: i, ...data})).filter(i => !!i._value);

		} else {

			return new (cls)({value, ...data});
		}
	}
}

export default ComplexType;
