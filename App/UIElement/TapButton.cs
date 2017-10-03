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

		private App App => (App)Application.Current;
		internal KeyCommand KeyCommand { set; get; }

		public TapButton(IPanelItemSpecs specs) {

			var s = (TapButtonSpecs)specs;

			this.KeyCommand = InputSimulatorHelper.ParseKeyCommand(s.keyCommand);
		}

		protected override async void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);

			// Style change
			this.FontWeight = FontWeights.Bold;

			// Set activation flag
			this.Tag = true;

			if (this.KeyCommand == null)
				return;

			App.inputSimulator.SimulateKeyPress(KeyCommand);

			await Task.Delay(100);

			// Style change
			this.FontWeight = FontWeights.Normal;
		}
	}
}
