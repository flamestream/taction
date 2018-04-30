import registry from './registry';

class Type {

	constructor({value, defaultValue, initValue, array} = {}) {

		// If set, undefined value fallback (acts as required flag)
		this.defaultValue = defaultValue;
		// Value taken when defined and no value yet
		this.initValue = initValue;
		// Constructor name
		this.type = this.constructor.name;

		this._value = undefined;

		if (this.init) this.init(value);

		// Flag indicating if the field is undefined
		// @NOTE has to be after init, since init will generate containers
		this.notDefined = value === undefined;

		this.parent = null;

		registry.$register(this.type, this);
	}

	get required() {

		return this.defaultValue !== undefined;
	}

	get notDefined() {

		return this._notDefined;
	}

	set notDefined(v) {

		// Force required flag
		if (this.required && v)
			return;

		this._notDefined = v;

		// Update parent
		let { parent } = this;
		if (!parent || parent.notDefined === v) return;

		if (v) {
			for (let k in parent._value) {

				let c = parent._value[k];
				if (!c.notDefined)
					return;
			}
		}

		parent.notDefined = v;
	}

	get value() {

		if (this.notDefined)
			return;

		let out = this._value;
		if (out === undefined)
			out = (this.initValue !== undefined) ? this.initValue : this.defaultValue;

		return out;
	}

	set value(v) {

		this._notDefined = false;
		this._value = v;

		// Set parent
		if (typeof v === 'object') {

			for (let k in v) {

				let c = v[k];
				if (c instanceof Type)
					c.parent = this;
			}
		}
	}

	getObj(path, getEvenIfNotDefined, getValue) {

		if (!path)
			path = [];
		else if (!Array.isArray(path))
			path = path.split('.');

		let current = this;
		for (let segment of path) {

			let value = current._value;
			if (!value)
				return;

			current = value[segment];
			if (!current)
				return;
		}

		if (current.notDefined && !getEvenIfNotDefined)
			return;

		if (!getValue)
			return current;

		let value = current.value;
		if (value === undefined)
			value = (current.initValue !== undefined) ? current.initValue : current.defaultValue;

		return value;
	}

	toJSON() {

		return this.value;
	}
}

export default Type;
