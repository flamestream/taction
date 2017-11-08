using System.Windows;
using System.Windows.Controls;

namespace Taction.UIElement {

	public partial class StyleButton : Button {

		public ButtonStyleSetSpecs StyleSetSpecs { get; protected set; }
		public Rect Boundaries { get; protected set; }

		public double? ComputedHeight {
			get {

				var parent = Parent as StackPanel;
				if (parent == null || parent.Orientation == Orientation.Vertical)
					return StyleSetSpecs?.Size;

				return null;
			}
		}

		public double? ComputedWidth {

			get {

				var parent = Parent as StackPanel;
				if (parent == null || parent.Orientation == Orientation.Horizontal)
					return StyleSetSpecs?.Size;

				return null;
			}
		}

		public StyleButton() {

			InitializeComponent();
			DataContext = this;

		}

		public StyleButton(ButtonStyleSetSpecs specs) : this() {

			StyleSetSpecs = specs;
		}

		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {

			base.OnRenderSizeChanged(sizeInfo);

			UpdateBoundaries();

			Designer.AdjustGradientColor(this, Background);
			Designer.AdjustGradientColor(this, BorderBrush);
		}

		public void UpdateBoundaries() {

			Boundaries = GetBoundaries();
		}

		public Rect GetBoundaries() {

			var origin = TranslatePoint(new Point(), null);
			var bounds = new Rect(origin.X, origin.Y, ActualWidth, ActualHeight);

			return bounds;
		}
	}
}
