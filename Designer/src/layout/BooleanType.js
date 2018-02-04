import Type from './Type';

class BooleanType extends Type {

	init(v) {

		if (v === undefined && !this.required)
			return;

		this.value = !!v;
	}
}

export default BooleanType;
