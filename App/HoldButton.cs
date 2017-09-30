using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Taction {

	/// <summary>
	/// A button that supports holding down to execute key command.
	/// </summary>
	internal class HoldButton : Button {

		private App App => (App)Application.Current;
		internal KeyCommand KeyCommand { set; get; }

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
