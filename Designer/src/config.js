export default {
	name: 'Taction Designer',
	version: '1.0',
	defaultLayout: {
		name: 'My Touch Panel'
	},
	supportedImportExts: ['png', 'ttf'],
	supportedLayoutVersion: 4,
	maximumExportFileNameLength: 64,
	maximumImportFileSize: 20 * 1024 * 1024, // 50MB
	maximumAssetSize: 1024 * 1024 // 1MB
};
