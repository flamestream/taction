using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WindowsInput;
using VirtualKeyCode = WindowsInput.Native.VirtualKeyCode;

namespace Taction {

	internal class InputSimulatorHelper {

		private static readonly VirtualKeyCode[] ModifierKeyCodes = {
			VirtualKeyCode.CONTROL,
			VirtualKeyCode.LCONTROL,
			VirtualKeyCode.RCONTROL,
			VirtualKeyCode.MENU,
			VirtualKeyCode.LMENU,
			VirtualKeyCode.RMENU,
			VirtualKeyCode.SHIFT,
			VirtualKeyCode.LSHIFT,
			VirtualKeyCode.RSHIFT,
			VirtualKeyCode.LWIN,
			VirtualKeyCode.RWIN
		};

		private InputSimulator inputSimulator;

		public InputSimulatorHelper() {

			inputSimulator = new InputSimulator();
		}

		/// <summary>
		/// Simulate KeyPress events of passed key code list.
		/// </summary>
		/// <param name="keyCodes">List of key codes to simulate event</param>
		public void SimulateKeyPress(KeyCommand keyCommand) {

			if (keyCommand == null)
				return;

			var keyCodes = keyCommand.keyCodes;
			if (keyCodes == null)
				return;

			var modifierKeyCodes = keyCodes.Intersect(ModifierKeyCodes);
			var normalKeyCodes = keyCodes.Except(modifierKeyCodes);

			Debug.WriteLine(string.Format("KEY PRESS {0}", string.Join("+", keyCodes)));
			inputSimulator.Keyboard.ModifiedKeyStroke(modifierKeyCodes, normalKeyCodes);
		}

		/// <summary>
		/// Simulate KeyDown events of passed key code list.
		/// </summary>
		/// <param name="keyCodes">List of key codes to simulate event</param>
		public void SimulateKeyDown(KeyCommand keyCommand) {

			if (keyCommand == null)
				return;

			var keyCodes = keyCommand.keyCodes;
			if (keyCodes == null)
				return;

			foreach (var keyCode in keyCodes) {

				Debug.WriteLine(string.Format("KEY DOWN {0}", keyCode));
				inputSimulator.Keyboard.KeyDown(keyCode);
			}
		}

		/// <summary>
		/// Simulate KeyUp events of passed key code list.
		/// Note that the list is processed in reverse order.
		/// </summary>
		/// <param name="keyCodes">List of key codes to simulate event</param>
		public void SimulateKeyUp(KeyCommand keyCommand) {

			if (keyCommand == null)
				return;

			var keyCodes = keyCommand.keyCodes;
			if (keyCodes == null)
				return;

			for (var i = keyCodes.Count; --i >= 0;) {

				var keyCode = keyCodes[i];
				Debug.WriteLine(string.Format("KEY UP {0}", keyCode));
				inputSimulator.Keyboard.KeyUp(keyCode);
			}
		}

		/// <summary>
		/// Parse key comamand.
		/// Throws if format is invalid.
		/// </summary>
		/// <param name="keyCommand">User-inputted key command</param>
		/// <returns>
		/// A KeyCommand object, or null if invalid.
		/// </returns>
		public static KeyCommand ParseKeyCommand(string keyCommand) {

			var keyCodes = new List<VirtualKeyCode>();
			var keyIds = keyCommand.Split(' ');

			foreach (var keyId in keyIds) {

				// Valid Key ID check
				var enumType = typeof(VirtualKeyCode);
				if (!Enum.IsDefined(enumType, keyId))
					return null;

				var virtualKeyCode = (VirtualKeyCode)Enum.Parse(enumType, keyId);
				keyCodes.Add(virtualKeyCode);
			}

			return new KeyCommand {
				keyCodes = keyCodes
			};
		}
	}

	internal class KeyCommand {
		public List<VirtualKeyCode> keyCodes;
	}
}
