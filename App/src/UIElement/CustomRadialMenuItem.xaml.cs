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
						SetValue(BorderBrushProperty, borderSpecs.Color);

					if (borderSpecs.Thickness != null)
						SetValue(BorderThicknessProperty, borderSpecs.Thickness.Value);

					// No border radius
				}

				if (innerEdgeStyle.Color != null)
					SetValue(BackgroundProperty, innerEdgeStyle.Color);

				if (innerEdgeStyle.StartDistance != null)
					SetValue(InnerRadiusProperty, innerEdgeStyle.StartDistance.Value);

				if (innerEdgeStyle.Size != null)
					SetValue(OuterRadiusProperty, InnerRadius + innerEdgeStyle.Size.Value);
			}

			var outerEdgeStyle = style.OuterEdgeSpecs;
			if (outerEdgeStyle != null) {

				var borderSpecs = outerEdgeStyle.Border;
				if (borderSpecs != null) {

					if (borderSpecs.Color != null)
						SetValue(EdgeBorderBrushProperty, borderSpecs.Color);

					if (borderSpecs.Thickness != null)
						SetValue(EdgeBorderThicknessProperty, borderSpecs.Thickness.Value.Top);

					// No border radius
				}

				if (outerEdgeStyle.Color != null)
					SetValue(EdgeBackgroundProperty, outerEdgeStyle.Color);

				// Calculate default size
				var size = Math.Max(EdgeOuterRadius - EdgeInnerRadius, 0);

				if (outerEdgeStyle.StartDistance == null)
					SetValue(EdgeInnerRadiusProperty, Math.Max(EdgeInnerRadius, OuterRadius));
				else
					SetValue(EdgeInnerRadiusProperty, OuterRadius + outerEdgeStyle.StartDistance.Value);

				if (outerEdgeStyle.Size != null)
					size = outerEdgeStyle.Size.Value;

				SetValue(EdgeOuterRadiusProperty, EdgeInnerRadius + size);
			}

			var labelStyle = style.LabelSpecs;
			if (labelStyle != null) {

				var borderStyle = labelStyle.Border;
				if (borderStyle != null) {

					if (borderStyle.Color != null)
						SetValue(IconBorderBrushProperty, borderStyle.Color);

					if (borderStyle.Radius != null)
						SetValue(IconCornerRadiusProperty, borderStyle.Radius.Value);

					if (borderStyle.Thickness != null)
						SetValue(IconBorderThicknessProperty, borderStyle.Thickness.Value);
				}

				if (labelStyle.Color != null)
					SetValue(IconBackgroundProperty, labelStyle.Color);

				if (labelStyle.Content != null)
					SetValue(IconContentProperty, labelStyle.Content);

				if (labelStyle.ContentPadding != null)
					SetValue(IconPaddingProperty, labelStyle.ContentPadding.Value);

				if (labelStyle.Margin != null)
					SetValue(IconMarginProperty, labelStyle.Margin.Value);

				if (labelStyle.Size != null)
					IconSize = labelStyle.Size.Value;

				if (labelStyle.StartDistance != null)
					SetValue(ContentRadiusProperty, labelStyle.StartDistance.Value);

				var textStyle = labelStyle.TextStyle;
				if (textStyle != null) {

					if (textStyle.Color != null)
						SetValue(IconForegroundProperty, textStyle.Color);

					if (textStyle.FontFamily != null)
						SetValue(IconFontFamilyProperty, textStyle.FontFamily);

					if (textStyle.FontSize != null)
						SetValue(IconFontSizeProperty, textStyle.FontSize.Value);

					if (textStyle.FontWeight != null)
						SetValue(IconFontWeightProperty, textStyle.FontWeight.Value);
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
						SetValue(Active_BorderBrushProperty, borderSpecs.Color);

					if (borderSpecs.Thickness != null)
						SetValue(Active_BorderThicknessProperty, borderSpecs.Thickness.Value);

					// No border radius
				}

				if (innerEdgeStyle.Color != null)
					SetValue(Active_BackgroundProperty, innerEdgeStyle.Color);

				if (innerEdgeStyle.StartDistance != null)
					SetValue(Active_InnerRadiusProperty, innerEdgeStyle.StartDistance.Value);

				if (innerEdgeStyle.Size != null)
					SetValue(Active_OuterRadiusProperty, InnerRadius + innerEdgeStyle.Size.Value);
			}

			var outerEdgeStyle = style.OuterEdgeSpecs;
			if (outerEdgeStyle != null) {

				var borderSpecs = outerEdgeStyle.Border;
				if (borderSpecs != null) {

					if (borderSpecs.Color != null)
						SetValue(Active_EdgeBorderBrushProperty, borderSpecs.Color);

					if (borderSpecs.Thickness != null)
						SetValue(Active_EdgeBorderThicknessProperty, borderSpecs.Thickness.Value.Top);

					// No border radius
				}

				if (outerEdgeStyle.Color != null)
					SetValue(Active_EdgeBackgroundProperty, outerEdgeStyle.Color);

				// Calculate default size
				var size = Math.Max(EdgeOuterRadius - EdgeInnerRadius, 0);

				if (outerEdgeStyle.StartDistance == null)
					SetValue(Active_EdgeInnerRadiusProperty, Math.Max(EdgeInnerRadius, OuterRadius));
				else
					SetValue(Active_EdgeInnerRadiusProperty, OuterRadius + outerEdgeStyle.StartDistance.Value);

				if (outerEdgeStyle.Size != null)
					size = outerEdgeStyle.Size.Value;

				SetValue(Active_EdgeOuterRadiusProperty, EdgeInnerRadius + size);
			}

			var labelStyle = style.LabelSpecs;
			if (labelStyle != null) {

				var borderStyle = labelStyle.Border;
				if (borderStyle != null) {

					if (borderStyle.Color != null)
						SetValue(Active_IconBorderBrushProperty, borderStyle.Color);

					if (borderStyle.Radius != null)
						SetValue(Active_IconCornerRadiusProperty, borderStyle.Radius.Value);

					if (borderStyle.Thickness != null)
						SetValue(Active_IconBorderThicknessProperty, borderStyle.Thickness.Value);
				}

				if (labelStyle.Color != null)
					SetValue(Active_IconBackgroundProperty, labelStyle.Color);

				if (labelStyle.Content != null)
					SetValue(Active_IconContentProperty, labelStyle.Content);

				if (labelStyle.ContentPadding != null)
					SetValue(Active_IconPaddingProperty, labelStyle.ContentPadding.Value);

				if (labelStyle.Margin != null)
					SetValue(Active_IconMarginProperty, labelStyle.Margin.Value);

				if (labelStyle.Size != null)
					Active_IconSize = labelStyle.Size.Value;

				if (labelStyle.StartDistance != null)
					SetValue(Active_ContentRadiusProperty, labelStyle.StartDistance.Value);

				var textStyle = labelStyle.TextStyle;
				if (textStyle != null) {

					if (textStyle.Color != null)
						SetValue(Active_IconForegroundProperty, textStyle.Color);

					if (textStyle.FontFamily != null)
						SetValue(Active_IconFontFamilyProperty, textStyle.FontFamily);

					if (textStyle.FontSize != null)
						SetValue(Active_IconFontSizeProperty, textStyle.FontSize.Value);

					if (textStyle.FontWeight != null)
						SetValue(Active_IconFontWeightProperty, textStyle.FontWeight.Value);
				}
			}
		}

		public void SpreadBaseStyle() {

			SetValue(Active_BackgroundProperty, Background);
			SetValue(Active_BorderBrushProperty, BorderBrush);
			SetValue(Active_BorderThicknessProperty, BorderThickness);
			SetValue(Active_ContentRadiusProperty, ContentRadius);
			SetValue(Active_EdgeBackgroundProperty, EdgeBackground);
			SetValue(Active_EdgeBorderBrushProperty, EdgeBorderBrush);
			SetValue(Active_EdgeBorderThicknessProperty, EdgeBorderThickness);
			SetValue(Active_EdgeInnerRadiusProperty, EdgeInnerRadius);
			SetValue(Active_EdgeOuterRadiusProperty, EdgeOuterRadius);
			SetValue(Active_InnerRadiusProperty, InnerRadius);
			SetValue(Active_OuterRadiusProperty, OuterRadius);

			SetValue(Active_IconBorderBrushProperty, IconBorderBrush);
			SetValue(Active_IconCornerRadiusProperty, IconCornerRadius);
			SetValue(Active_IconBorderThicknessProperty, IconBorderThickness);
			SetValue(Active_IconBackgroundProperty, IconBackground);
			SetValue(Active_IconContentProperty, IconContent);
			SetValue(Active_IconPaddingProperty, IconPadding);
			SetValue(Active_IconMarginProperty, IconMargin);
			SetValue(Active_IconHeightProperty, IconHeight);
			SetValue(Active_IconWidthProperty, IconWidth);
			SetValue(Active_IconForegroundProperty, IconForeground);
			SetValue(Active_IconFontFamilyProperty, IconFontFamily);
			SetValue(Active_IconFontSizeProperty, IconFontSize);
			SetValue(Active_IconFontWeightProperty, IconFontWeight);
		}

		#region Active_BorderBrush

		public static readonly DependencyProperty Active_BorderBrushProperty = DependencyProperty.Register(
			"Active_BorderBrush",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_BorderBrush(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_BorderBrushProperty, value);
		}

		public static Brush GetActive_BorderBrush(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_BorderBrushProperty);
		}

		public Brush Active_BorderBrush {
			get { return (Brush)GetValue(Active_BorderBrushProperty); }
			set { SetValue(Active_BorderBrushProperty, value); }
		}

		#endregion Active_BorderBrush

		#region Active_BorderThickness

		public static readonly DependencyProperty Active_BorderThicknessProperty = DependencyProperty.Register(
			"Active_BorderThickness",
			typeof(Thickness),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_BorderThickness(CustomRadialMenuItem element, Thickness value) {

			element.SetValue(Active_BorderThicknessProperty, value);
		}

		public static Thickness GetActive_BorderThickness(CustomRadialMenuItem element) {
			return (Thickness)element.GetValue(Active_BorderThicknessProperty);
		}

		public Thickness Active_BorderThickness {
			get { return (Thickness)GetValue(Active_BorderThicknessProperty); }
			set { SetValue(Active_BorderThicknessProperty, value); }
		}

		#endregion Active_BorderThickness

		#region Active_Background

		public static readonly DependencyProperty Active_BackgroundProperty = DependencyProperty.Register(
			"Active_Background",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_Background(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_BackgroundProperty, value);
		}

		public static Brush GetActive_Background(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_BackgroundProperty);
		}

		public Brush Active_Background {
			get { return (Brush)GetValue(Active_BackgroundProperty); }
			set { SetValue(Active_BackgroundProperty, value); }
		}

		#endregion Active_Background

		#region Active_ContentRadius

		public static readonly DependencyProperty Active_ContentRadiusProperty = DependencyProperty.Register(
			"Active_ContentRadius",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_ContentRadius(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_ContentRadiusProperty, value);
		}

		public static double GetActive_ContentRadius(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_ContentRadiusProperty);
		}

		public double Active_ContentRadius {
			get { return (double)GetValue(Active_ContentRadiusProperty); }
			set { SetValue(Active_ContentRadiusProperty, value); }
		}

		#endregion Active_ContentRadius

		#region Active_InnerRadius

		public static readonly DependencyProperty Active_InnerRadiusProperty = DependencyProperty.Register(
			"Active_InnerRadius",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_InnerRadius(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_InnerRadiusProperty, value);
		}

		public static double GetActive_InnerRadius(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_InnerRadiusProperty);
		}

		public double Active_InnerRadius {
			get { return (double)GetValue(Active_InnerRadiusProperty); }
			set { SetValue(Active_InnerRadiusProperty, value); }
		}

		#endregion Active_InnerRadius

		#region Active_OuterRadius

		public static readonly DependencyProperty Active_OuterRadiusProperty = DependencyProperty.Register(
			"Active_OuterRadius",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_OuterRadius(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_OuterRadiusProperty, value);
		}

		public static double GetActive_OuterRadius(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_OuterRadiusProperty);
		}

		public double Active_OuterRadius {
			get { return (double)GetValue(Active_OuterRadiusProperty); }
			set { SetValue(Active_OuterRadiusProperty, value); }
		}

		#endregion Active_OuterRadius

		#region Active_EdgeBorderBrush

		public static readonly DependencyProperty Active_EdgeBorderBrushProperty = DependencyProperty.Register(
			"Active_EdgeBorderBrush",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_EdgeBorderBrush(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_EdgeBorderBrushProperty, value);
		}

		public static Brush GetActive_EdgeBorderBrush(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_EdgeBorderBrushProperty);
		}

		public Brush Active_EdgeBorderBrush {
			get { return (Brush)GetValue(Active_EdgeBorderBrushProperty); }
			set { SetValue(Active_EdgeBorderBrushProperty, value); }
		}

		#endregion Active_EdgeBorderBrush

		#region Active_EdgeBorderThickness

		public static readonly DependencyProperty Active_EdgeBorderThicknessProperty = DependencyProperty.Register(
			"Active_EdgeBorderThickness",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_EdgeBorderThickness(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_EdgeBorderThicknessProperty, value);
		}

		public static double GetActive_EdgeBorderThickness(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_EdgeBorderThicknessProperty);
		}

		public double Active_EdgeBorderThickness {
			get { return (double)GetValue(Active_EdgeBorderThicknessProperty); }
			set { SetValue(Active_EdgeBorderThicknessProperty, value); }
		}

		#endregion Active_EdgeBorderThickness

		#region Active_EdgeBackground

		public static readonly DependencyProperty Active_EdgeBackgroundProperty = DependencyProperty.Register(
			"Active_EdgeBackground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_EdgeBackground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_EdgeBackgroundProperty, value);
		}

		public static Brush GetActive_EdgeBackground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_EdgeBackgroundProperty);
		}

		public Brush Active_EdgeBackground {
			get { return (Brush)GetValue(Active_EdgeBackgroundProperty); }
			set { SetValue(Active_EdgeBackgroundProperty, value); }
		}

		#endregion Active_EdgeBackground

		#region Active_EdgeInnerRadius

		public static readonly DependencyProperty Active_EdgeInnerRadiusProperty = DependencyProperty.Register(
			"Active_EdgeInnerRadius",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_EdgeInnerRadius(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_EdgeInnerRadiusProperty, value);
		}

		public static double GetActive_EdgeInnerRadius(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_EdgeInnerRadiusProperty);
		}

		public double Active_EdgeInnerRadius {
			get { return (double)GetValue(Active_EdgeInnerRadiusProperty); }
			set { SetValue(Active_EdgeInnerRadiusProperty, value); }
		}

		#endregion Active_EdgeInnerRadius

		#region Active_EdgeOuterRadius

		public static readonly DependencyProperty Active_EdgeOuterRadiusProperty = DependencyProperty.Register(
			"Active_EdgeOuterRadius",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_EdgeOuterRadius(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_EdgeOuterRadiusProperty, value);
		}

		public static double GetActive_EdgeOuterRadius(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_EdgeOuterRadiusProperty);
		}

		public double Active_EdgeOuterRadius {
			get { return (double)GetValue(Active_EdgeOuterRadiusProperty); }
			set { SetValue(Active_EdgeOuterRadiusProperty, value); }
		}

		#endregion Active_EdgeOuterRadius

		#region IconContent

		public static readonly DependencyProperty IconContentProperty = DependencyProperty.Register(
			"IconContent",
			typeof(Object),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconContent(CustomRadialMenuItem element, Object value) {

			element.SetValue(IconContentProperty, value);
		}

		public static Object GetIconContent(CustomRadialMenuItem element) {
			return (Object)element.GetValue(IconContentProperty);
		}

		public Object IconContent {
			get { return (Object)GetValue(IconContentProperty); }
			set { SetValue(IconContentProperty, value); }
		}

		#endregion IconContent

		#region IconBackground

		public static readonly DependencyProperty IconBackgroundProperty = DependencyProperty.Register(
			"IconBackground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconBackground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(IconBackgroundProperty, value);
		}

		public static Brush GetIconBackground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(IconBackgroundProperty);
		}

		public Brush IconBackground {
			get { return (Brush)GetValue(IconBackgroundProperty); }
			set { SetValue(IconBackgroundProperty, value); }
		}

		#endregion IconBackground

		#region IconCornerRadius

		public static readonly DependencyProperty IconCornerRadiusProperty = DependencyProperty.Register(
			"IconCornerRadius",
			typeof(CornerRadius),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconCornerRadius(CustomRadialMenuItem element, CornerRadius value) {

			element.SetValue(IconCornerRadiusProperty, value);
		}

		public static CornerRadius GetIconCornerRadius(CustomRadialMenuItem element) {
			return (CornerRadius)element.GetValue(IconCornerRadiusProperty);
		}

		public CornerRadius IconCornerRadius {
			get { return (CornerRadius)GetValue(IconCornerRadiusProperty); }
			set { SetValue(IconCornerRadiusProperty, value); }
		}

		#endregion IconCornerRadius

		#region IconBorderBrush

		public static readonly DependencyProperty IconBorderBrushProperty = DependencyProperty.Register(
			"IconBorderBrush",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconBorderBrush(CustomRadialMenuItem element, Brush value) {

			element.SetValue(IconBorderBrushProperty, value);
		}

		public static Brush GetIconBorderBrush(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(IconBorderBrushProperty);
		}

		public Brush IconBorderBrush {
			get { return (Brush)GetValue(IconBorderBrushProperty); }
			set { SetValue(IconBorderBrushProperty, value); }
		}

		#endregion IconBorderBrush

		#region IconBorderThickness

		public static readonly DependencyProperty IconBorderThicknessProperty = DependencyProperty.Register(
			"IconBorderThickness",
			typeof(Thickness),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconBorderThickness(CustomRadialMenuItem element, Thickness value) {

			element.SetValue(IconBorderThicknessProperty, value);
		}

		public static Thickness GetIconBorderThickness(CustomRadialMenuItem element) {
			return (Thickness)element.GetValue(IconBorderThicknessProperty);
		}

		public Thickness IconBorderThickness {
			get { return (Thickness)GetValue(IconBorderThicknessProperty); }
			set { SetValue(IconBorderThicknessProperty, value); }
		}

		#endregion IconBorderThickness

		#region IconFontFamily

		public static readonly DependencyProperty IconFontFamilyProperty = DependencyProperty.Register(
			"IconFontFamily",
			typeof(FontFamily),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconFontFamily(CustomRadialMenuItem element, FontFamily value) {

			element.SetValue(IconFontFamilyProperty, value);
		}

		public static FontFamily GetIconFontFamily(CustomRadialMenuItem element) {
			return (FontFamily)element.GetValue(IconFontFamilyProperty);
		}

		public FontFamily IconFontFamily {
			get { return (FontFamily)GetValue(IconFontFamilyProperty); }
			set { SetValue(IconFontFamilyProperty, value); }
		}

		#endregion IconFontFamily

		#region IconFontSize

		public static readonly DependencyProperty IconFontSizeProperty = DependencyProperty.Register(
			"IconFontSize",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconFontSize(CustomRadialMenuItem element, double value) {

			element.SetValue(IconFontSizeProperty, value);
		}

		public static double GetIconFontSize(CustomRadialMenuItem element) {
			return (double)element.GetValue(IconFontSizeProperty);
		}

		public double IconFontSize {
			get { return (double)GetValue(IconFontSizeProperty); }
			set { SetValue(IconFontSizeProperty, value); }
		}

		#endregion IconFontSize

		#region IconFontWeight

		public static readonly DependencyProperty IconFontWeightProperty = DependencyProperty.Register(
			"IconFontWeight",
			typeof(FontWeight),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconFontWeight(CustomRadialMenuItem element, FontWeight value) {

			element.SetValue(IconFontWeightProperty, value);
		}

		public static FontWeight GetIconFontWeight(CustomRadialMenuItem element) {
			return (FontWeight)element.GetValue(IconFontWeightProperty);
		}

		public FontWeight IconFontWeight {
			get { return (FontWeight)GetValue(IconFontWeightProperty); }
			set { SetValue(IconFontWeightProperty, value); }
		}

		#endregion IconFontWeight

		#region IconForeground

		public static readonly DependencyProperty IconForegroundProperty = DependencyProperty.Register(
			"IconForeground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconForeground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(IconForegroundProperty, value);
		}

		public static Brush GetIconForeground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(IconForegroundProperty);
		}

		public Brush IconForeground {
			get { return (Brush)GetValue(IconForegroundProperty); }
			set { SetValue(IconForegroundProperty, value); }
		}

		#endregion IconForeground

		#region IconWidth

		public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register(
			"IconWidth",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconWidth(CustomRadialMenuItem element, double value) {

			element.SetValue(IconWidthProperty, value);
		}

		public static double GetIconWidth(CustomRadialMenuItem element) {
			return (double)element.GetValue(IconWidthProperty);
		}

		public double IconWidth {
			get { return (double)GetValue(IconWidthProperty); }
			set { SetValue(IconWidthProperty, value); }
		}

		#endregion IconWidth

		#region IconHeight

		public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register(
			"IconHeight",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconHeight(CustomRadialMenuItem element, double value) {

			element.SetValue(IconHeightProperty, value);
		}

		public static double GetIconHeight(CustomRadialMenuItem element) {
			return (double)element.GetValue(IconHeightProperty);
		}

		public double IconHeight {
			get { return (double)GetValue(IconHeightProperty); }
			set { SetValue(IconHeightProperty, value); }
		}

		#endregion IconHeight

		#region IconSize

		public double IconSize {
			get { return IconWidth * 0.5; }
			set {
				SetValue(IconWidthProperty, value * 2);
				SetValue(IconHeightProperty, value * 2);
			}
		}

		#endregion IconSize

		#region IconMargin

		public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register(
			"IconMargin",
			typeof(Thickness),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconMargin(CustomRadialMenuItem element, Brush value) {

			element.SetValue(IconMarginProperty, value);
		}

		public static Thickness GetIconMargin(CustomRadialMenuItem element) {
			return (Thickness)element.GetValue(IconMarginProperty);
		}

		public Thickness IconMargin {
			get { return (Thickness)GetValue(IconMarginProperty); }
			set { SetValue(IconMarginProperty, value); }
		}

		#endregion IconMargin

		#region IconPadding

		public static readonly DependencyProperty IconPaddingProperty = DependencyProperty.Register(
			"IconPadding",
			typeof(Thickness),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconPadding(CustomRadialMenuItem element, Brush value) {

			element.SetValue(IconPaddingProperty, value);
		}

		public static Thickness GetIconPadding(CustomRadialMenuItem element) {
			return (Thickness)element.GetValue(IconPaddingProperty);
		}

		public Thickness IconContentPadding {
			get { return (Thickness)GetValue(IconPaddingProperty); }
			set { SetValue(IconPaddingProperty, value); }
		}

		public Thickness IconPadding {
			get { return (Thickness)GetValue(IconPaddingProperty); }
			set { SetValue(IconPaddingProperty, value); }
		}

		#endregion IconPadding

		#region Active_IconContent

		public static readonly DependencyProperty Active_IconContentProperty = DependencyProperty.Register(
			"Active_IconContent",
			typeof(Object),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconContent(CustomRadialMenuItem element, Object value) {

			element.SetValue(Active_IconContentProperty, value);
		}

		public static Object GetActive_IconContent(CustomRadialMenuItem element) {
			return (Object)element.GetValue(Active_IconContentProperty);
		}

		public Object Active_IconContent {
			get { return (Object)GetValue(Active_IconContentProperty); }
			set { SetValue(Active_IconContentProperty, value); }
		}

		#endregion Active_IconContent

		#region Active_IconBackground

		public static readonly DependencyProperty Active_IconBackgroundProperty = DependencyProperty.Register(
			"Active_IconBackground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconBackground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_IconBackgroundProperty, value);
		}

		public static Brush GetActive_IconBackground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_IconBackgroundProperty);
		}

		public Brush Active_IconBackground {
			get { return (Brush)GetValue(Active_IconBackgroundProperty); }
			set { SetValue(Active_IconBackgroundProperty, value); }
		}

		#endregion Active_IconBackground

		#region Active_IconCornerRadius

		public static readonly DependencyProperty Active_IconCornerRadiusProperty = DependencyProperty.Register(
			"Active_IconCornerRadius",
			typeof(CornerRadius),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconCornerRadius(CustomRadialMenuItem element, CornerRadius value) {

			element.SetValue(Active_IconCornerRadiusProperty, value);
		}

		public static CornerRadius GetActive_IconCornerRadius(CustomRadialMenuItem element) {
			return (CornerRadius)element.GetValue(Active_IconCornerRadiusProperty);
		}

		public CornerRadius Active_IconCornerRadius {
			get { return (CornerRadius)GetValue(Active_IconCornerRadiusProperty); }
			set { SetValue(Active_IconCornerRadiusProperty, value); }
		}

		#endregion Active_IconCornerRadius

		#region Active_IconBorderBrush

		public static readonly DependencyProperty Active_IconBorderBrushProperty = DependencyProperty.Register(
			"Active_IconBorderBrush",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconBorderBrush(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_IconBorderBrushProperty, value);
		}

		public static Brush GetActive_IconBorderBrush(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_IconBorderBrushProperty);
		}

		public Brush Active_IconBorderBrush {
			get { return (Brush)GetValue(Active_IconBorderBrushProperty); }
			set { SetValue(Active_IconBorderBrushProperty, value); }
		}

		#endregion Active_IconBorderBrush

		#region Active_IconBorderThickness

		public static readonly DependencyProperty Active_IconBorderThicknessProperty = DependencyProperty.Register(
			"Active_IconBorderThickness",
			typeof(Thickness),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconBorderThickness(CustomRadialMenuItem element, Thickness value) {

			element.SetValue(Active_IconBorderThicknessProperty, value);
		}

		public static Thickness GetActive_IconBorderThickness(CustomRadialMenuItem element) {
			return (Thickness)element.GetValue(Active_IconBorderThicknessProperty);
		}

		public Thickness Active_IconBorderThickness {
			get { return (Thickness)GetValue(Active_IconBorderThicknessProperty); }
			set { SetValue(Active_IconBorderThicknessProperty, value); }
		}

		#endregion Active_IconBorderThickness

		#region Active_IconFontFamily

		public static readonly DependencyProperty Active_IconFontFamilyProperty = DependencyProperty.Register(
			"Active_IconFontFamily",
			typeof(FontFamily),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconFontFamily(CustomRadialMenuItem element, FontFamily value) {

			element.SetValue(Active_IconFontFamilyProperty, value);
		}

		public static FontFamily GetActive_IconFontFamily(CustomRadialMenuItem element) {
			return (FontFamily)element.GetValue(Active_IconFontFamilyProperty);
		}

		public FontFamily Active_IconFontFamily {
			get { return (FontFamily)GetValue(Active_IconFontFamilyProperty); }
			set { SetValue(Active_IconFontFamilyProperty, value); }
		}

		#endregion Active_IconFontFamily

		#region Active_IconFontSize

		public static readonly DependencyProperty Active_IconFontSizeProperty = DependencyProperty.Register(
			"Active_IconFontSize",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconFontSize(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_IconFontSizeProperty, value);
		}

		public static double GetActive_IconFontSize(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_IconFontSizeProperty);
		}

		public double Active_IconFontSize {
			get { return (double)GetValue(Active_IconFontSizeProperty); }
			set { SetValue(Active_IconFontSizeProperty, value); }
		}

		#endregion Active_IconFontSize

		#region Active_IconFontWeight

		public static readonly DependencyProperty Active_IconFontWeightProperty = DependencyProperty.Register(
			"Active_IconFontWeight",
			typeof(FontWeight),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconFontWeight(CustomRadialMenuItem element, FontWeight value) {

			element.SetValue(Active_IconFontWeightProperty, value);
		}

		public static FontWeight GetActive_IconFontWeight(CustomRadialMenuItem element) {
			return (FontWeight)element.GetValue(Active_IconFontWeightProperty);
		}

		public FontWeight Active_IconFontWeight {
			get { return (FontWeight)GetValue(Active_IconFontWeightProperty); }
			set { SetValue(Active_IconFontWeightProperty, value); }
		}

		#endregion Active_IconFontWeight

		#region Active_IconForeground

		public static readonly DependencyProperty Active_IconForegroundProperty = DependencyProperty.Register(
			"Active_IconForeground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconForeground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_IconForegroundProperty, value);
		}

		public static Brush GetActive_IconForeground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_IconForegroundProperty);
		}

		public Brush Active_IconForeground {
			get { return (Brush)GetValue(Active_IconForegroundProperty); }
			set { SetValue(Active_IconForegroundProperty, value); }
		}

		#endregion Active_IconForeground

		#region Active_IconWidth

		public static readonly DependencyProperty Active_IconWidthProperty = DependencyProperty.Register(
			"Active_IconWidth",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconWidth(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_IconWidthProperty, value);
		}

		public static double GetActive_IconWidth(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_IconWidthProperty);
		}

		public double Active_IconWidth {
			get { return (double)GetValue(Active_IconWidthProperty); }
			set { SetValue(Active_IconWidthProperty, value); }
		}

		#endregion Active_IconWidth

		#region Active_IconHeight

		public static readonly DependencyProperty Active_IconHeightProperty = DependencyProperty.Register(
			"Active_IconHeight",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconHeight(CustomRadialMenuItem element, double value) {

			element.SetValue(Active_IconHeightProperty, value);
		}

		public static double GetActive_IconHeight(CustomRadialMenuItem element) {
			return (double)element.GetValue(Active_IconHeightProperty);
		}

		public double Active_IconHeight {
			get { return (double)GetValue(Active_IconHeightProperty); }
			set { SetValue(Active_IconHeightProperty, value); }
		}

		#endregion Active_IconHeight

		#region Active_IconSize

		public double Active_IconSize {
			get { return Active_IconWidth * 0.5; }
			set {
				SetValue(Active_IconHeightProperty, value * 2);
				SetValue(Active_IconWidthProperty, value * 2);
			}
		}

		#endregion Active_IconSize

		#region Active_IconMargin

		public static readonly DependencyProperty Active_IconMarginProperty = DependencyProperty.Register(
			"Active_IconMargin",
			typeof(Thickness),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconMargin(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_IconMarginProperty, value);
		}

		public static Thickness GetActive_IconMargin(CustomRadialMenuItem element) {
			return (Thickness)element.GetValue(Active_IconMarginProperty);
		}

		public Thickness Active_IconMargin {
			get { return (Thickness)GetValue(Active_IconMarginProperty); }
			set { SetValue(Active_IconMarginProperty, value); }
		}

		#endregion Active_IconMargin

		#region Active_IconPadding

		public static readonly DependencyProperty Active_IconPaddingProperty = DependencyProperty.Register(
			"Active_IconPadding",
			typeof(Thickness),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_IconPadding(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_IconPaddingProperty, value);
		}

		public static Thickness GetActive_IconPadding(CustomRadialMenuItem element) {
			return (Thickness)element.GetValue(Active_IconPaddingProperty);
		}

		public Thickness Active_IconContentPadding {
			get { return (Thickness)GetValue(Active_IconPaddingProperty); }
			set { SetValue(Active_IconPaddingProperty, value); }
		}

		public Thickness Active_IconPadding {
			get { return (Thickness)GetValue(Active_IconPaddingProperty); }
			set { SetValue(Active_IconPaddingProperty, value); }
		}

		#endregion Active_IconPadding
	}
}
