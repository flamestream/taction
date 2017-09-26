using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArtTouchPanel {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		private InputSimulatorHelper inputSimulator { get; set; }
		private GlobalMouseHook globalMouseHook { get; set; }
		private bool isPassthrough { get; set; }
		private Config config { get; set; }
		private Dictionary<Button, KeyCommand> buttonCommands { get; set; }

		public MainWindow() {

			InitializeComponent();
			inputSimulator = new InputSimulatorHelper();
			globalMouseHook = new GlobalMouseHook(this);
			globalMouseHook.OnMouseLeaveBoundaries += HandleMouseLeaveBoundaries;
			buttonCommands = new Dictionary<Button, KeyCommand>();

			string errMsg = null;
			try {
				config = Config.Load();
			} catch (FileNotFoundException e) {
				errMsg = string.Format("{0}:\n{1}", "Config load error", e.Message);
			} catch (FormatException e) {
				errMsg = string.Format("{0}:\n{1}", "Config validation error", e.Message);
			} catch (Newtonsoft.Json.JsonReaderException e) {
				errMsg = string.Format("{0}:\n{1}", "Config syntax error", e.Message);
			} catch (Exception e) {
				errMsg = string.Format("{0}:\n{1}", e.GetType(), e.Message);
			}

			if (errMsg != null) {

				MessageBox.Show(errMsg, string.Format("{0} Error", Properties.Resources.AppName));
				Application.Current.Shutdown(1);
				return;
			}

			ReloadLayout();
		}

		private void ResetLayout() {

			globalMouseHook.Disable();
			SetPassthrough(false);
			panel.Children.Clear();
			buttonCommands.Clear();
		}

		private void ReloadLayout() {

			ResetLayout();

			if (config == null)
				return;

			this.Visibility = Visibility.Visible;
			Designer.GenerateLayout(this);
		}

		/// <summary>
		/// Enable or disable pass-though using config values for opacity.
		/// </summary>
		/// <param name="isWanted">Flag indicating if pass-through is wanted</param>
		private void SetPassthrough(bool isWanted) {

			float opacity = isWanted ?
				config.data.opacityHide :
				config.data.opacity;

			SetPassthrough(isWanted, opacity);
		}

		/// <summary>
		/// Enable or disable pass-though.
		/// </summary>
		/// <param name="isWanted">Flag indicating if pass-through is wanted</param>
		/// <param name="opacity">Opacity value to set</param>
		private void SetPassthrough(bool isWanted, float opacity) {

			if (isWanted && !isPassthrough) {

				if (opacity == 0)
					this.Visibility = Visibility.Hidden;
				else
					this.Opacity = opacity;

				Win32.SetWindowExTransparent(this, true);
				isPassthrough = true;
				globalMouseHook.Enable();

			} else if (!isWanted && isPassthrough) {

				this.Opacity = opacity;
				this.Visibility = Visibility.Visible;

				Win32.SetWindowExTransparent(this, false);
				isPassthrough = false;
			}
		}

		private void HandleMouseLeaveBoundaries(object sender, GlobalMouseHook.EventArgs e) {

			globalMouseHook.Disable();
			SetPassthrough(false);
		}

		protected override void OnClosing(CancelEventArgs e) {

			base.OnClosing(e);

			if (globalMouseHook != null)
				globalMouseHook.Dispose();
		}

		protected override void OnActivated(EventArgs e) {

			base.OnActivated(e);
			Win32.CancelActivation(this);
		}

		private void Button_TouchDown(object sender, TouchEventArgs e) {

			e.Handled = true;

			Button btn = (Button)sender;
			btn.FontWeight = FontWeight.FromOpenTypeWeight(500);

			KeyCommand keyCommand;
			if (!buttonCommands.TryGetValue(btn, out keyCommand))
				return;

			if (keyCommand.isPressWanted)
				inputSimulator.SimulateKeyPress(keyCommand.keyCodes);
			else
				inputSimulator.SimulateKeyDown(keyCommand.keyCodes);
		}

		private void Button_TouchUp(object sender, TouchEventArgs e) {

			e.Handled = true;

			Button btn = (Button)sender;
			btn.FontWeight = FontWeight.FromOpenTypeWeight(200);

			KeyCommand keyCommand;
			if (!buttonCommands.TryGetValue(btn, out keyCommand))
				return;

			if (!keyCommand.isPressWanted)
				inputSimulator.SimulateKeyUp(keyCommand.keyCodes);
		}

		private void Window_PreviewMouseMove(object sender, MouseEventArgs e) {

			// Touch/Pen promotion check
			// @NOTE Weirdly, handling PreviewTouchMove won't prevent this event from happening
			if (e.StylusDevice != null)
				return;

			if (!config.data.disableHide) {

				Debug.WriteLine("Hide Panel (mouse)");
				SetPassthrough(true);
			}
		}

		private void Window_PreviewStylusInAirMove(object sender, StylusEventArgs e) {

			// Prevent mouse move event
			e.Handled = true;

			if (!config.data.disableHide) {

				Debug.WriteLine("Hide Panel (pen)");
				SetPassthrough(true);
			}
		}
	}
}
