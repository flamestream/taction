using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Taction.UIElement {

	/// <summary>
	/// A button that executes key command only once.
	/// </summary>
	partial class CustomToggleButton : ToggleButton {

		internal KeyCommand KeyCommand { get; set; }

		public CustomToggleButton(IPanelItemSpecs specs) {

			var s = (ToggleButtonSpecs)specs;
			KeyCommand = s.KeyCommand;

			// Event binding
			Checked += HandleChecked;
			Unchecked += HandleUnchecked;

			var res = new ResourceDictionary {
				Source = new Uri(@"pack://application:,,,/UIElement/Button.xaml")
			};
			Style = (Style)res["ToggleButtonStyle"];
		}

		protected void HandleChecked(Object sender, RoutedEventArgs e) {

			App.Instance.InputSimulator.SimulateKeyDown(KeyCommand);
		}

		protected void HandleUnchecked(Object sender, RoutedEventArgs e) {

			App.Instance.InputSimulator.SimulateKeyUp(KeyCommand);
		}
	}
}
