using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace app {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		private InputSimulatorHelper inputSimulator;
		private GlobalMouseHook globalMouseHook;
		private bool isPassthrough;

		public MainWindow() {

			InitializeComponent();
			inputSimulator = new InputSimulatorHelper();
			globalMouseHook = new GlobalMouseHook(this);
			globalMouseHook.OnMouseLeaveBoundaries += HandleMouseLeaveBoundaries;
		}

		private void LoadConfig() {

		}

		private void SetPassthrough(float value) {

			// Clamp value
			if (value < 0) value = 0;
			else if (value > 1) value = 1;

			if (value == 0) {

				SetPassthrough(true);
				return;
			}

			var isWanted = value != 1;
			if (isWanted && !isPassthrough) {

				this.Opacity = value;
				Win32.SetWindowExTransparent(this, true);
				isPassthrough = true;

			} else if (!isWanted && isPassthrough) {

				this.Opacity = value;
				Win32.SetWindowExTransparent(this, false);
				isPassthrough = false;
			}
		}

		private void SetPassthrough(bool isWanted) {

			if (isWanted && !isPassthrough) {

				this.Visibility = Visibility.Hidden;
				isPassthrough = true;

			} else if (!isWanted && isPassthrough) {

				this.Visibility = Visibility.Visible;
				isPassthrough = false;
			}
		}

		private void HandleMouseLeaveBoundaries(object sender, GlobalMouseHook.EventArgs e) {

			SetPassthrough(false);
		}

		protected override void OnClosing(CancelEventArgs e) {

			base.OnClosing(e);
			globalMouseHook.Dispose();
		}

		protected override void OnActivated(EventArgs e) {

			base.OnActivated(e);
			Win32.CancelActivation(this);
		}

		private void Button_TouchDown(object sender, TouchEventArgs e) {

			Button btn = (Button)sender;
			btn.FontWeight = FontWeight.FromOpenTypeWeight(500);

			var keyCommand = (string)btn.Tag;
			var parsedKeyCommand = InputSimulatorHelper.ParseKeyCommand(keyCommand);

			if (parsedKeyCommand.isPressWanted)
				inputSimulator.SimulateKeyPress(parsedKeyCommand.keyCodes);
			else
				inputSimulator.SimulateKeyDown(parsedKeyCommand.keyCodes);

			e.Handled = true;
		}

		private void Button_TouchUp(object sender, TouchEventArgs e) {

			Button btn = (Button)sender;
			btn.FontWeight = FontWeight.FromOpenTypeWeight(200);

			var keyCommand = (string)btn.Tag;
			var parsedKeyCommand = InputSimulatorHelper.ParseKeyCommand(keyCommand);

			if (!parsedKeyCommand.isPressWanted)
				inputSimulator.SimulateKeyUp(parsedKeyCommand.keyCodes);

			e.Handled = true;
		}

		private void Window_PreviewMouseMove(object sender, MouseEventArgs e) {

			// Touch/Pen promotion check
			// @NOTE Weirdly, handling PreviewTouchMove won't prevent this event from happening
			if (e.StylusDevice != null)
				return;

			Debug.WriteLine("Hide Panel (mouse)");
			SetPassthrough(true);
		}

		private void Window_PreviewStylusInAirMove(object sender, StylusEventArgs e) {

			Debug.WriteLine("Hide Panel (pen)");
			SetPassthrough(true);

			// Prevent mouse move event
			e.Handled = true;
		}

		private void Window_PreviewTouchMove(object sender, TouchEventArgs e) {

			// Doesn't prevent duplicate, but whatever...
			e.Handled = true;
		}
	}
}
