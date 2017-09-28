using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.IO;

namespace Taction {

	partial class Config {

		/// <summary>
		/// Cached file path of the config state file.
		/// </summary>
		public static string FileStatePath {
			get {
				if (_FileStatePath == null) {

					_FileStatePath = string.Format(@"{0}\{1}",
						App.AppDataDir,
						Properties.Resources.ConfigStateFileName
					);
				}

				return _FileStatePath;
			}
		}

		public void LoadState(string path = null) {

			JObject json;
			if (File.Exists(FileStatePath)) {

				using (var reader = File.OpenText(FileLayoutPath))
				using (var jsonReader = new JsonTextReader(reader)) {

					// @TODO File load error, etc
					json = JObject.Load(jsonReader);
				}

			} else {

				// Load default config (Guaranteed)
				json = JObject.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.DefaultConfigJson));
			}

			// Load schema
			if (schema == null)
				schema = JSchema.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.ConfigJsonSchema));

			// Validation check
			//if (!json.IsValid(schema, out IList<ValidationError> errors)) {

			//	var errMsgs = new List<string>();
			//	foreach (var error in errors)
			//		errMsgs.Add(ParseError(error));

			//	var errMsg = string.Join(Environment.NewLine, errMsgs);

			//	throw new FormatException(errMsg);
			//}

			this.state = State.Load(json);
		}

		public class State {

			public double x;
			public double y;

			public static State Load(JObject json) {

				return JsonConvert.DeserializeObject<State>(JsonConvert.SerializeObject(json));
			}
		}
	}
}
