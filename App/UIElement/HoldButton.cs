using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Taction.Config;

namespace Taction.UIElement {

	/// <summary>
	/// A button that supports holding down to execute key command.
	/// </summary>
	internal class HoldButton : Button {

		internal KeyCommand KeyCommand { set; get; }

		public HoldButton(IPanelItemSpecs specs) {

			var s = (HoldButtonSpecs)specs;

			KeyCommand = s.KeyCommand;
		}

		protected override void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);

			// Style change
			FontWeight = FontWeights.Bold;

			// Set activation flag
			Tag = true;

			App.Instance.InputSimulator.SimulateKeyDown(KeyCommand);
		}

		protected override void OnTouchLeave(TouchEventArgs e) {

			// Activation flag check
			// @NOTE Needed because TouchLeave can be triggered without TouchDown
			if (Tag == null) return;
			Tag = null;

			// Style change
			FontWeight = FontWeights.Normal;

			App.Instance.InputSimulator.SimulateKeyUp(KeyCommand);
		}

	}
}
