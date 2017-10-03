using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Taction.CustomAttribute;
using Taction.CustomUIElement;

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
					}
				}

			} else if (ext == ".json") {

				using (var reader = File.OpenText(path))
				using (var jsonReader = new JsonTextReader(reader)) {

					json = JObject.Load(jsonReader);
				}

			} else {

				throw new FileFormatException(string.Format("Unsupported format {0}", ext));
			}

			LoadLayout(json);
		}

		public void LoadLayout(JObject json) {

			// Validation check
			if (!json.IsValid(layoutJsonSchema, out IList<ValidationError> errors)) {

				var errMsgs = new List<string>();
				foreach (var error in errors)
					errMsgs.Add(ParseError(error));

				var errMsg = string.Join(Environment.NewLine, errMsgs);

				throw new FormatException(errMsg);
			}

			// Populate
			this.layout = JsonConvert.DeserializeObject<Layout>(JsonConvert.SerializeObject(json));
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

						var typeStr = ((JsonStringTypeValueAttribute)typeAttrs[0]).value;
						_StringVsPanelItemSpecs.Add(typeStr, specsType);
					}
				}

				// Return copy
				return new Dictionary<string, Type>(_StringVsPanelItemSpecs);
			}
		}

		#endregion -- Cached Generated values --

		#region -- Internal Classes (Layout POJO) --

		/// <summary>
		/// Configuration root definition
		/// </summary>
		public class Layout : IPanelItemSpecs {

			private float _opacity;
			private float _opacityHide;
			private uint _fadeAnimationTime;

			public int size { get; set; }
			public List<IPanelItemSpecs> items { get; set; }

			[JsonConverter(typeof(PanelOrientationConverter))]
			public System.Windows.Controls.Orientation orientation { get; set; }

			[DefaultValue(1)]
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
			public float opacity {
				get => _opacity;
				set {
					if (value < 0) value = 0;
					else if (value > 1) value = 1;
					_opacity = value;
				}
			}

			[DefaultValue(0)]
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
			public float opacityHide {
				get => _opacityHide;
				set {
					if (value < 0) value = 0;
					else if (value > 1) value = 1;
					_opacityHide = value;
				}
			}

			public bool disableHide { get; set; }

			[DefaultValue(500)]
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
			public uint fadeAnimationTime {
				get => _fadeAnimationTime;
				set {
					if (value < 0) value = 0;
					_fadeAnimationTime = value;
				}
			}

			public bool disableFadeAnimation { get; set; }
		}

		public interface IPanelItemSpecs {

			int size { get; set; }

			[JsonConverter(typeof(PanelItemsConverter))]
			List<IPanelItemSpecs> items { get; set; }
		}

		[AssociatedClass(typeof(HoldButton))]
		[JsonStringTypeValue("hold")]
		public class HoldButtonSpecs : IPanelItemSpecs {

			public int size { get; set; }
			public List<IPanelItemSpecs> items { get; set; }
			public TextSpecs text { get; set; }

			[JsonProperty("command")]
			public string keyCommand { get; set; }

			[JsonConverter(typeof(BrushConverter))]
			public System.Windows.Media.Brush color { get; set; }

			[JsonConverter(typeof(BorderSpecsConverter))]
			public BorderSpecs border { get; set; }
		}

		[AssociatedClass(typeof(TapButton))]
		[JsonStringTypeValue("tap")]
		public class TapButtonSpecs : IPanelItemSpecs {

			public int size { get; set; }
			public List<IPanelItemSpecs> items { get; set; }
			public TextSpecs text { get; set; }

			[JsonProperty("command")]
			public string keyCommand { get; set; }

			[JsonConverter(typeof(BrushConverter))]
			public System.Windows.Media.Brush color { get; set; }

			[JsonConverter(typeof(BorderSpecsConverter))]
			public BorderSpecs border { get; set; }
		}

		[AssociatedClass(typeof(CustomToggleButton))]
		[JsonStringTypeValue("toggle")]
		public class ToggleButtonSpecs : IPanelItemSpecs {

			public int size { get; set; }
			public List<IPanelItemSpecs> items { get; set; }
			public TextSpecs text { get; set; }

			[JsonProperty("command")]
			public string keyCommand { get; set; }

			[JsonConverter(typeof(BrushConverter))]
			public System.Windows.Media.Brush color { get; set; }

			[JsonConverter(typeof(BorderSpecsConverter))]
			public BorderSpecs border { get; set; }
		}

		[AssociatedClass(typeof(CustomStackPanel))]
		[JsonStringTypeValue("pivot")]
		public class PivotSpecs : IPanelItemSpecs {

			public int size { get; set; }
			public List<IPanelItemSpecs> items { get; set; }
		}

		[AssociatedClass(typeof(MoveButton))]
		[JsonStringTypeValue("move")]
		public class MoveButtonSpecs : IPanelItemSpecs {

			public int size { get; set; }
			public List<IPanelItemSpecs> items { get; set; }
			public TextSpecs text { get; set; }

			[JsonConverter(typeof(BrushConverter))]
			public System.Windows.Media.Brush color { get; set; }

			[JsonConverter(typeof(BorderSpecsConverter))]
			public BorderSpecs border { get; set; }
		}

		public class TextSpecs {

			public string value { get; set; }
			public double size { get; set; }

			[JsonConverter(typeof(BrushConverter))]
			public System.Windows.Media.Brush color { get; set; }

			[JsonConverter(typeof(FontFamilyConverter))]
			public System.Windows.Media.FontFamily font { get; set; }
		}
	}

	#endregion -- Internal Classes (Layout POJO) --
}
