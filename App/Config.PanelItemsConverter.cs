using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Taction {

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

					// Prepare valid types
					// @TODO Would be nice to have to do this only once
					var panelItemSpecsType = new Dictionary<string, Type>();
					var candidatesAttr = (JsonPanelItemCandidatesAttribute)typeof(IPanelItemSpecs).GetCustomAttributes(typeof(JsonPanelItemCandidatesAttribute), true)[0];
					foreach (var t in candidatesAttr.values) {

						var typeAttr = (JsonPanelItemTypeAttribute)t.GetCustomAttributes(typeof(JsonPanelItemTypeAttribute), true)[0];
						var typeStr = typeAttr.value;
						panelItemSpecsType.Add(typeStr, t);
					}

					List<JObject> jsonArray = serializer.Deserialize<List<JObject>>(reader);
					foreach (var item in jsonArray) {

						var type = panelItemSpecsType[item.Value<string>("type")];
						if (type != null)
							o.Add((IPanelItemSpecs)serializer.Deserialize(new JTokenReader(item), type));
					}
				}

				return o;
			}
		}
	}
}
