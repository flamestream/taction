using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Taction.Config;

namespace Taction.CustomUIElement {

	/// <summary>
	/// A button that supports holding down to execute key command.
	/// </summary>
	internal class HoldButton : Button {

		private App App => (App)Application.Current;
		internal KeyCommand KeyCommand { set; get; }

		public HoldButton(IPanelItemSpecs specs, StackPanel panel = null) {

			var s = (HoldButtonSpecs)specs;

			this.KeyCommand = InputSimulatorHelper.ParseKeyCommand(s.keyCommand);

			this.Content = s.text != null ?
				s.text :
				s.keyCommand;

			if (panel == null || panel.Orientation == Orientation.Vertical)
				this.Height = s.size;
			else
				this.Width = s.size;
		}

		protected override void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);

			// Style change
			this.FontWeight = FontWeight.FromOpenTypeWeight(500);

			// Set activation flag
			this.Tag = true;

			App.inputSimulator.SimulateKeyDown(this.KeyCommand);
		}

		protected override void OnTouchLeave(TouchEventArgs e) {

			// Activation flag check
			// @NOTE Needed because TouchLeave can be triggered without TouchDown
			if (this.Tag == null) return;
			this.Tag = null;

			// Style change
			this.FontWeight = FontWeight.FromOpenTypeWeight(200);

			App.inputSimulator.SimulateKeyUp(this.KeyCommand);
		}

	}
}
