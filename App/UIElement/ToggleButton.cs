using System;
using System.Windows;
using System.Windows.Controls;
using static Taction.Config;

namespace Taction.UIElement {

	/// <summary>
	/// A button that executes key command only once.
	/// </summary>
	internal class ToggleButton : System.Windows.Controls.Primitives.ToggleButton {

		private App App => (App)Application.Current;
		internal KeyCommand KeyCommand { set; get; }

		public ToggleButton(IPanelItemSpecs specs) {

			var s = (ToggleButtonSpecs)specs;

			this.KeyCommand = InputSimulatorHelper.ParseKeyCommand(s.keyCommand);

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
