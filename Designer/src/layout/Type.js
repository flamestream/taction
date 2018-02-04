import registry from './registry';

class Type {

	constructor({value, required, defaultValue}) {

		this.required = !!required;
		this.defaultValue = defaultValue;
		this.type = this.constructor.name;
		this.init(value);
		registry.$register(this.type, this);
	}

	init(input, { append } = {}) {

		/* eslint no-mixed-operators: 0 */
		if (input === undefined && !this.required)
			return;

		let value = {};
		let { prototype } = this.constructor;

		if (prototype.$typedDefinition) {
			let type = typeof input === 'object' && input.type || this.value && this.value.type.value;

			let definition = prototype.$typedDefinition[type];
			value = this._process({
				input,
				definition
			});

		} else if (prototype.$definition) {

			value = this._process({
				input,
				definition: prototype.$definition
			});
		}

		if (append && this.value && typeof this.value === 'object') {

			for (let k in value) {

				let v = value[k];
				if (v === undefined) continue;

				if (Array.isArray(this.value[k]))
					this.value[k].push(v);
				else
					this.value[k] = v;
			}

		} else {

			this.value = value;// || v; // @NOTE Keeps unknowns
		}
	}

	_process({input, definition}) {

		if (!definition)
			return;

		let out = {};

		for (let key in definition) {

			let value = input[key];
			if (value === undefined) continue;

			out[key] = this._init({key, value, definition});
		}

		return out;
	}

	_init({key, value, definition}) {

		let def = definition[key];
		if (!def) return;

		let { cls, data } = def;

		/* eslint new-cap: 0 */
		if (Array.isArray(value)) {
			return value.map(i => new (cls)({value: i, ...data})).filter(i => !!i.value);
		} else {
			return new (cls)({value, ...data});
		}
	}

	toJSON() {

		return this.value;
	}
}

export default Type;
