using System;
using System.Windows;

namespace Taction.UIElement {

	internal partial class RadialMenuButton : StyleToggleButton {

		public RadialMenuWindow RadialMenuWindow;

		public RadialMenuButton(RadialMenuButtonSpecs specs) : base(specs.Style) {

			// Set up radial menu window
			RadialMenuWindow = new RadialMenuWindow(specs.RadialMenuSpecs);
			RadialMenuWindow.IsVisibleChanged += RadialMenuWindow_IsVisibleChanged;

			// Event binding
			Checked += RadialMenuButton_Checked;
			Unchecked += RadialMenuButton_Unchecked;

			SetResourceReference(StyleProperty, typeof(StyleToggleButton));
		}

		private void RadialMenuWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {

			// Match check status to child window visibility
			IsChecked = (bool)e.NewValue;
		}

		private void RadialMenuButton_Checked(Object sender, RoutedEventArgs e) {

			// Already there check
			if (RadialMenuWindow.Visibility == Visibility.Visible)
				return;

			// Close all radial menu windows
			var mainPanel = App.Instance.MainPanel;
			mainPanel.CloseAllRadialMenuWindows();

			// Open own menu
			RadialMenuWindow.SetVisibility(true);

			// Ensure that the panel is always above the radial menu
			mainPanel.Owner = RadialMenuWindow;
		}

		private void RadialMenuButton_Unchecked(Object sender, RoutedEventArgs e) {

			// Not there check
			if (RadialMenuWindow.Visibility != Visibility.Visible)
				return;

			RadialMenuWindow.SetVisibility(false, false);
		}
	}
}
