using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Taction {

	/// <summary>
	/// A button that executes key command only once.
	/// </summary>
	internal class TapButton : Button {

		private App App => (App)Application.Current;
		internal KeyCommand KeyCommand { set; get; }

		protected override async void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);

			// Style change
			this.FontWeight = FontWeight.FromOpenTypeWeight(500);

			// Set activation flag
			this.Tag = true;

			if (this.KeyCommand == null)
				return;

			App.inputSimulator.SimulateKeyPress(KeyCommand);

			await Task.Delay(100);

			// Style change
			this.FontWeight = FontWeight.FromOpenTypeWeight(200);
		}
	}
}
