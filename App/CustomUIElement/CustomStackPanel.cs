using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Taction.Config;

namespace Taction.CustomUIElement {

	internal class CustomStackPanel : StackPanel {

		public CustomStackPanel(IPanelItemSpecs specs, StackPanel panel = null) {

			var s = (PivotSpecs)specs;

			this.Orientation = panel.Orientation == Orientation.Horizontal ?
				Orientation.Vertical :
				Orientation.Horizontal;

			if (this.Orientation != panel.Orientation) {
				if (panel.Orientation == Orientation.Vertical)
					this.Height = s.size;
				else
					this.Width = s.size;
			}
		}
	}
}
