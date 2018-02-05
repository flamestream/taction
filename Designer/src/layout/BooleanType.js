import Type from './Type';

class BooleanType extends Type {

	init(v) {

		this.value = !!v;
	}
}

export default BooleanType;
