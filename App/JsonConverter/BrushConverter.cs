using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Taction.JsonConverter {

	internal class BrushConverter : Newtonsoft.Json.JsonConverter {

		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType) {

			return objectType == typeof(Brush);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

			throw new InvalidOperationException("Not supported");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

			var o = default(Brush);

			var json = serializer.Deserialize<JObject>(reader);
			var type = json.Value<string>("type");

			switch (type) {

				case "image":

					var stretch = json.Value<string>("stretch");
					var tile = json.Value<string>("tile");
					var source = json.Value<string>("source");

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

					} catch (NotSupportedException e) {

						App.Instance.Config.LoadLayoutErrors.Add(string.Format("Image '{0}': Format is not supported: {1}", source, e.Message));
						return o;
					}

					o = new ImageBrush {
						Stretch = GetStretch(stretch),
						TileMode = GetTileMode(tile),
						ImageSource = bitmap,
					};
					break;

				case "solid":

					var value = json.Value<string>("value");
					if (!TryGetColor(value, out var color)) {

						App.Instance.Config.LoadLayoutErrors.Add(string.Format("Solid color input '{0}': Invalid format", value));
						return o;
					}

					o = new SolidColorBrush {
						Color = color
					};
					break;

				case "gradient":

					var brush = new LinearGradientBrush {
						EndPoint = new Point(0, 1),
						StartPoint = new Point(0, 0),
					};

					var values = json.Value<JToken>("values").Values<string>();
					foreach (var v in values) {

						var errMsg = AttemptGetGradientStop(v, out var gs);
						if (errMsg != null) {

							App.Instance.Config.LoadLayoutErrors.Add(string.Format("Gradient color input '{0}': {1}", v, errMsg));
							continue;
						}

						brush.GradientStops.Add(gs);
					}

					o = brush;
					break;
			}

			return o;
		}

		private Stretch GetStretch(string id) {

			var o = Stretch.None;

			switch (id) {
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

		private TileMode GetTileMode(string id) {

			var o = TileMode.None;

			switch (id) {
				case "normal":
					o = TileMode.Tile;
					break;

				case "flip-x":
					o = TileMode.FlipX;
					break;

				case "flip-y":
					o = TileMode.FlipY;
					break;

				case "flip-xy":
					o = TileMode.FlipXY;
					break;
			}

			return o;
		}

		private bool TryGetColor(string input, out Color color) {

			color = default(Color);

			try {

				color = (Color)ColorConverter.ConvertFromString(input);

			} catch (Exception e) {

				return false;
			}

			return true;
		}

		private string AttemptGetGradientStop(string input, out GradientStop o) {

			o = default(GradientStop);

			// Format check
			var parts = input.Split(' ');
			if (parts.Length != 2) {

				return "Format should consist of two values";
			}

			if (!double.TryParse(parts[0], out var offset)) {

				return "First value should be a valid number";
			}

			if (!TryGetColor(parts[1], out var color)) {

				return "Invalid color";
			}

			o = new GradientStop() {
				Color = color,
				Offset = offset
			};

			return null;
		}
	}
}
