using System;
using System.Windows;
using System.Windows.Input;

namespace Taction {

	/// <summary>
	/// A button that executes key command only once.
	/// </summary>
	internal class ToggleButton : System.Windows.Controls.Primitives.ToggleButton {

		private App App => (App)Application.Current;
		internal KeyCommand KeyCommand { set; get; }

		public ToggleButton() {

			this.Checked += HandleChecked;
			this.Unchecked += HandleUnchecked;
		}

		protected void HandleChecked(Object sender, RoutedEventArgs e) {

			App.inputSimulator.SimulateKeyDown(KeyCommand);
		}

		protected void HandleUnchecked(Object sender, RoutedEventArgs e) {

			App.inputSimulator.SimulateKeyUp(KeyCommand);
		}
	}
}
