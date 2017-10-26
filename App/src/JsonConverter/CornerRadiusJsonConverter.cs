using Newtonsoft.Json;
using System;
using System.Windows;

namespace Taction.JsonConverter {

	internal class CornerRadiusJsonConverter : Newtonsoft.Json.JsonConverter {

		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType) {

			return objectType == typeof(CornerRadius);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

			throw new InvalidOperationException("Not supported");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

			var value = serializer.Deserialize<string>(reader);
			return FromString(value);
		}

		public static CornerRadius FromString(string input) {

			var o = default(CornerRadius);

			if (input != null) {

				var c = new CornerRadiusConverter();
				o = (CornerRadius)c.ConvertFromString(input);
			}

			return o;
		}
	}
}
