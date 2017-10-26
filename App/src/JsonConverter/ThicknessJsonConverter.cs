using Newtonsoft.Json;
using System;
using System.Windows;

namespace Taction.JsonConverter {

	internal class ThicknessJsonConverter : Newtonsoft.Json.JsonConverter {

		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType) {

			return objectType == typeof(Thickness);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

			throw new InvalidOperationException("Not supported");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

			var value = serializer.Deserialize<string>(reader);
			return FromString(value);
		}

		public static Thickness FromString(string input) {

			var o = default(Thickness);

			if (input == null)
				return o;

			// Parse value
			var values = input.Split(' ');

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
