using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Media;

namespace Taction.JsonConverter {

	internal class BrushJsonConverter : Newtonsoft.Json.JsonConverter {

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

			if (type != null)
				type = type.ToLower();

			switch (type) {

				case "image": {

						var stretch = json.Value<string>("stretch");
						var tile = json.Value<string>("tile");
						var source = json.Value<string>("source");

						var bitmap = ContentJsonConverter.GetBitmap(source);

						var colorize = json.Value<string>("colorize");
						if (colorize != null) {

							if (!TryGetColor(colorize, out var color)) {

								App.Instance.Config.LoadLayoutErrors.Add(string.Format("Colorize input '{0}': Invalid format", color));
								return o;
							}

							bitmap = Designer.ColorizeImage(bitmap, color);
						}

						o = new ImageBrush {
							Stretch = ContentJsonConverter.GetStretch(stretch),
							TileMode = GetTileMode(tile),
							ImageSource = bitmap,
						};
						break;
					}

				case "solid": {

						var value = json.Value<string>("value");
						if (!TryGetColor(value, out var color)) {

							App.Instance.Config.LoadLayoutErrors.Add(string.Format("Solid color input '{0}': Invalid format", value));
							return o;
						}

						o = new SolidColorBrush {
							Color = color
						};
						break;
					}

				case "gradient": {

						var angle = json.Value<double>("angle");
						var points = ConvertAngleToPoints(angle);

						var brush = new LinearGradientBrush {
							StartPoint = points.Item1,
							EndPoint = points.Item2,
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
			}

			return o;
		}

		public static TileMode GetTileMode(string id) {

			var o = TileMode.None;

			if (id != null)
				id = id.ToLower();

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

		public static bool TryGetColor(string input, out Color color) {

			color = default(Color);

			try {

				color = (Color)ColorConverter.ConvertFromString(input);

			} catch (Exception) {

				return false;
			}

			return true;
		}

		public static string AttemptGetGradientStop(string input, out GradientStop o) {

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

		public static Tuple<Point, Point> ConvertAngleToPoints(double angle) {

			// Simplify
			while (angle < 0) angle += 360;
			while (angle > 360) angle -= 360;

			// Do operation on quadrant 1
			var quadrantAngle = angle % 90;
			var radAngle = quadrantAngle * Math.PI / 180;
			var start = new Point(0, 1);
			var end = (quadrantAngle <= 45) ?
					new Point(Math.Tan(radAngle), 0) :
					new Point(1, 1 - 1 / Math.Tan(radAngle));

			// Rotate if needed
			Point s, e;
			if (angle < 90) {
				s = start;
				e = end;
			} else if (angle < 180) {
				s = new Point(end.Y, start.X);
				e = new Point(start.Y, end.X);
			} else if (angle < 270) {
				s = new Point(1 - start.X, end.Y);
				e = new Point(1 - end.X, start.Y);
			} else {
				s = new Point(start.Y, 1 - start.X);
				e = new Point(end.Y, 1 - end.X);
			}
			start = s;
			end = e;

			return new Tuple<Point, Point>(start, end);
		}
	}
}
