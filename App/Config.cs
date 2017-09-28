using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;

namespace Taction {

	internal partial class Config {

		private static string _FilePath;

		/// <summary>
		/// Loaded config data.
		/// </summary>
		public Data data { get; private set; }

		/// <summary>
		/// Cached schema
		/// </summary>
		private static JSchema schema;

		/// <summary>
		/// Cached file path of the config file.
		/// </summary>
		public static string FilePath {
			get {
				if (_FilePath == null) {

					_FilePath = string.Format(@"{0}\{1}",
						App.AppDataDir,
						Properties.Resources.ConfigFileName
					);
				}

				return _FilePath;
			}
		}

		/// <summary>
		/// Use Load instead.
		/// </summary>
		public Config() {

			data = new Data();
		}

		public void Load() {

			JObject json;
			if (File.Exists(FilePath)) {

				using (var reader = File.OpenText(FilePath))
				using (var jsonReader = new JsonTextReader(reader)) {

					// @TODO File load error, etc
					json = JObject.Load(jsonReader);
				}

			} else {

				// Load default config (Guaranteed)
				json = JObject.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.DefaultConfigJson));
			}

			Load(json);
		}

		/// <summary>
		/// Load configuration.
		/// </summary>
		/// <param name="configFile">Path to config file</param>
		/// <returns></returns>
		public void Load(JObject json) {

			// Load schema
			if (schema == null)
				schema = JSchema.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.ConfigJsonSchema));

			// Validation check
			if (!json.IsValid(schema, out IList<ValidationError> errors)) {

				var errMsgs = new List<string>();
				foreach (var error in errors)
					errMsgs.Add(ParseError(error));

				var errMsg = string.Join(Environment.NewLine, errMsgs);

				throw new FormatException(errMsg);
			}

			// Load data
			this.data = JsonConvert.DeserializeObject<Data>(JsonConvert.SerializeObject(json));
		}

		public void Save(string path = null) {

			if (path == null)
				path = FilePath;

			using (StreamWriter file = File.CreateText(path)) {

				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, this.data);
			}
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
