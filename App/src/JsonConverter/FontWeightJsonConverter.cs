using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Taction.JsonConverter {

	internal class FontWeightJsonConverter : Newtonsoft.Json.JsonConverter {

		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType) {

			return objectType == typeof(FontWeight);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

			throw new InvalidOperationException("Not supported");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

			var o = default(FontWeight?);

			if (reader.TokenType == JsonToken.String) {

				var value = serializer.Deserialize<string>(reader);
				o = (FontWeight)new FontWeightConverter().ConvertFromString(value);
			}

			return o;
		}
	}
}
