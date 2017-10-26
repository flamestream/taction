﻿using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Taction.UIElement {

	internal class RadialMenuButton : CustomButton {

		private RadialMenuWindow radialMenuWindow;

		public RadialMenuButton(IPanelItemSpecs specs) {

			var s = (RadialMenuButtonSpecs)specs;
			radialMenuWindow = new RadialMenuWindow(s.RadialMenuSpecs);
		}

		protected override void OnTouchDown(TouchEventArgs e) {

			base.OnTouchDown(e);
			radialMenuWindow.SetVisibility(true);
		}
	}
}