using RadialMenu.Controls;
using System.Windows;

namespace Taction.UIElement {

	public partial class CustomRadialMenuItem : RadialMenuItem {

		internal KeyCommand KeyCommand { set; get; }
		public RadialMenuItemStyleSetSpecs StyleSetSpecs { get; protected set; }

		public CustomRadialMenuItem() {

			InitializeComponent();
			LayoutRoot.DataContext = this;
		}

		public CustomRadialMenuItem(RadialMenuItemSpecs specs, RadialMenuItemStyleSetSpecs defaultStyle) : this() {

			KeyCommand = specs.KeyCommand;
			Command = new RelayCommand(() => {

				// Press key
				App.Instance.InputSimulator.SimulateKeyPress(KeyCommand);

				// Collapse menu/window
				var window = Window.GetWindow(this);
				if (!(window is RadialMenuWindow))
					return;

				var radialMenuwindow = (RadialMenuWindow)window;
				radialMenuwindow.SetVisibility(false);
			});

			StyleSetSpecs = ApplyStyle(App.Instance.Config.Layout.DefaultRadialMenuItemStyle, defaultStyle, specs.Style);
		}

		public double? ComputedLabelBaseSize {
			get {
				return StyleSetSpecs?.Base?.LabelSpecs?.Size * 2;
			}
		}

		public double? ComputedBaseInnerRadius {
			get {
				return StyleSetSpecs?.Base?.InnerEdgeSpecs?.StartDistance;
			}
		}

		public double? ComputedBaseOuterRadius {
			get {
				return ComputedBaseInnerRadius + StyleSetSpecs?.Base?.InnerEdgeSpecs?.Size;
			}
		}

		public double? ComputedBaseBorderThickness {
			get {
				return StyleSetSpecs?.Base?.InnerEdgeSpecs?.Border.Thickness?.Top;
			}
		}

		public double? ComputedBaseEdgeInnerRadius {
			get {
				return ComputedBaseOuterRadius + StyleSetSpecs?.Base?.OuterEdgeSpecs?.StartDistance;
			}
		}

		public double? ComputedBaseEdgeOuterRadius {
			get {
				return ComputedBaseEdgeInnerRadius + StyleSetSpecs?.Base?.OuterEdgeSpecs?.Size;
			}
		}

		public double? ComputedBaseEdgeBorderThickness {
			get {
				return StyleSetSpecs?.Base?.OuterEdgeSpecs?.Border.Thickness?.Top;
			}
		}

		public double? ComputedLabelActiveSize {
			get {
				return StyleSetSpecs?.Active?.LabelSpecs?.Size * 2;
			}
		}

		public double? ComputedActiveInnerRadius {
			get {
				return StyleSetSpecs?.Active?.InnerEdgeSpecs?.StartDistance;
			}
		}

		public double? ComputedActiveOuterRadius {
			get {
				return ComputedActiveInnerRadius + StyleSetSpecs?.Active?.InnerEdgeSpecs?.Size;
			}
		}

		public double? ComputedActiveBorderThickness {
			get {
				return StyleSetSpecs?.Active?.InnerEdgeSpecs?.Border.Thickness?.Top;
			}
		}

		public double? ComputedActiveEdgeInnerRadius {
			get {
				return ComputedActiveOuterRadius + StyleSetSpecs?.Active?.OuterEdgeSpecs?.StartDistance;
			}
		}

		public double? ComputedActiveEdgeOuterRadius {
			get {
				return ComputedActiveEdgeInnerRadius + StyleSetSpecs?.Active?.OuterEdgeSpecs?.Size;
			}
		}

		public double? ComputedActiveEdgeBorderThickness {
			get {
				return StyleSetSpecs?.Active?.OuterEdgeSpecs?.Border.Thickness?.Top;
			}
		}

		private static RadialMenuItemStyleSetSpecs ApplyStyle(params RadialMenuItemStyleSetSpecs[] args) {

			var output = new RadialMenuItemStyleSetSpecs();

			foreach (var s in args) {

				if (s == null)
					continue;

				ApplyStyle(output.Base, s.Base);
				ApplyStyle(output.Active, s.Active);
			}

			ApplyStyle(output.Active, output.Base, true);

			return output;
		}

		private static void ApplyStyle(RadialMenuItemStyleSpecs source, RadialMenuItemStyleSpecs apply, bool ignoreIfNotNull = false) {

			if (source.InnerEdgeSpecs.Border.Color == null || !ignoreIfNotNull)
				source.InnerEdgeSpecs.Border.Color = apply.InnerEdgeSpecs.Border.Color ?? source.InnerEdgeSpecs.Border.Color;

			if (source.InnerEdgeSpecs.Border.Radius == null || !ignoreIfNotNull)
				source.InnerEdgeSpecs.Border.Radius = apply.InnerEdgeSpecs.Border.Radius ?? source.InnerEdgeSpecs.Border.Radius;

			if (source.InnerEdgeSpecs.Border.Thickness == null || !ignoreIfNotNull)
				source.InnerEdgeSpecs.Border.Thickness = apply.InnerEdgeSpecs.Border.Thickness ?? source.InnerEdgeSpecs.Border.Thickness;

			if (source.InnerEdgeSpecs.Color == null || !ignoreIfNotNull)
				source.InnerEdgeSpecs.Color = apply.InnerEdgeSpecs.Color ?? source.InnerEdgeSpecs.Color;

			if (source.InnerEdgeSpecs.Size == null || !ignoreIfNotNull)
				source.InnerEdgeSpecs.Size = apply.InnerEdgeSpecs.Size ?? source.InnerEdgeSpecs.Size;

			if (source.InnerEdgeSpecs.StartDistance == null || !ignoreIfNotNull)
				source.InnerEdgeSpecs.StartDistance = apply.InnerEdgeSpecs.StartDistance ?? source.InnerEdgeSpecs.StartDistance;

			if (source.LabelSpecs.Border.Color == null || !ignoreIfNotNull)
				source.LabelSpecs.Border.Color = apply.LabelSpecs.Border.Color ?? source.LabelSpecs.Border.Color;

			if (source.LabelSpecs.Border.Radius == null || !ignoreIfNotNull)
				source.LabelSpecs.Border.Radius = apply.LabelSpecs.Border.Radius ?? source.LabelSpecs.Border.Radius;

			if (source.LabelSpecs.Border.Thickness == null || !ignoreIfNotNull)
				source.LabelSpecs.Border.Thickness = apply.LabelSpecs.Border.Thickness ?? source.LabelSpecs.Border.Thickness;

			if (source.LabelSpecs.Color == null || !ignoreIfNotNull)
				source.LabelSpecs.Color = apply.LabelSpecs.Color ?? source.LabelSpecs.Color;

			if (source.LabelSpecs.Content == null || !ignoreIfNotNull)
				source.LabelSpecs.Content = apply.LabelSpecs.Content ?? source.LabelSpecs.Content;

			if (source.LabelSpecs.Margin == null || !ignoreIfNotNull)
				source.LabelSpecs.Margin = apply.LabelSpecs.Margin ?? source.LabelSpecs.Margin;

			if (source.LabelSpecs.Opacity == null || !ignoreIfNotNull)
				source.LabelSpecs.Opacity = apply.LabelSpecs.Opacity ?? source.LabelSpecs.Opacity;

			if (source.LabelSpecs.Padding == null || !ignoreIfNotNull)
				source.LabelSpecs.Padding = apply.LabelSpecs.Padding ?? source.LabelSpecs.Padding;

			if (source.LabelSpecs.Size == null || !ignoreIfNotNull)
				source.LabelSpecs.Size = apply.LabelSpecs.Size ?? source.LabelSpecs.Size;

			if (source.LabelSpecs.StartDistance == null || !ignoreIfNotNull)
				source.LabelSpecs.StartDistance = apply.LabelSpecs.StartDistance ?? source.LabelSpecs.StartDistance;

			if (source.LabelSpecs.TextStyle.Color == null || !ignoreIfNotNull)
				source.LabelSpecs.TextStyle.Color = apply.LabelSpecs.TextStyle.Color ?? source.LabelSpecs.TextStyle.Color;

			if (source.LabelSpecs.TextStyle.FontFamily == null || !ignoreIfNotNull)
				source.LabelSpecs.TextStyle.FontFamily = apply.LabelSpecs.TextStyle.FontFamily ?? source.LabelSpecs.TextStyle.FontFamily;

			if (source.LabelSpecs.TextStyle.FontSize == null || !ignoreIfNotNull)
				source.LabelSpecs.TextStyle.FontSize = apply.LabelSpecs.TextStyle.FontSize ?? source.LabelSpecs.TextStyle.FontSize;

			if (source.LabelSpecs.TextStyle.FontWeight == null || !ignoreIfNotNull)
				source.LabelSpecs.TextStyle.FontWeight = apply.LabelSpecs.TextStyle.FontWeight ?? source.LabelSpecs.TextStyle.FontWeight;

			if (source.OuterEdgeSpecs.Border.Color == null || !ignoreIfNotNull)
				source.OuterEdgeSpecs.Border.Color = apply.OuterEdgeSpecs.Border.Color ?? source.OuterEdgeSpecs.Border.Color;

			if (source.OuterEdgeSpecs.Border.Radius == null || !ignoreIfNotNull)
				source.OuterEdgeSpecs.Border.Radius = apply.OuterEdgeSpecs.Border.Radius ?? source.OuterEdgeSpecs.Border.Radius;

			if (source.OuterEdgeSpecs.Border.Thickness == null || !ignoreIfNotNull)
				source.OuterEdgeSpecs.Border.Thickness = apply.OuterEdgeSpecs.Border.Thickness ?? source.OuterEdgeSpecs.Border.Thickness;

			if (source.OuterEdgeSpecs.Color == null || !ignoreIfNotNull)
				source.OuterEdgeSpecs.Color = apply.OuterEdgeSpecs.Color ?? source.OuterEdgeSpecs.Color;

			if (source.OuterEdgeSpecs.Size == null || !ignoreIfNotNull)
				source.OuterEdgeSpecs.Size = apply.OuterEdgeSpecs.Size ?? source.OuterEdgeSpecs.Size;

			if (source.OuterEdgeSpecs.StartDistance == null || !ignoreIfNotNull)
				source.OuterEdgeSpecs.StartDistance = apply.OuterEdgeSpecs.StartDistance ?? source.OuterEdgeSpecs.StartDistance;
		}
	}
}
