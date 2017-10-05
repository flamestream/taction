using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace Taction.JsonConverter {

	internal class KeyCommandListConverter : Newtonsoft.Json.JsonConverter {

		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType) {

			return objectType == typeof(KeyCommand);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

			throw new InvalidOperationException("Not supported");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

			var str = serializer.Deserialize<string>(reader);

			if (!TryParseValue(str, out var keyCodes))
				throw new FormatException("Invalid key found");

			var o = new KeyCommand {
				KeyCodes = keyCodes
			};

			return o;
		}

		public static bool TryParseValue(string input, out List<VirtualKeyCode> keyCodes) {

			keyCodes = new List<VirtualKeyCode>();
			var keyIds = input.Split(' ');

			foreach (var keyId in keyIds) {

				// Valid Key ID check
				var enumType = typeof(VirtualKeyCode);
				if (!Enum.IsDefined(enumType, keyId))
					return false;

				var virtualKeyCode = (VirtualKeyCode)Enum.Parse(enumType, keyId);
				keyCodes.Add(virtualKeyCode);
			}

			return true;
		}
	}
}
