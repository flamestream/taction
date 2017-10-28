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

			// Apply app default style
			var appDefaultStyle = App.Instance.Config.Layout;
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

		public void ApplyBaseStyle(RadialMenuItemStyleSetSpecs styleSet) {

			if (styleSet == null)
				return;

			ApplyBaseStyle(styleSet.Base);
		}

		public void ApplyBaseStyle(RadialMenuItemStyleSpecs style) {

			if (style == null)
				return;

			var labelStyle = style.LabelSpecs;
			if (labelStyle != null) {

				var borderStyle = labelStyle.Border;
				if (borderStyle != null) {

					if (borderStyle.Color != null)
						IconBorderBrush = borderStyle.Color;

					if (borderStyle.Radius != null)
						IconCornerRadius = borderStyle.Radius.Value;

					if (borderStyle.Thickness != null)
						IconBorderThickness = borderStyle.Thickness.Value;
				}

				if (labelStyle.Color != null)
					IconBackground = labelStyle.Color;

				if (labelStyle.Content != null)
					IconContent = labelStyle.Content;

				if (labelStyle.ContentPadding != null)
					IconPadding = labelStyle.ContentPadding.Value;

				if (labelStyle.Margin != null)
					IconMargin = labelStyle.Margin.Value;

				if (labelStyle.Size != null)
					IconSize = labelStyle.Size.Value;

				var textStyle = labelStyle.TextStyle;
				if (textStyle != null) {

					if (textStyle.Color != null)
						IconForeground = textStyle.Color;

					if (textStyle.FontFamily != null)
						IconFontFamily = textStyle.FontFamily;

					if (textStyle.FontSize != null)
						IconFontSize = textStyle.FontSize.Value;

					if (textStyle.FontWeight != null)
						IconFontWeight = textStyle.FontWeight.Value;
				}
			}
		}

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
			set { IconHeight = IconWidth = value * 2; }
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
	}
}
