using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Taction {

	partial class Config {

		private static string _fileStatePath;

		private App App => (App)App.Current;

		/// <summary>
		/// Cached file path of the config state file.
		/// </summary>
		public static string fileStatePath {
			get {
				if (_fileStatePath == null) {

					_fileStatePath = string.Format(@"{0}\{1}",
						App.AppDataDir,
						Properties.Resources.ConfigStateFileName
					);
				}

				return _fileStatePath;
			}
		}

		public void LoadState() {

			// Existence check
			// @NOTE Load default one here if there are any
			if (!File.Exists(fileStatePath))
				return;

			try {

				JObject json;
				using (var reader = File.OpenText(fileStatePath))
				using (var jsonReader = new JsonTextReader(reader)) {

					json = JObject.Load(jsonReader);
				}

				// Populate
				State = JsonConvert.DeserializeObject<ConfigState>(JsonConvert.SerializeObject(json));

			} catch (Exception e) {

				// Load error, but continue with default state
				App.ErrorLogger.Log(string.Format("Failed to load state: {0}\n{1}", e.Message, e.StackTrace));
				ResetState();
			}
		}

		public void ResetState() {

			File.Delete(fileStatePath);
		}
	}
}
