using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Taction.Config;

namespace Taction.UIElement {

	/// <summary>
	/// A button that executes key command only once.
	/// </summary>
	internal class TapButton : Button {

		internal KeyCommand KeyCommand { set; get; }

		public TapButton(IPanelItemSpecs specs) {

			var s = (TapButtonSpecs)specs;

			 KeyCommand = InputSimulatorHelper.ParseKeyCommand(s.keyCommand);
		}

		protected override async void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);

			// Style change
			 FontWeight = FontWeights.Bold;

			// Set activation flag
			 Tag = true;

			if ( KeyCommand == null)
				return;

			App.Instance.InputSimulator.SimulateKeyPress(KeyCommand);

			await Task.Delay(100);

			// Style change
			 FontWeight = FontWeights.Normal;
		}
	}
}
