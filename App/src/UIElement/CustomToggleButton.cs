using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Taction.UIElement {

	partial class CustomToggleButton : StyleToggleButton {

		internal KeyCommand KeyCommand { get; set; }

		public CustomToggleButton(IPanelItemSpecs specs) {

			var s = (ToggleButtonSpecs)specs;
			KeyCommand = s.KeyCommand;

			// Event binding
			Checked += HandleChecked;
			Unchecked += HandleUnchecked;

			SetResourceReference(StyleProperty, typeof(StyleToggleButton));
		}

		protected void HandleChecked(Object sender, RoutedEventArgs e) {

			App.Instance.InputSimulator.SimulateKeyDown(KeyCommand);
		}

		protected void HandleUnchecked(Object sender, RoutedEventArgs e) {

			App.Instance.InputSimulator.SimulateKeyUp(KeyCommand);
		}
	}
}
