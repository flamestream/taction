import registry from './registry';

class Type {

	constructor({value, defaultValue, array} = {}) {

		// If set, undefined value fallback (acts as required flag)
		this.defaultValue = defaultValue;
		// Constructor name
		this.type = this.constructor.name;

		this._value = undefined;

		if (this.init) this.init(value);

		// Flag indicating if the field is undefined
		// @NOTE has to be after init, since init will generate containers
		this.notDefined = value === undefined;

		registry.$register(this.type, this);
	}

	get required() {

		return this.defaultValue !== undefined;
	}

	get value() {

		if (this.notDefined)
			return;

		return this._value;
	}

	set value(v) {

		if (v === undefined) {

			if (!this.required) {

				this.notDefined = true;
				return;
			}

			this.value = this.defaultValue;
			return;
		}

		this.notDefined = false;
		console.log('set!', v, this.type, this.notDefined)
		this._value = v;
	}

	toJSON() {

		return this.value;
	}
}

export default Type;
