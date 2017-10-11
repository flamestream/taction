using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Media;

namespace Taction.JsonConverter {

	internal class FontFamilyConverter : Newtonsoft.Json.JsonConverter {

		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType) {

			return objectType == typeof(FontFamily);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

			throw new InvalidOperationException("Not supported");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

			var value = serializer.Deserialize<string>(reader);

			// Compute all possible font name strings
			var fontNames = new List<string>();
			foreach (var entry in value.Split(','))
				fontNames.Add(GetFontString(entry));

			// Compute full font family string
			var fontFamilyString = string.Join(", ", fontNames);

			return new FontFamily(fontFamilyString);
		}

		private static string GetFontString(string name) {

			// Sanity
			name = name.Trim();

			// Font name is file check
			if (!name.StartsWith("./"))
				return name; // Installed font

			// Sanity
			name = Path.GetFileName(name);

			// File existence check
			var config = App.Instance.Config;
			var fontFile = Path.Combine(App.FontDir, name);
			var zip = config.LoadedLayoutZip;

			if (!File.Exists(fontFile)) {

				// In zip check
				if (zip == null) {

					config.LoadLayoutErrors.Add(string.Format("Font '{0}': File is not found at {1}", name, fontFile));
					return null;
				}

				// Extract font from zip
				var entry = zip.Entries.First(e => e.Name == name);
				if (entry == null) {

					config.LoadLayoutErrors.Add(string.Format("Font '{0}': File is not found in bundle", name));
					return null;
				}

				entry.ExtractToFile(fontFile);
			}

			// Keep file on clean up step
			config.LoadedFonts.Add(fontFile);

			// Get font family name
			string fontName;
			using (var pfc = new PrivateFontCollection()) {

				pfc.AddFontFile(fontFile);
				fontName = pfc.Families.First().Name;
			}
			// Ensure release lock; .NET bug
			WinApi.RemoveFontResourceEx(fontFile, (int)WinApi.FR.FR_PRIVATE, IntPtr.Zero);

			// Create path
			var localUri = new UriBuilder(new Uri(fontFile).AbsoluteUri) {
				Fragment = fontName
			};

			return localUri.ToString();
		}
	}
}
