using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Taction.UIElement {

	public partial class StyleButton : Button {

		public ButtonStyleSetSpecs StyleSetSpecs { get; private set; }

		public double? ComputedHeight {
			get {

				var parent = Parent as StackPanel;
				if (parent == null || parent.Orientation == Orientation.Vertical)
					return StyleSetSpecs.Size;

				return null;
			}
		}

		public double? ComputedWidth {

			get {

				var parent = Parent as StackPanel;
				if (parent == null || parent.Orientation == Orientation.Horizontal)
					return StyleSetSpecs.Size;

				return null;
			}
		}

		public StyleButton(ButtonStyleSetSpecs specs) {

			InitializeComponent();
			DataContext = this;

			StyleSetSpecs = specs;
			StyleSetSpecs.Size = 100;
			StyleSetSpecs.Base.Color = new SolidColorBrush(Colors.Red);
			StyleSetSpecs.Active.Color = new SolidColorBrush(Colors.Yellow);
			StyleSetSpecs.Base.Border.Thickness = new Thickness(4);
			StyleSetSpecs.Base.Border.Color = new SolidColorBrush(Colors.Yellow);
			StyleSetSpecs.Active.Border.Color = new SolidColorBrush(Colors.Green);
			StyleSetSpecs.Base.TextStyle.Color = new SolidColorBrush(Colors.Wheat);
			StyleSetSpecs.Active.TextStyle.Color = new SolidColorBrush(Colors.White);
			StyleSetSpecs.Base.Content = StyleSetSpecs.Active.Content = "rawr";
			StyleSetSpecs.Base.Opacity = 0.7;

			//Height2 = 200;
		}
	}
}
