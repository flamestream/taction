using Newtonsoft.Json;
using System;
using System.Windows.Controls;

namespace Taction {

	partial class Config {

		/// <summary>
		/// Covert JSON string value to enumerator
		/// </summary>
		private class PanelOrientationConverter : JsonConverter {

			public override bool CanWrite => false;
			public override bool CanRead => true;

			public override bool CanConvert(Type objectType) {

				return objectType == typeof(Orientation);
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

				throw new InvalidOperationException("Not supported");
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

				var o = default(Orientation);

				if (reader.TokenType == JsonToken.String) {

					var alignment = serializer.Deserialize<string>(reader);
					switch (alignment) {

						case "horizontal":
							o = Orientation.Horizontal;
							break;

						case "vertical":
							o = Orientation.Vertical;
							break;
					}
				}

				return o;
			}
		}
	}
}
