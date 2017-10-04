using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Taction.Config;

namespace Taction.UIElement {

	/// <summary>
	/// A button that supports holding down to execute key command.
	/// </summary>
	internal class HoldButton : Button {

		private App App => (App)Application.Current;
		internal KeyCommand KeyCommand { set; get; }

		public HoldButton(IPanelItemSpecs specs) {

			var s = (HoldButtonSpecs)specs;

			 KeyCommand = InputSimulatorHelper.ParseKeyCommand(s.keyCommand);
		}

		protected override void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);

			// Style change
			 FontWeight = FontWeights.Bold;

			// Set activation flag
			 Tag = true;

			App.InputSimulator.SimulateKeyDown( KeyCommand);
		}

		protected override void OnTouchLeave(TouchEventArgs e) {

			// Activation flag check
			// @NOTE Needed because TouchLeave can be triggered without TouchDown
			if ( Tag == null) return;
			 Tag = null;

			// Style change
			 FontWeight = FontWeights.Normal;

			App.InputSimulator.SimulateKeyUp( KeyCommand);
		}

	}
}
