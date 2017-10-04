using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static Taction.Config;

namespace Taction.JsonConverter {

	internal class PanelItemsConverter : Newtonsoft.Json.JsonConverter {

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

				// Use getter once
				var stringVsPanelItemSpecs = App.StringVsPanelItemSpecs;

				List<JObject> jsonArray = serializer.Deserialize<List<JObject>>(reader);
				foreach (var item in jsonArray) {

					var typeStr = item.Value<string>("type");
					if (stringVsPanelItemSpecs.ContainsKey(typeStr)) {
						var type = stringVsPanelItemSpecs[typeStr];
						o.Add((IPanelItemSpecs)serializer.Deserialize(new JTokenReader(item), type));
					}
				}
			}

			return o;
		}
	}
}
