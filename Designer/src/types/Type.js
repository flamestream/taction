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

		return this._value;
	}

	set value(v) {

		if (v === undefined) {

			if (!this.required) {

				this.notDefined = true;
				return;
			}

			if (this.defaultValue === undefined)
				return;

			this.value = this.defaultValue;
			return;
		}

		this.notDefined = false;
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

	getObj(path, getEvenIfNotDefined) {

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

		return current;
	}

	toJSON() {

		return this.value;
	}
}

export default Type;
