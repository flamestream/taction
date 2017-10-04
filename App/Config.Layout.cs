using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Taction.Attribute;

namespace Taction {

	partial class Config {

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

						App.LoadedZip = zip;
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

		#region -- Cached Generated values --

		private static string _FileLayoutPath;
		private static string _FileBundleName;
		private static string _FileBundlePath;
		private static Dictionary<string, Type> _StringVsPanelItemSpecs;

		public static string FileLayoutPath {
			get {
				if (_FileLayoutPath == null) {

					_FileLayoutPath = string.Format(@"{0}\{1}",
						App.AppDataDir,
						Properties.Resources.ConfigLayoutFileName
					);
				}

				return _FileLayoutPath;
			}
		}

		public static string FileBundlePath {
			get {
				if (_FileBundlePath == null) {

					_FileBundlePath = string.Format(@"{0}\{1}",
						App.AppDataDir,
						FileBundleName
					);
				}

				return _FileBundlePath;
			}
		}

		public static string FileBundleName {
			get {
				if (_FileBundleName == null) {

					_FileBundleName = Properties.Resources.ConfigBundleFileBaseName
						+ Properties.Resources.ConfigBundleFileExtension;
				}

				return _FileBundleName;
			}
		}

		public static Dictionary<string, Type> StringVsPanelItemSpecs {
			get {
				if (_StringVsPanelItemSpecs == null) {

					_StringVsPanelItemSpecs = new Dictionary<string, Type>();

					// List implemented PanelItemSpecs
					var specsTypes = AppDomain.CurrentDomain.GetAssemblies()
						.SelectMany(s => s.GetTypes())
						.Where(p => p.IsClass && typeof(IPanelItemSpecs).IsAssignableFrom(p));

					// Create associations
					foreach (var specsType in specsTypes) {

						var typeAttrs = specsType.GetCustomAttributes(typeof(JsonStringTypeValueAttribute), true);
						if (typeAttrs.Length == 0)
							continue;

						var typeStr = ((JsonStringTypeValueAttribute)typeAttrs[0]).Value;
						_StringVsPanelItemSpecs.Add(typeStr, specsType);
					}
				}

				// Return copy
				return new Dictionary<string, Type>(_StringVsPanelItemSpecs);
			}
		}

		#endregion -- Cached Generated values --

	}
}
