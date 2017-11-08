using System.Windows.Input;

namespace Taction.UIElement {

	internal class TapButton : StyleButton {

		internal KeyCommand KeyCommand { set; get; }

		public TapButton(TapButtonSpecs specs) : base(specs.Style) {

			SetResourceReference(StyleProperty, typeof(StyleButton));
			KeyCommand = specs.KeyCommand;
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
