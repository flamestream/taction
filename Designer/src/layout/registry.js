class Registry {

	$register(name, data) {

		if (!name)
			return;

		if (!this[name]) {

			this[name] = {
				_maxId: -1
			};
		}

		let registry = this[name];

		data.id = ++registry._maxId;
		registry[data.id] = data;
	}

	$clear() {

		for (let k in this) {

			if (typeof k !== 'string' || !k.startsWith('$'))
				continue;

			this[k] = undefined;
		}
	}
}

const registry = new Registry();

export default registry;
