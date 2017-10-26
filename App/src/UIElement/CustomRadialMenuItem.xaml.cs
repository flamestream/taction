using RadialMenu.Controls;
using System;
using System.Windows;
using System.Windows.Media;

namespace Taction.UIElement {

	public partial class CustomRadialMenuItem : RadialMenuItem {

		public CustomRadialMenuItem() {

			InitializeComponent();
			LayoutRoot.DataContext = this;
		}

		public CustomRadialMenuItem(RadialMenuItemSpecs specs, RadialMenuItemSetSpecs defaultSpecs) : this() {

			var style = specs.Style;
			if (style == null)
				return;

			var baseStyle = style.Base;
			if (baseStyle == null)
				return;

			var labelSpecs = baseStyle.LabelSpecs;
			if (labelSpecs != null) {

				var content = labelSpecs.Content;
				if (content != null)
					IconContent = content;
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

		#region IconSize

		public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
			"IconSize",
			typeof(double),
			typeof(CustomRadialMenuItem)
		);

		public static void SetIconSize(CustomRadialMenuItem element, Brush value) {

			element.SetValue(IconSizeProperty, value);
		}

		public static double GetIconSize(CustomRadialMenuItem element) {
			return (double)element.GetValue(IconSizeProperty);
		}

		public double IconSize {
			get { return (double)GetValue(IconSizeProperty); }
			set { SetValue(IconSizeProperty, value); }
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
