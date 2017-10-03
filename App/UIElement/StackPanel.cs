using System.Windows.Controls;
using static Taction.Config;

namespace Taction.UIElement {

	internal class StackPanel : System.Windows.Controls.StackPanel {

		public StackPanel(IPanelItemSpecs specs, System.Windows.Controls.StackPanel panel = null) {

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
