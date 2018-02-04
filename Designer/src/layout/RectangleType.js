import Type from './Type';

class RectangleType extends Type {

	constructor() {

		super(...arguments);
		this.valid = false;
	}

	init(v) {

		if (v === undefined && !this.required)
			return;

		if (typeof v !== 'string')
			v = '';

		this.valid = this._isValueValid(v);
		this.value = v;
	}

	toJSON() {

		return this.value || undefined;
	}

	_isValueValid(v) {

		return v.match(/^\d+(\.\d+)?( \d+(\.\d+)?){0,3}$/);
	}
}

export default RectangleType;
