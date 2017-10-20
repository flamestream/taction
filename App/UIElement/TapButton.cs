using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Taction.UIElement {

	/// <summary>
	/// A button that executes key command only once.
	/// </summary>
	internal class TapButton : CustomButton {

		internal KeyCommand KeyCommand { set; get; }

		public TapButton(IPanelItemSpecs specs) {

			var s = (TapButtonSpecs)specs;
			KeyCommand = s.KeyCommand;
		}

		protected override void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);

			// Set activation flag
			Tag = true;

			if (KeyCommand == null)
				return;

			App.Instance.InputSimulator.SimulateKeyPress(KeyCommand);
		}
	}
}
