using RadialMenu.Controls;
using System;
using System.Windows;
using System.Windows.Media;

namespace Taction.UIElement {

	public partial class CustomRadialMenuItem : RadialMenuItem {

		internal KeyCommand KeyCommand { set; get; }

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
				radialMenuwindow.SetVisibility(false, false);
			});

			// Apply default style
			ApplyDefaultStyle();

			// Special label content handling
			if (Base_LabelContent == null) {

				Base_LabelContent = KeyCommand.ToString();
			}

			var appCustomStyle = App.Instance.Config.Layout;

			// Apply base style
			{
				// Apply app default style
				if (appCustomStyle != null) {

					ApplyBaseStyle(appCustomStyle.DefaultRadialMenuItemStyle);
				}

				// Apply button default style
				ApplyBaseStyle(defaultStyle);

				// Apply own style
				if (specs != null) {

					ApplyBaseStyle(specs.Style);
				}
			}

			SpreadBaseStyle();

			// Apply active style
			{
				// Apply app default style
				if (appCustomStyle != null) {

					ApplyActiveStyle(appCustomStyle.DefaultRadialMenuItemStyle);
				}

				// Apply button default style
				ApplyActiveStyle(defaultStyle);

				// Apply own style
				if (specs != null) {

					ApplyActiveStyle(specs.Style);
				}
			}
		}

		public void ApplyBaseStyle(RadialMenuItemStyleSetSpecs styleSet) {

			if (styleSet == null)
				return;

			ApplyBaseStyle(styleSet.Base);
		}

		public void ApplyBaseStyle(RadialMenuItemStyleSpecs style) {

			if (style == null)
				return;

			var innerEdgeStyle = style.InnerEdgeSpecs;
			if (innerEdgeStyle != null) {

				var borderSpecs = innerEdgeStyle.Border;
				if (borderSpecs != null) {

					if (borderSpecs.Color != null)
						Base_BorderBrush = borderSpecs.Color;

					if (borderSpecs.Thickness != null)
						Base_BorderThickness = borderSpecs.Thickness.Value;

					// No border radius
				}

				if (innerEdgeStyle.Color != null)
					Base_Background = innerEdgeStyle.Color;

				if (innerEdgeStyle.StartDistance != null)
					Base_InnerRadius = innerEdgeStyle.StartDistance.Value;

				if (innerEdgeStyle.Size != null)
					Base_OuterRadius = InnerRadius + innerEdgeStyle.Size.Value;
			}

			var outerEdgeStyle = style.OuterEdgeSpecs;
			if (outerEdgeStyle != null) {

				var borderSpecs = outerEdgeStyle.Border;
				if (borderSpecs != null) {

					if (borderSpecs.Color != null)
						Base_EdgeBorderBrush = borderSpecs.Color;

					if (borderSpecs.Thickness != null)
						Base_EdgeBorderThickness = borderSpecs.Thickness.Value.Top;

					// No border radius
				}

				if (outerEdgeStyle.Color != null)
					Base_EdgeBackground = outerEdgeStyle.Color;

				// Calculate default size
				var size = Math.Max(EdgeOuterRadius - EdgeInnerRadius, 0);

				if (outerEdgeStyle.StartDistance == null)
					Base_EdgeInnerRadius = Math.Max(EdgeInnerRadius, OuterRadius);
				else
					Base_EdgeInnerRadius = OuterRadius + outerEdgeStyle.StartDistance.Value;

				if (outerEdgeStyle.Size != null)
					size = outerEdgeStyle.Size.Value;

				Base_EdgeOuterRadius = EdgeInnerRadius + size;
			}

			var labelStyle = style.LabelSpecs;
			if (labelStyle != null) {

				var borderStyle = labelStyle.Border;
				if (borderStyle != null) {

					if (borderStyle.Color != null)
						Base_LabelBorderBrush = borderStyle.Color;

					if (borderStyle.Radius != null)
						Base_LabelCornerRadius = borderStyle.Radius.Value;

					if (borderStyle.Thickness != null)
						Base_LabelBorderThickness = borderStyle.Thickness.Value;
				}

				if (labelStyle.Color != null)
					Base_LabelBackground = labelStyle.Color;

				if (labelStyle.Content != null)
					Base_LabelContent = labelStyle.Content;

				if (labelStyle.Padding != null)
					Base_LabelPadding = labelStyle.Padding.Value;

				if (labelStyle.Margin != null)
					Base_LabelMargin = labelStyle.Margin.Value;

				if (labelStyle.Size != null)
					IconSize = labelStyle.Size.Value;

				if (labelStyle.StartDistance != null)
					ContentRadius = labelStyle.StartDistance.Value;

				var textStyle = labelStyle.TextStyle;
				if (textStyle != null) {

					if (textStyle.Color != null)
						Base_LabelForeground = textStyle.Color;

					if (textStyle.FontFamily != null)
						Base_LabelFontFamily = textStyle.FontFamily;

					if (textStyle.FontSize != null)
						Base_LabelFontSize = textStyle.FontSize.Value;

					if (textStyle.FontWeight != null)
						Base_LabelFontWeight = textStyle.FontWeight.Value;
				}
			}
		}

		public void ApplyActiveStyle(RadialMenuItemStyleSetSpecs styleSet) {

			if (styleSet == null)
				return;

			ApplyActiveStyle(styleSet.Base);
			ApplyActiveStyle(styleSet.Active);
		}

		public void ApplyActiveStyle(RadialMenuItemStyleSpecs style) {

			if (style == null)
				return;

			var innerEdgeStyle = style.InnerEdgeSpecs;
			if (innerEdgeStyle != null) {

				var borderSpecs = innerEdgeStyle.Border;
				if (borderSpecs != null) {

					if (borderSpecs.Color != null)
						Active_BorderBrush = borderSpecs.Color;

					if (borderSpecs.Thickness != null)
						Active_BorderThickness = borderSpecs.Thickness.Value;

					// No border radius
				}

				if (innerEdgeStyle.Color != null)
					Active_Background = innerEdgeStyle.Color;

				if (innerEdgeStyle.StartDistance != null)
					Active_InnerRadius = innerEdgeStyle.StartDistance.Value;

				if (innerEdgeStyle.Size != null)
					Active_OuterRadius = InnerRadius + innerEdgeStyle.Size.Value;
			}

			var outerEdgeStyle = style.OuterEdgeSpecs;
			if (outerEdgeStyle != null) {

				var borderSpecs = outerEdgeStyle.Border;
				if (borderSpecs != null) {

					if (borderSpecs.Color != null)
						Active_EdgeBorderBrush = borderSpecs.Color;

					if (borderSpecs.Thickness != null)
						Active_EdgeBorderThickness = borderSpecs.Thickness.Value.Top;

					// No border radius
				}

				if (outerEdgeStyle.Color != null)
					Active_EdgeBackground = outerEdgeStyle.Color;

				// Calculate default size
				var size = Math.Max(EdgeOuterRadius - EdgeInnerRadius, 0);

				if (outerEdgeStyle.StartDistance == null)
					Active_EdgeInnerRadius = Math.Max(EdgeInnerRadius, OuterRadius);
				else
					Active_EdgeInnerRadius = OuterRadius + outerEdgeStyle.StartDistance.Value;

				if (outerEdgeStyle.Size != null)
					size = outerEdgeStyle.Size.Value;

				Active_EdgeOuterRadius = EdgeInnerRadius + size;
			}

			var labelStyle = style.LabelSpecs;
			if (labelStyle != null) {

				var borderStyle = labelStyle.Border;
				if (borderStyle != null) {

					if (borderStyle.Color != null)
						Active_LabelBorderBrush = borderStyle.Color;

					if (borderStyle.Radius != null)
						Active_LabelCornerRadius = borderStyle.Radius.Value;

					if (borderStyle.Thickness != null)
						Active_LabelBorderThickness = borderStyle.Thickness.Value;
				}

				if (labelStyle.Color != null)
					Active_LabelBackground = labelStyle.Color;

				if (labelStyle.Content != null)
					Active_LabelContent = labelStyle.Content;

				if (labelStyle.Padding != null)
					Active_LabelPadding = labelStyle.Padding.Value;

				if (labelStyle.Margin != null)
					Active_LabelMargin = labelStyle.Margin.Value;

				if (labelStyle.Size != null)
					Active_IconSize = labelStyle.Size.Value;

				if (labelStyle.StartDistance != null)
					Active_ContentRadius = labelStyle.StartDistance.Value;

				var textStyle = labelStyle.TextStyle;
				if (textStyle != null) {

					if (textStyle.Color != null)
						Active_LabelForeground = textStyle.Color;

					if (textStyle.FontFamily != null)
						Active_LabelFontFamily = textStyle.FontFamily;

					if (textStyle.FontSize != null)
						Active_LabelFontSize = textStyle.FontSize.Value;

					if (textStyle.FontWeight != null)
						Active_LabelFontWeight = textStyle.FontWeight.Value;
				}
			}
		}

		public void ApplyDefaultStyle() {

			// Base style
			var Default_Background = TryFindResource("Default_Background") as Brush;
			if (Default_Background != null)
				Base_Background = Default_Background;

			var Default_BorderBrush = TryFindResource("Default_BorderBrush") as Brush;
			if (Default_BorderBrush != null)
				Base_BorderBrush = Default_BorderBrush;

			var Default_BorderThickness = TryFindResource("Default_BorderThickness") as Thickness?;
			if (Default_BorderThickness != null)
				Base_BorderThickness = Default_BorderThickness.Value;

			var Default_ContentRadius = TryFindResource("Default_ContentRadius") as double?;
			if (Default_ContentRadius != null)
				Base_ContentRadius = Default_ContentRadius.Value;

			var Default_EdgeBackground = TryFindResource("Default_EdgeBackground") as Brush;
			if (Default_EdgeBackground != null)
				Base_EdgeBackground = Default_EdgeBackground;

			var Default_EdgeBorderBrush = TryFindResource("Default_EdgeBorderBrush") as Brush;
			if (Default_EdgeBorderBrush != null)
				Base_EdgeBorderBrush = Default_EdgeBorderBrush;

			var Default_EdgeBorderThickness = TryFindResource("Default_EdgeBorderThickness") as double?;
			if (Default_EdgeBorderThickness != null)
				Base_EdgeBorderThickness = Default_EdgeBorderThickness.Value;

			var Default_EdgeInnerRadius = TryFindResource("Default_EdgeInnerRadius") as double?;
			if (Default_EdgeInnerRadius != null)
				Base_EdgeInnerRadius = Default_EdgeInnerRadius.Value;

			var Default_EdgeOuterRadius = TryFindResource("Default_EdgeOuterRadius") as double?;
			if (Default_EdgeOuterRadius != null)
				Base_EdgeOuterRadius = Default_EdgeOuterRadius.Value;

			var Default_InnerRadius = TryFindResource("Default_InnerRadius") as double?;
			if (Default_InnerRadius != null)
				Base_InnerRadius = Default_InnerRadius.Value;

			var Default_OuterRadius = TryFindResource("Default_OuterRadius") as double?;
			if (Default_OuterRadius != null)
				Base_OuterRadius = Default_OuterRadius.Value;

			var Default_LabelBackground = TryFindResource("Default_LabelBackground") as Brush;
			if (Default_LabelBackground != null)
				Base_LabelBackground = Default_LabelBackground;

			var Default_LabelBorderBrush = TryFindResource("Default_LabelBorderBrush") as Brush;
			if (Default_LabelBorderBrush != null)
				Base_LabelBackground = Default_LabelBorderBrush;

			var Default_LabelBorderThickness = TryFindResource("Default_LabelBorderThickness") as Thickness?;
			if (Default_LabelBorderThickness != null)
				Base_LabelBorderThickness = Default_LabelBorderThickness.Value;

			var Default_LabelCornerRadius = TryFindResource("Default_LabelCornerRadius") as CornerRadius?;
			if (Default_LabelCornerRadius != null)
				Base_LabelCornerRadius = Default_LabelCornerRadius.Value;

			var Default_LabelPadding = TryFindResource("Default_LabelPadding") as Thickness?;
			if (Default_LabelPadding != null)
				Base_LabelPadding = Default_LabelPadding.Value;

			var Default_LabelMargin = TryFindResource("Default_LabelMargin") as Thickness?;
			if (Default_LabelMargin != null)
				Base_LabelMargin = Default_LabelMargin.Value;

			var Default_LabelHeight = TryFindResource("Default_LabelHeight") as double?;
			if (Default_LabelHeight != null)
				Base_LabelHeight = Default_LabelHeight.Value;

			var Default_LabelWidth = TryFindResource("Default_LabelWidth") as double?;
			if (Default_LabelWidth != null)
				Base_LabelWidth = Default_LabelWidth.Value;

			var Default_LabelFontFamily = TryFindResource("Default_LabelFontFamily") as FontFamily;
			if (Default_LabelFontFamily != null)
				Base_LabelFontFamily = Default_LabelFontFamily;

			var Default_LabelFontSize = TryFindResource("Default_LabelFontSize") as double?;
			if (Default_LabelFontSize != null)
				Base_LabelFontSize = Default_LabelFontSize.Value;

			var Default_LabelFontWeight = TryFindResource("Default_LabelFontWeight") as FontWeight?;
			if (Default_LabelFontWeight != null)
				Base_LabelFontWeight = Default_LabelFontWeight.Value;

			var Default_LabelForeground = TryFindResource("Default_LabelForeground") as Brush;
			if (Default_LabelForeground != null)
				Base_LabelForeground = Default_LabelForeground;

			// Active style
			var Default_Active_Background = TryFindResource("Default_Active_Background") as Brush;
			if (Default_Active_Background != null)
				Active_Background = Default_Active_Background;

			var Default_Active_BorderBrush = TryFindResource("Default_Active_BorderBrush") as Brush;
			if (Default_Active_BorderBrush != null)
				Active_BorderBrush = Default_Active_BorderBrush;

			var Default_Active_BorderThickness = TryFindResource("Default_Active_BorderThickness") as Thickness?;
			if (Default_Active_BorderThickness != null)
				Active_BorderThickness = Default_Active_BorderThickness.Value;

			var Default_Active_ContentRadius = TryFindResource("Default_Active_ContentRadius") as double?;
			if (Default_Active_ContentRadius != null)
				Active_ContentRadius = Default_Active_ContentRadius.Value;

			var Default_Active_EdgeBackground = TryFindResource("Default_Active_EdgeBackground") as Brush;
			if (Default_Active_EdgeBackground != null)
				Active_EdgeBackground = Default_Active_EdgeBackground;

			var Default_Active_EdgeBorderBrush = TryFindResource("Default_Active_EdgeBorderBrush") as Brush;
			if (Default_Active_EdgeBorderBrush != null)
				Active_EdgeBorderBrush = Default_Active_EdgeBorderBrush;

			var Default_Active_EdgeBorderThickness = TryFindResource("Default_Active_EdgeBorderThickness") as double?;
			if (Default_Active_EdgeBorderThickness != null)
				Active_EdgeBorderThickness = Default_Active_EdgeBorderThickness.Value;

			var Default_Active_EdgeInnerRadius = TryFindResource("Default_Active_EdgeInnerRadius") as double?;
			if (Default_Active_EdgeInnerRadius != null)
				Active_EdgeInnerRadius = Default_Active_EdgeInnerRadius.Value;

			var Default_Active_EdgeOuterRadius = TryFindResource("Default_Active_EdgeOuterRadius") as double?;
			if (Default_Active_EdgeOuterRadius != null)
				Active_EdgeOuterRadius = Default_Active_EdgeOuterRadius.Value;

			var Default_Active_InnerRadius = TryFindResource("Default_Active_InnerRadius") as double?;
			if (Default_Active_InnerRadius != null)
				Active_InnerRadius = Default_Active_InnerRadius.Value;

			var Default_Active_OuterRadius = TryFindResource("Default_Active_OuterRadius") as double?;
			if (Default_Active_OuterRadius != null)
				Active_OuterRadius = Default_Active_OuterRadius.Value;

			var Default_Active_LabelBackground = TryFindResource("Default_Active_LabelBackground") as Brush;
			if (Default_Active_LabelBackground != null)
				Active_LabelBackground = Default_Active_LabelBackground;

			var Default_Active_LabelBorderBrush = TryFindResource("Default_Active_LabelBorderBrush") as Brush;
			if (Default_Active_LabelBorderBrush != null)
				Active_LabelBackground = Default_Active_LabelBorderBrush;

			var Default_Active_LabelBorderThickness = TryFindResource("Default_Active_LabelBorderThickness") as Thickness?;
			if (Default_Active_LabelBorderThickness != null)
				Active_LabelBorderThickness = Default_Active_LabelBorderThickness.Value;

			var Default_Active_LabelCornerRadius = TryFindResource("Default_Active_LabelCornerRadius") as CornerRadius?;
			if (Default_Active_LabelCornerRadius != null)
				Active_LabelCornerRadius = Default_Active_LabelCornerRadius.Value;

			var Default_Active_LabelPadding = TryFindResource("Default_Active_LabelPadding") as Thickness?;
			if (Default_Active_LabelPadding != null)
				Active_LabelPadding = Default_Active_LabelPadding.Value;

			var Default_Active_LabelMargin = TryFindResource("Default_Active_LabelMargin") as Thickness?;
			if (Default_Active_LabelMargin != null)
				Active_LabelMargin = Default_Active_LabelMargin.Value;

			var Default_Active_LabelHeight = TryFindResource("Default_Active_LabelHeight") as double?;
			if (Default_Active_LabelHeight != null)
				Active_LabelHeight = Default_Active_LabelHeight.Value;

			var Default_Active_LabelWidth = TryFindResource("Default_Active_LabelWidth") as double?;
			if (Default_Active_LabelWidth != null)
				Active_LabelWidth = Default_Active_LabelWidth.Value;

			var Default_Active_LabelFontFamily = TryFindResource("Default_Active_LabelFontFamily") as FontFamily;
			if (Default_Active_LabelFontFamily != null)
				Active_LabelFontFamily = Default_Active_LabelFontFamily;

			var Default_Active_LabelFontSize = TryFindResource("Default_Active_LabelFontSize") as double?;
			if (Default_Active_LabelFontSize != null)
				Active_LabelFontSize = Default_Active_LabelFontSize.Value;

			var Default_Active_LabelFontWeight = TryFindResource("Default_Active_LabelFontWeight") as FontWeight?;
			if (Default_Active_LabelFontWeight != null)
				Active_LabelFontWeight = Default_Active_LabelFontWeight.Value;

			var Default_Active_LabelForeground = TryFindResource("Default_Active_LabelForeground") as Brush;
			if (Default_Active_LabelForeground != null)
				Active_LabelForeground = Default_Active_LabelForeground;
		}

		public void SpreadBaseStyle() {

			if (Active_Background == null)
				Active_Background = Base_Background;
			if (Active_BorderBrush == null)
				Active_BorderBrush = Base_BorderBrush;
			if (Active_BorderThickness == null)
				Active_BorderThickness = Base_BorderThickness;
			if (Active_ContentRadius == null)
				Active_ContentRadius = Base_ContentRadius;
			if (Active_EdgeBackground == null)
				Active_EdgeBackground = Base_EdgeBackground;
			if (Active_EdgeBorderBrush == null)
				Active_EdgeBorderBrush = Base_EdgeBorderBrush;
			if (Active_EdgeBorderThickness == null)
				Active_EdgeBorderThickness = Base_EdgeBorderThickness;
			if (Active_EdgeInnerRadius == null)
				Active_EdgeInnerRadius = Base_EdgeInnerRadius;
			if (Active_EdgeOuterRadius == null)
				Active_EdgeOuterRadius = Base_EdgeOuterRadius;
			if (Active_InnerRadius == null)
				Active_InnerRadius = Base_InnerRadius;
			if (Active_OuterRadius == null)
				Active_OuterRadius = Base_OuterRadius;

			if (Active_LabelBackground == null)
				Active_LabelBackground = Base_LabelBackground;
			if (Active_LabelBorderBrush == null)
				Active_LabelBorderBrush = Base_LabelBorderBrush;
			if (Active_LabelBorderThickness == null)
				Active_LabelBorderThickness = Base_LabelBorderThickness;
			if (Active_LabelCornerRadius == null)
				Active_LabelCornerRadius = Base_LabelCornerRadius;
			if (Active_LabelPadding == null)
				Active_LabelPadding = Base_LabelPadding;
			if (Active_LabelMargin == null)
				Active_LabelMargin = Base_LabelMargin;
			if (Active_LabelHeight == null)
				Active_LabelHeight = Base_LabelHeight;
			if (Active_LabelWidth == null)
				Active_LabelWidth = Base_LabelWidth;
			if (Active_LabelFontFamily == null)
				Active_LabelFontFamily = Base_LabelFontFamily;
			if (Active_LabelFontSize == null)
				Active_LabelFontSize = Base_LabelFontSize;
			if (Active_LabelFontWeight == null)
				Active_LabelFontWeight = Base_LabelFontWeight;
			if (Active_LabelForeground == null)
				Active_LabelForeground = Base_LabelForeground;

			if (Active_LabelContent == null)
				Active_LabelContent = Base_LabelContent;
		}

		#region IconSize

		public double? IconSize {
			get {
				return Base_LabelWidth != null ?
					(double?)Base_LabelWidth.Value * 0.5 :
					null;
			}
			set {
				Base_LabelWidth = value * 2;
				Base_LabelHeight = value * 2;
			}
		}

		#endregion IconSize

		#region Active_IconSize

		public double? Active_IconSize {
			get {
				return Active_LabelWidth != null ?
					(double?)Active_LabelWidth.Value * 0.5 :
					null;
			}
			set {
				Active_LabelHeight = value * 2;
				Active_LabelWidth = value * 2;
			}
		}

		#endregion Active_IconSize
	}
}
