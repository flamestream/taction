using RadialMenu.Controls;
using System;
using System.Windows;

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

			var appDefaultStyle = App.Instance.Config.Layout;

			// Apply base style
			{
				// Apply app default style
				if (appDefaultStyle != null) {

					ApplyBaseStyle(appDefaultStyle.DefaultRadialMenuItemStyle);
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
				if (appDefaultStyle != null) {

					ApplyActiveStyle(appDefaultStyle.DefaultRadialMenuItemStyle);
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

				if (labelStyle.ContentPadding != null)
					Base_LabelPadding = labelStyle.ContentPadding.Value;

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

				if (labelStyle.ContentPadding != null)
					Active_LabelPadding = labelStyle.ContentPadding.Value;

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

		public void SpreadBaseStyle() {

			Active_Background = Base_Background;
			Active_BorderBrush = Base_BorderBrush;
			Active_BorderThickness = Base_BorderThickness;
			Active_ContentRadius = Base_ContentRadius;
			Active_EdgeBackground = Base_EdgeBackground;
			Active_EdgeBorderBrush = Base_EdgeBorderBrush;
			Active_EdgeBorderThickness = Base_EdgeBorderThickness;
			Active_EdgeInnerRadius = Base_EdgeInnerRadius;
			Active_EdgeOuterRadius = Base_EdgeOuterRadius;
			Active_InnerRadius = Base_InnerRadius;
			Active_OuterRadius = Base_OuterRadius;

			Active_LabelBorderBrush = Base_LabelBorderBrush;
			Active_LabelCornerRadius = Base_LabelCornerRadius;
			Active_LabelBorderThickness = Base_LabelBorderThickness;
			Active_LabelBackground = Base_LabelBackground;
			Active_LabelContent = Base_LabelContent;
			Active_LabelPadding = Base_LabelPadding;
			Active_LabelMargin = Base_LabelMargin;
			Active_LabelHeight = Base_LabelHeight;
			Active_LabelWidth = Base_LabelWidth;
			Active_LabelForeground = Base_LabelForeground;
			Active_LabelFontFamily = Base_LabelFontFamily;
			Active_LabelFontSize = Base_LabelFontSize;
			Active_LabelFontWeight = Base_LabelFontWeight;
		}

		#region IconSize

		public double IconSize {
			get { return Base_LabelWidth * 0.5; }
			set {
				Base_LabelWidth = value * 2;
				Base_LabelHeight = value * 2;
			}
		}

		#endregion IconSize

		#region Active_IconSize

		public double Active_IconSize {
			get { return Active_LabelWidth * 0.5; }
			set {
				Active_LabelHeight = value * 2;
				Active_LabelWidth = value * 2;
			}
		}

		#endregion Active_IconSize
	}
}
