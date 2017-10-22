using System.Windows.Controls;

namespace Taction.UIElement {

	internal class CustomStackPanel : StackPanel {

		public CustomStackPanel(IPanelItemSpecs specs) {

			var s = (PivotSpecs)specs;
		}
	}
}
