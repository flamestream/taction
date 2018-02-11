import fileType from 'file-type'
import fileReaderAsync from '@/helpers/file-reader-async'

export default async function(file) {

	let header = file.slice(0, 4100);
	let blob = await fileReaderAsync(header);
	return fileType(blob);
};
