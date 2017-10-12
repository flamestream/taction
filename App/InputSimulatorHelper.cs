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

		private InputSimulator InputSimulatorInstance;

		public InputSimulatorHelper() {

			InputSimulatorInstance = new InputSimulator();
		}

		/// <summary>
		/// Simulate KeyPress events of passed key code list.
		/// </summary>
		/// <param name="keyCodes">List of key codes to simulate event</param>
		public void SimulateKeyPress(KeyCommand keyCommand) {

			if (keyCommand == null)
				return;

			var keyCodes = keyCommand.KeyCodes;
			if (keyCodes == null)
				return;

			var modifierKeyCodes = keyCodes.Intersect(ModifierKeyCodes);
			var normalKeyCodes = keyCodes.Except(modifierKeyCodes);

			Debug.WriteLine(string.Format("KEY PRESS {0}", string.Join("+", keyCodes)));
			InputSimulatorInstance.Keyboard.ModifiedKeyStroke(modifierKeyCodes, normalKeyCodes);
		}

		/// <summary>
		/// Simulate KeyDown events of passed key code list.
		/// </summary>
		/// <param name="keyCodes">List of key codes to simulate event</param>
		public void SimulateKeyDown(KeyCommand keyCommand) {

			if (keyCommand == null)
				return;

			var keyCodes = keyCommand.KeyCodes;
			if (keyCodes == null)
				return;

			foreach (var keyCode in keyCodes) {

				Debug.WriteLine(string.Format("KEY DOWN {0}", keyCode));
				InputSimulatorInstance.Keyboard.KeyDown(keyCode);
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

			var keyCodes = keyCommand.KeyCodes;
			if (keyCodes == null)
				return;

			for (var i = keyCodes.Count; --i >= 0;) {

				var keyCode = keyCodes[i];
				Debug.WriteLine(string.Format("KEY UP {0}", keyCode));
				InputSimulatorInstance.Keyboard.KeyUp(keyCode);
			}
		}
	}
}
