using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArtTouchPanel {

	internal partial class Config {

		/// <summary>
		/// Loaded config data.
		/// </summary>
		public Data data { get; private set; }

		/// <summary>
		/// Cached schema
		/// </summary>
		private static JSchema schema;

		/// <summary>
		/// Use Load instead.
		/// </summary>
		private Config() { }

		/// <summary>
		/// Load configuration.
		/// </summary>
		/// <param name="configFile">Path to config file</param>
		/// <returns></returns>
		public static Config Load(string configFile = null) {

			// Read file
			JObject json;
			if (configFile != null) {

				using (var reader = File.OpenText(Environment.ExpandEnvironmentVariables(configFile)))
				using (var jsonReader = new JsonTextReader(reader)) {

					json = JObject.Load(jsonReader);
				}

			} else {
				// Load default config
				json = JObject.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.DefaultConfigJson));
			}

			// Load schema
			if (schema == null)
				schema = JSchema.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.ConfigJsonSchema));

			// Validation check
			if (!json.IsValid(schema, out IList<ValidationError> errors)) {

				var errMsgs = new List<string>();
				foreach (var error in errors)
					errMsgs.Add(ParseError(error));

				var errMsg = string.Join("\n", errMsgs);

				// Prevent generated message to overflow due to abuse
				if (errMsg.Length > 512)
					errMsg = errMsg.Substring(0, 512) + "...";

				throw new FormatException(errMsg);
			}

			// Load data
			var config = new Config();
			config.data = JsonConvert.DeserializeObject<Data>(JsonConvert.SerializeObject(json));

			return config;
		}

		private static string ParseError(ValidationError error) {

			if (error.ChildErrors.Count == 0)
				return string.Format("Line {1}:{2}: {0}", error.Message, error.LineNumber, error.LinePosition);

			var errMsgs = new List<string>();
			foreach (var e in error.ChildErrors)
				errMsgs.Add(ParseError(e));

			return string.Join("\n", errMsgs);
		}
	}
}
