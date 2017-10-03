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

				var value = serializer.Deserialize<JObject>(reader);

				var o = new Thickness();

				if (value.TryGetValue("top", out var top))
					o.Top = (double)top;

				if (value.TryGetValue("right", out var right))
					o.Right = (double)right;

				if (value.TryGetValue("bottom", out var bottom))
					o.Bottom = (double)bottom;

				if (value.TryGetValue("left", out var left))
					o.Left = (double)left;

				return o;
			}
		}
	}
}
