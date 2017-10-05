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

					// Already loading check
					if (loadingStreams.ContainsKey(source)) {

						stream = loadingStreams[source];
					} else {

						var zip = App.Instance.Config.LoadedLayoutZip;
						if (zip != null) { // Try zip

							var entry = zip.GetEntry(source);
							if (entry == null)
								throw new Exception("Image file not found");

							using (var s = entry.Open()) {

								s.CopyTo(stream);
							}

						} else { // Try local

							var file = Path.Combine(App.AppDataDir, source);
							if (!File.Exists(file))
								throw new Exception("Image file not found");

							using (var s = File.Open(file, FileMode.Open, FileAccess.Read)) {

								s.CopyTo(stream);
							}
						}

						loadingStreams[source] = stream;
					}

					// Create source
					var bitmap = new BitmapImage();
					bitmap.BeginInit();
					bitmap.StreamSource = stream;
					bitmap.EndInit();

					o = new ImageBrush {
						Stretch = GetStretch(stretch),
						TileMode = GetTileMode(tile),
						ImageSource = bitmap,
					};
					break;

				case "solid":

					var value = json.Value<string>("value");

					o = new SolidColorBrush {
						Color = GetColor(value)
					};
					break;

				case "gradient":

					var brush = new LinearGradientBrush {
						EndPoint = new Point(0, 1),
						StartPoint = new Point(0, 0),
					};

					var values = json.Value<JToken>("values").Values<string>();
					foreach (var v in values) {

						// @TODO compile errors
						if (!TryGetGradientStop(v, out var gs)) {

							continue;
						}

						brush.GradientStops.Add(gs);
					}

					// @TODO compile errors

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

		private Color GetColor(string id) {

			// var o = default(Color);

			// Named check
			//var candidate = typeof(Colors).GetProperty(id, typeof(Color)).GetValue(null);
			//if (candidate != null)
			//	return (Color)candidate;

			// @TODO Color.Empty?
			return (Color)ColorConverter.ConvertFromString(id);
		}

		private bool TryGetGradientStop(string id, out GradientStop o) {

			o = default(GradientStop);

			// Format check
			var parts = id.Split(' ');
			if (parts.Length != 2)
				return false;

			if (!double.TryParse(parts[0], out var offset))
				return false;

			var color = GetColor(parts[1]);

			o = new GradientStop() {
				Color = color,
				Offset = offset
			};

			return true;
		}
	}
}
