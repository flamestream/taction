using Newtonsoft.Json;
using System;

namespace ArtTouchPanel {

	partial class Config {

		/// <summary>
		/// Covert JSON string value to enumerator
		/// </summary>
		private class PanelAlignmentConverter : JsonConverter {

			public override bool CanWrite => false;
			public override bool CanRead => true;

			public override bool CanConvert(Type objectType) {

				return objectType == typeof(PanelAlignment);
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

				throw new InvalidOperationException("Not supported");
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

				var o = default(PanelAlignment);

				if (reader.TokenType == JsonToken.String) {

					var alignment = serializer.Deserialize<string>(reader);
					switch (alignment) {

						case "horizontal":
							o = PanelAlignment.Horizontal;
							break;

						case "vertical":
							o = PanelAlignment.Vertical;
							break;
					}
				}

				return o;
			}
		}
	}
}
