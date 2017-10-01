using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;

namespace Taction {

	internal partial class Config {

		private static JSchema _layoutJsonSchema;

		/// <summary>
		/// Loaded config layout data.
		/// </summary>
		public Layout layout { get; private set; }

		/// <summary>
		/// Loaded config state data.
		/// </summary>
		public State state { get; private set; }

		/// <summary>
		/// Cached schema.
		/// </summary>
		public static JSchema layoutJsonSchema {
			get {

				if (_layoutJsonSchema == null)
					_layoutJsonSchema = JSchema.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.ConfigLayoutJsonSchema));

				return _layoutJsonSchema;
			}
		}

		/// <summary>
		/// Use Load instead.
		/// </summary>
		public Config() {

			layout = new Layout();
			state = new State();
		}

		public void Save() {

			using (StreamWriter file = File.CreateText(fileStatePath)) {

				JsonSerializer serializer = new JsonSerializer {
					Formatting = Formatting.Indented
				};
				serializer.Serialize(file, this.state);
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
