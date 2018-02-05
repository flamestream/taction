import Type from './Type';

class BooleanType extends Type {

	init(v) {

		if (v !== undefined)
			v = !!v

		this.value = v;
	}
}

export default BooleanType;
