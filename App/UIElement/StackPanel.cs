using System.Windows.Controls;
using static Taction.Config;

namespace Taction.UIElement {

	internal class StackPanel : System.Windows.Controls.StackPanel {

		public StackPanel(IPanelItemSpecs specs) {

			var s = (PivotSpecs)specs;
		}
	}
}
