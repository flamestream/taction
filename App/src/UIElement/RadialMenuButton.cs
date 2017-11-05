using System.Windows.Input;

namespace Taction.UIElement {

	internal class RadialMenuButton : CustomButton {

		public RadialMenuWindow RadialMenuWindow;

		public RadialMenuButton(IPanelItemSpecs specs) {

			var s = (RadialMenuButtonSpecs)specs;
			RadialMenuWindow = new RadialMenuWindow(s.RadialMenuSpecs);
		}

		protected override void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);

			// Visibility check
			if (RadialMenuWindow.IsVisible) {

				RadialMenuWindow.SetVisibility(false, false);
				return;
			}

			// Close all radial menu windows
			var mainPanel = App.Instance.MainPanel;
			mainPanel.CloseAllRadialMenuWindows();

			// Open own menu
			RadialMenuWindow.SetVisibility(true);

			// This ensures that the panel is always above the radial menu
			mainPanel.Owner = RadialMenuWindow;
		}
	}
}
