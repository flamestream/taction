using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Taction {

	partial class Config {

		private static string _fileStatePath;

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

			JObject json;
			using (var reader = File.OpenText(fileStatePath))
			using (var jsonReader = new JsonTextReader(reader)) {

				// @TODO File load error, etc
				json = JObject.Load(jsonReader);
			}

			// Populate
			this.state = JsonConvert.DeserializeObject<State>(JsonConvert.SerializeObject(json));
		}

		public class State {

			public double x;
			public double y;
		}
	}
}
