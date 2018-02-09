function fileReaderAsync(f) {

	return new Promise((resolve, reject) => {

		let reader = new FileReader();

		reader.onload = (ev) => {

			resolve(ev.target.result);
		};

		reader.onerror = (ev) => {

			reject(ev.target);
		};
		reader.onabort = reader.onerror;

		reader.readAsArrayBuffer(f);
	});
}

export default fileReaderAsync;
