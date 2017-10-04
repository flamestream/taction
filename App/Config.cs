using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Taction {

	internal partial class Config {

		private static JSchema _layoutJsonSchema;

		public ConfigLayout Layout { get; private set; }
		public ConfigState State { get; private set; }

		public static JSchema LayoutJsonSchema {
			get {

				if (_layoutJsonSchema == null)
					_layoutJsonSchema = JSchema.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.ConfigLayoutJsonSchema));

				return _layoutJsonSchema;
			}
		}

		public Config() {

			Layout = new ConfigLayout();
			State = new ConfigState();
		}

		public void Save() {

			using (StreamWriter file = File.CreateText(App.FileStatePath)) {

				JsonSerializer serializer = new JsonSerializer {
					Formatting = Formatting.Indented
				};
				serializer.Serialize(file, State);
			}
		}

		public void LoadLayout(string path) {

			if (path == null)
				throw new ArgumentNullException("LoadLayout may not receive null path");

			JObject json;

			var ext = Path.GetExtension(path);
			if (ext == Properties.Resources.ConfigBundleFileExtension) {

				using (var zip = ZipFile.Open(path, ZipArchiveMode.Read)) {

					// Layout existence check
					var entry = zip.Entries.First(e => e.Name == Properties.Resources.ConfigLayoutFileName);
					if (entry == null)
						throw new FileFormatException("Malformed config bundle");

					using (var streamReader = new StreamReader(entry.Open()))
					using (var jsonReader = new JsonTextReader(streamReader)) {

						json = JObject.Load(jsonReader);

						App.Instance.LoadedZip = zip;
						LoadLayout(json);
					}
				}

			} else if (ext == ".json") {

				using (var reader = File.OpenText(path))
				using (var jsonReader = new JsonTextReader(reader)) {

					json = JObject.Load(jsonReader);
					LoadLayout(json);
				}

			} else {

				throw new FileFormatException(string.Format("Unsupported format {0}", ext));
			}
		}

		public void LoadLayout(JObject json) {

			// Validation check
			if (!json.IsValid(LayoutJsonSchema, out IList<ValidationError> errors)) {

				var errMsgs = new List<string>();
				foreach (var error in errors)
					errMsgs.Add(ParseError(error));

				var errMsg = string.Join(Environment.NewLine, errMsgs);

				throw new FormatException(errMsg);
			}

			// Populate
			Layout = JsonConvert.DeserializeObject<ConfigLayout>(JsonConvert.SerializeObject(json));
		}

		public void LoadState() {

			// Existence check
			// @NOTE Load default one here if there are any
			if (!File.Exists(App.FileStatePath))
				return;

			try {

				JObject json;
				using (var reader = File.OpenText(App.FileStatePath))
				using (var jsonReader = new JsonTextReader(reader)) {

					json = JObject.Load(jsonReader);
				}

				// Populate
				State = JsonConvert.DeserializeObject<ConfigState>(JsonConvert.SerializeObject(json));

			} catch (Exception e) {

				// Load error, but continue with default state
				App.Instance.ErrorLogger.Log(string.Format("Failed to load state: {0}\n{1}", e.Message, e.StackTrace));
				ResetState();
			}
		}

		public void ResetState() {

			File.Delete(App.FileStatePath);
		}

		private static string ParseError(ValidationError error) {

			if (error.ChildErrors.Count == 0)
				return string.Format("Line {1}:{2}: {0}", error.Message, error.LineNumber, error.LinePosition);

			var errMsgs = new List<string>();
			foreach (var e in error.ChildErrors)
				errMsgs.Add(ParseError(e));

			return string.Join(Environment.NewLine, errMsgs);
		}
	}
}
