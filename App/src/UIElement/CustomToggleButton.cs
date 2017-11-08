using System;
using System.Windows;

namespace Taction.UIElement {

	partial class CustomToggleButton : StyleToggleButton {

		internal KeyCommand KeyCommand { get; set; }

		public CustomToggleButton(ToggleButtonSpecs specs) : base(specs.Style) {

			SetResourceReference(StyleProperty, typeof(StyleToggleButton));

			KeyCommand = specs.KeyCommand;

			// Event binding
			Checked += HandleChecked;
			Unchecked += HandleUnchecked;
		}

		protected void HandleChecked(Object sender, RoutedEventArgs e) {

			App.Instance.InputSimulator.SimulateKeyDown(KeyCommand);
		}

		protected void HandleUnchecked(Object sender, RoutedEventArgs e) {

			App.Instance.InputSimulator.SimulateKeyUp(KeyCommand);
		}
	}
}
