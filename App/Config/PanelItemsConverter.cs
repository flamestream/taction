using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ArtTouchPanel {

	partial class Config {

		/// <summary>
		/// Covert ambiguous JSON object into correct implemented IPanelItemSpecs
		/// </summary>
		private class PanelItemsConverter : JsonConverter {

			public override bool CanWrite => false;
			public override bool CanRead => true;

			public override bool CanConvert(Type objectType) {

				return objectType == typeof(IPanelItemSpecs);
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

				throw new InvalidOperationException("Not supported");
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

				var o = new List<IPanelItemSpecs>();

				if (reader.TokenType == JsonToken.StartArray) {

					List<JObject> jsonArray = serializer.Deserialize<List<JObject>>(reader);
					foreach (var item in jsonArray) {

						string type = item.Value<string>("type");
						switch (type) {

							case "button":

								o.Add(new ButtonSpecs {
									size = item.Value<int>("size"),
									keyCommand = item.Value<string>("command"),
									text = item.Value<string>("label")
								});
								break;

							case "panel":

								var orientation = serializer.Deserialize<Orientation>(new JTokenReader(item.GetValue("orientation")));

								var r = new JTokenReader(item.GetValue("items"));
								r.Read(); // Advance pointer to simulate behaviour
								var items = (List<IPanelItemSpecs>)ReadJson(r, objectType, null, serializer);

								o.Add(new PanelSpecs {
									size = item.Value<int>("size"),
									orientation = orientation,
									items = items
								});
								break;
						}
					}
				}

				return o;
			}
		}
	}
}
