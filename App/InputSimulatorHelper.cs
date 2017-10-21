using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
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
		private Dictionary<VirtualKeyCode, List<KeyCommand>> KeyDownCommandRegistry;
		private Timer KeyUpCheckTimer;

		public delegate void DetectedKeyUpEventHandler(object sender, DetectedKeyUpEventEventArgs args);

		public class DetectedKeyUpEventEventArgs {
			public List<KeyCommand> KeyCommands;
		}

		public event DetectedKeyUpEventHandler OnDetectedKeyUp;

		public InputSimulatorHelper() {

			InputSimulatorInstance = new InputSimulator();
			KeyDownCommandRegistry = new Dictionary<VirtualKeyCode, List<KeyCommand>>();
			KeyUpCheckTimer = new Timer(500);

			KeyUpCheckTimer.Elapsed += KeyUpCheckTimerTask;
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

				// Register command
				if (!KeyDownCommandRegistry.ContainsKey(keyCode))
					KeyDownCommandRegistry.Add(keyCode, new List<KeyCommand>());

				var list = KeyDownCommandRegistry[keyCode];
				list.Add(keyCommand);
				StartPolling();

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

				// Unregister Command
				if (KeyDownCommandRegistry.ContainsKey(keyCode)) {

					var keyCommands = KeyDownCommandRegistry[keyCode];
					keyCommands.Remove(keyCommand);
				}
			}
		}

		public void ClearKeyDownCommandRegistry() {

			KeyDownCommandRegistry.Clear();
		}

		public void StartPolling() {

			Debug.WriteLine("Start polling");

			if (KeyUpCheckTimer == null)
				return;

			if (KeyDownCommandRegistry.Count == 0)
				return;

			KeyUpCheckTimer.Start();
		}

		/// <summary>
		/// Task used to periodically check for key up downed by togglers.
		/// Seems like a better idea than a global key listener.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyUpCheckTimerTask(object sender, ElapsedEventArgs e) {

			// Need check
			if (KeyDownCommandRegistry.Count == 0) {

				KeyUpCheckTimer.Stop();
				return;
			}

			// Registered listener check
			if (OnDetectedKeyUp == null)
				return;

			// Scan up keys
			var detectedKeyUpCode = new HashSet<VirtualKeyCode>();
			var detectedKeyUpKeyCommands = new HashSet<KeyCommand>();
			foreach (var entry in KeyDownCommandRegistry) {

				var keyCode = entry.Key;
				if (!InputSimulatorInstance.InputDeviceState.IsKeyUp(keyCode))
					continue;

				var keyCommands = entry.Value;

				detectedKeyUpCode.Add(keyCode);
				detectedKeyUpKeyCommands.UnionWith(keyCommands);
			}

			// Remove from registry
			foreach (var keyCode in detectedKeyUpCode) {
				KeyDownCommandRegistry.Remove(keyCode);
			}

			// Detect count check
			if (detectedKeyUpKeyCommands.Count == 0)
				return;

			// Trigger event on main thread
			Application.Current.Dispatcher.Invoke(() => {

				OnDetectedKeyUp.Invoke(null, new DetectedKeyUpEventEventArgs {
					KeyCommands = new List<KeyCommand>(detectedKeyUpKeyCommands)
				});
			}, DispatcherPriority.ContextIdle);
		}
	}
}
