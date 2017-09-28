using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;

namespace Taction {

	internal partial class Config {

		private static string _FileLayoutPath;
		private static string _FileStatePath;

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
		private static JSchema schema;

		/// <summary>
		/// Use Load instead.
		/// </summary>
		public Config() {

			layout = new Layout();
			state = new State();
		}

		public void Load() {

			LoadState();
			LoadLayout();
		}

		public void Save() {

			using (StreamWriter file = File.CreateText(FileStatePath)) {

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
