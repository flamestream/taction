using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace Taction.JsonConverter {

	internal class KeyCommandListJsonConverter : Newtonsoft.Json.JsonConverter {

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

			var errMsg = AttemptParseValue(str, out var keyCodes);
			if (errMsg != null) {

				App.Instance.Config.LoadLayoutErrors.Add(string.Format("Key command input '{0}': {1}", str, errMsg));
				return null;
			}

			var o = new KeyCommand {
				KeyCodes = keyCodes
			};

			return o;
		}

		public static string AttemptParseValue(string input, out List<VirtualKeyCode> keyCodes) {

			var errMsgs = new List<string>();
			keyCodes = new List<VirtualKeyCode>();

			// Sanity
			input = input.Trim();

			var keyIds = input.Split(' ');
			foreach (var keyId in keyIds) {

				// Sanity
				if (keyId.Length == 0)
					continue;

				// Valid Key ID check
				var enumType = typeof(VirtualKeyCode);
				if (!Enum.IsDefined(enumType, keyId)) {

					errMsgs.Add(string.Format("Key ID '{0}' is not valid", keyId));
					continue;
				}

				var virtualKeyCode = (VirtualKeyCode)Enum.Parse(enumType, keyId);
				keyCodes.Add(virtualKeyCode);
			}

			// Error exists check
			if (errMsgs.Count != 0)
				return string.Join("; ", errMsgs);

			return null;
		}
	}
}
