using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using static Taction.Config;

namespace Taction.CustomUIElement {

	/// <summary>
	/// A button that executes key command only once.
	/// </summary>
	internal class CustomToggleButton : ToggleButton {

		private App App => (App)Application.Current;
		internal KeyCommand KeyCommand { set; get; }

		public CustomToggleButton(IPanelItemSpecs specs, StackPanel panel = null) {

			var s = (ToggleButtonSpecs)specs;

			this.KeyCommand = InputSimulatorHelper.ParseKeyCommand(s.keyCommand);

			this.Content = s.text != null ?
				s.text.value :
				s.keyCommand;

			if (panel == null || panel.Orientation == Orientation.Vertical)
				this.Height = s.size;
			else
				this.Width = s.size;

			// Event binding
			this.Checked += HandleChecked;
			this.Unchecked += HandleUnchecked;
		}

		protected void HandleChecked(Object sender, RoutedEventArgs e) {

			this.FontWeight = FontWeights.Bold;
			App.inputSimulator.SimulateKeyDown(KeyCommand);
		}

		protected void HandleUnchecked(Object sender, RoutedEventArgs e) {

			this.FontWeight = FontWeights.Normal;
			App.inputSimulator.SimulateKeyUp(KeyCommand);
		}
	}
}
