using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Media;

namespace Taction {

	partial class Config {

		private class ThicknessConverter : JsonConverter {

			public override bool CanWrite => false;
			public override bool CanRead => true;

			public override bool CanConvert(Type objectType) {

				return objectType == typeof(Thickness);
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

				throw new InvalidOperationException("Not supported");
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

				var o = default(Thickness);

				// Parse value
				var value = serializer.Deserialize<string>(reader);
				var values = value.Split(' ');

				// Valid check
				if (values.Length == 0 || values.Length > 4)
					return o;

				if (!double.TryParse(values[0], out var top))
					return o;

				if (values.Length <= 1 || !double.TryParse(values[1], out var right))
					right = top;

				if (values.Length <= 2 || !double.TryParse(values[2], out var bottom))
					bottom = top;

				if (values.Length <= 3 || !double.TryParse(values[3], out var left))
					left = right;

				o = new Thickness(left, top, right, bottom);

				return o;
			}
		}
	}
}
