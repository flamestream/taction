using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Taction.JsonConverter {

	internal class ContentConverter : Newtonsoft.Json.JsonConverter {

		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType) {

			return objectType == typeof(Object);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

			throw new InvalidOperationException("Not supported");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

			var o = default(Object);

			var json = serializer.Deserialize<JObject>(reader);
			var type = json.Value<string>("type");

			switch (type) {

				case "text":

					var value = json.Value<string>("value");
					o = value;

					break;

				case "image":

					var source = json.Value<string>("source");
					var bitmap = GetBitmap(source);

					var colorize = json.Value<string>("colorize");
					if (colorize != null) {

						if (!BrushConverter.TryGetColor(colorize, out var color)) {

							App.Instance.Config.LoadLayoutErrors.Add(string.Format("Colorize input '{0}': Invalid format", color));
							return o;
						}

						bitmap = Designer.ColorizeImage(bitmap, color);
					}

					var img = new Image {
						Source = bitmap
					};

					if (json.TryGetValue("opacity", out var jsonOpacity))
						img.Opacity = jsonOpacity.Value<double>();

					if (json.TryGetValue("stretch", out var jsonStretch))
						img.Stretch = GetStretch(jsonStretch.Value<string>());

					if (json.TryGetValue("margin", out var jsonMargin))
						img.Margin = ThicknessConverter.FromString(jsonMargin.Value<string>());

					o = img;

					break;
			}

			return o;
		}

		public static BitmapImage GetBitmap(string source) {

			var o = default(BitmapImage);

			// Read file
			var stream = new MemoryStream();
			var loadingStreams = App.Instance.Config.LoadingImageStreams;

			var zip = App.Instance.Config.LoadedLayoutZip;
			if (zip != null) { // Try zip

				var entry = zip.GetEntry(source);
				if (entry == null) {

					App.Instance.Config.LoadLayoutErrors.Add(string.Format("Image '{0}': File is not found", source));
					return o;
				}

				using (var s = entry.Open()) {

					s.CopyTo(stream);
				}

			} else { // Try local

				var file = Path.Combine(App.AppDataDir, source);
				if (!File.Exists(file)) {

					App.Instance.Config.LoadLayoutErrors.Add(string.Format("Image '{0}': File is not found", source));
					return o;
				}

				using (var s = File.Open(file, FileMode.Open, FileAccess.Read)) {

					s.CopyTo(stream);
				}
			}

			loadingStreams.Add(stream);

			// Create source
			var bitmap = new BitmapImage();
			try {

				bitmap.BeginInit();
				bitmap.StreamSource = stream;
				bitmap.EndInit();

				o = bitmap;

			} catch (NotSupportedException e) {

				App.Instance.Config.LoadLayoutErrors.Add(string.Format("Image '{0}': Format is not supported: {1}", source, e.Message));
			}

			return o;
		}

		public static Stretch GetStretch(string input) {

			var o = Stretch.Uniform;

			switch (input) {

				case "none":
					o = Stretch.None;
					break;

				case "fill":
					o = Stretch.Fill;
					break;

				case "uniform":
					o = Stretch.Uniform;
					break;

				case "uniform-fill":
					o = Stretch.UniformToFill;
					break;
			}

			return o;
		}
	}
}
