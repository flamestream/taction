using Newtonsoft.Json;
using System;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
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
			var o = new FontFamily(value);

			return o;
		}

		private static bool TryLoadFont(string name, out FontFamily font) {

			font = default(FontFamily);

			// In zip check
			var zip = App.Instance.Config.LoadedLayoutZip;
			if (zip != null) {

				PrivateFontCollection pfc = null;

				// Loaded check
				var loadedFonts = App.Instance.Config.LoadedFonts;
				if (loadedFonts.ContainsKey(name)) {

					pfc = loadedFonts[name];

				} else {

					// Layout existence check
					var entry = zip.Entries.First(e => e.Name == name);
					if (entry != null) {

						// From https://social.msdn.microsoft.com/Forums/vstudio/en-US/69d386f1-6a22-47ae-bf73-7002260374ce/c-wpf-load-a-fontfamily-from-a-byte-array-or-a-memory-stream?forum=wpf
						var stream = entry.Open();
						byte[] streamData = new byte[stream.Length];
						stream.Read(streamData, 0, streamData.Length);

						var data = Marshal.AllocCoTaskMem(streamData.Length);
						Marshal.Copy(streamData, 0, data, streamData.Length);
						pfc = new PrivateFontCollection();
						pfc.AddMemoryFont(data, streamData.Length);
						loadedFonts.Add(name, pfc);
						Marshal.FreeCoTaskMem(data);
					}
				}

				if (pfc != null) {

					font = new FontFamily(pfc.Families[0].Name);
					return true;
				}
			}

			font = new FontFamily(name);
			return true;
		}
	}
}
