using System;
using System.Windows;
using System.Windows.Media;

namespace Taction.UIElement {

	public partial class CustomRadialMenuItem {

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
			typeof(Thickness?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_BorderThickness(CustomRadialMenuItem element, Thickness? value) {

			element.SetValue(Active_BorderThicknessProperty, value);
		}

		public static Thickness? GetActive_BorderThickness(CustomRadialMenuItem element) {
			return (Thickness?)element.GetValue(Active_BorderThicknessProperty);
		}

		public Thickness? Active_BorderThickness {
			get { return (Thickness?)GetValue(Active_BorderThicknessProperty); }
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
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_ContentRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_ContentRadiusProperty, value);
		}

		public static double? GetActive_ContentRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_ContentRadiusProperty);
		}

		public double? Active_ContentRadius {
			get { return (double?)GetValue(Active_ContentRadiusProperty); }
			set { SetValue(Active_ContentRadiusProperty, value); }
		}

		#endregion Active_ContentRadius

		#region Active_InnerRadius

		public static readonly DependencyProperty Active_InnerRadiusProperty = DependencyProperty.Register(
			"Active_InnerRadius",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_InnerRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_InnerRadiusProperty, value);
		}

		public static double? GetActive_InnerRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_InnerRadiusProperty);
		}

		public double? Active_InnerRadius {
			get { return (double?)GetValue(Active_InnerRadiusProperty); }
			set { SetValue(Active_InnerRadiusProperty, value); }
		}

		#endregion Active_InnerRadius

		#region Active_OuterRadius

		public static readonly DependencyProperty Active_OuterRadiusProperty = DependencyProperty.Register(
			"Active_OuterRadius",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_OuterRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_OuterRadiusProperty, value);
		}

		public static double? GetActive_OuterRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_OuterRadiusProperty);
		}

		public double? Active_OuterRadius {
			get { return (double?)GetValue(Active_OuterRadiusProperty); }
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
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_EdgeBorderThickness(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_EdgeBorderThicknessProperty, value);
		}

		public static double? GetActive_EdgeBorderThickness(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_EdgeBorderThicknessProperty);
		}

		public double? Active_EdgeBorderThickness {
			get { return (double?)GetValue(Active_EdgeBorderThicknessProperty); }
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
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_EdgeInnerRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_EdgeInnerRadiusProperty, value);
		}

		public static double? GetActive_EdgeInnerRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_EdgeInnerRadiusProperty);
		}

		public double? Active_EdgeInnerRadius {
			get { return (double?)GetValue(Active_EdgeInnerRadiusProperty); }
			set { SetValue(Active_EdgeInnerRadiusProperty, value); }
		}

		#endregion Active_EdgeInnerRadius

		#region Active_EdgeOuterRadius

		public static readonly DependencyProperty Active_EdgeOuterRadiusProperty = DependencyProperty.Register(
			"Active_EdgeOuterRadius",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_EdgeOuterRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_EdgeOuterRadiusProperty, value);
		}

		public static double? GetActive_EdgeOuterRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_EdgeOuterRadiusProperty);
		}

		public double? Active_EdgeOuterRadius {
			get { return (double?)GetValue(Active_EdgeOuterRadiusProperty); }
			set { SetValue(Active_EdgeOuterRadiusProperty, value); }
		}

		#endregion Active_EdgeOuterRadius

		#region Active_LabelContent

		public static readonly DependencyProperty Active_LabelContentProperty = DependencyProperty.Register(
			"Active_LabelContent",
			typeof(Object),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelContent(CustomRadialMenuItem element, Object value) {

			element.SetValue(Active_LabelContentProperty, value);
		}

		public static Object GetActive_LabelContent(CustomRadialMenuItem element) {
			return (Object)element.GetValue(Active_LabelContentProperty);
		}

		public Object Active_LabelContent {
			get { return (Object)GetValue(Active_LabelContentProperty); }
			set { SetValue(Active_LabelContentProperty, value); }
		}

		#endregion Active_LabelContent

		#region Active_LabelBackground

		public static readonly DependencyProperty Active_LabelBackgroundProperty = DependencyProperty.Register(
			"Active_LabelBackground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelBackground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_LabelBackgroundProperty, value);
		}

		public static Brush GetActive_LabelBackground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_LabelBackgroundProperty);
		}

		public Brush Active_LabelBackground {
			get { return (Brush)GetValue(Active_LabelBackgroundProperty); }
			set { SetValue(Active_LabelBackgroundProperty, value); }
		}

		#endregion Active_LabelBackground

		#region Active_LabelCornerRadius

		public static readonly DependencyProperty Active_LabelCornerRadiusProperty = DependencyProperty.Register(
			"Active_LabelCornerRadius",
			typeof(CornerRadius?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelCornerRadius(CustomRadialMenuItem element, CornerRadius? value) {

			element.SetValue(Active_LabelCornerRadiusProperty, value);
		}

		public static CornerRadius? GetActive_LabelCornerRadius(CustomRadialMenuItem element) {
			return (CornerRadius?)element.GetValue(Active_LabelCornerRadiusProperty);
		}

		public CornerRadius? Active_LabelCornerRadius {
			get { return (CornerRadius?)GetValue(Active_LabelCornerRadiusProperty); }
			set { SetValue(Active_LabelCornerRadiusProperty, value); }
		}

		#endregion Active_LabelCornerRadius

		#region Active_LabelBorderBrush

		public static readonly DependencyProperty Active_LabelBorderBrushProperty = DependencyProperty.Register(
			"Active_LabelBorderBrush",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelBorderBrush(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_LabelBorderBrushProperty, value);
		}

		public static Brush GetActive_LabelBorderBrush(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_LabelBorderBrushProperty);
		}

		public Brush Active_LabelBorderBrush {
			get { return (Brush)GetValue(Active_LabelBorderBrushProperty); }
			set { SetValue(Active_LabelBorderBrushProperty, value); }
		}

		#endregion Active_LabelBorderBrush

		#region Active_LabelBorderThickness

		public static readonly DependencyProperty Active_LabelBorderThicknessProperty = DependencyProperty.Register(
			"Active_LabelBorderThickness",
			typeof(Thickness?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelBorderThickness(CustomRadialMenuItem element, Thickness? value) {

			element.SetValue(Active_LabelBorderThicknessProperty, value);
		}

		public static Thickness? GetActive_LabelBorderThickness(CustomRadialMenuItem element) {
			return (Thickness?)element.GetValue(Active_LabelBorderThicknessProperty);
		}

		public Thickness? Active_LabelBorderThickness {
			get { return (Thickness?)GetValue(Active_LabelBorderThicknessProperty); }
			set { SetValue(Active_LabelBorderThicknessProperty, value); }
		}

		#endregion Active_LabelBorderThickness

		#region Active_LabelFontFamily

		public static readonly DependencyProperty Active_LabelFontFamilyProperty = DependencyProperty.Register(
			"Active_LabelFontFamily",
			typeof(FontFamily),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelFontFamily(CustomRadialMenuItem element, FontFamily value) {

			element.SetValue(Active_LabelFontFamilyProperty, value);
		}

		public static FontFamily GetActive_LabelFontFamily(CustomRadialMenuItem element) {
			return (FontFamily)element.GetValue(Active_LabelFontFamilyProperty);
		}

		public FontFamily Active_LabelFontFamily {
			get { return (FontFamily)GetValue(Active_LabelFontFamilyProperty); }
			set { SetValue(Active_LabelFontFamilyProperty, value); }
		}

		#endregion Active_LabelFontFamily

		#region Active_LabelFontSize

		public static readonly DependencyProperty Active_LabelFontSizeProperty = DependencyProperty.Register(
			"Active_LabelFontSize",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelFontSize(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_LabelFontSizeProperty, value);
		}

		public static double? GetActive_LabelFontSize(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_LabelFontSizeProperty);
		}

		public double? Active_LabelFontSize {
			get { return (double?)GetValue(Active_LabelFontSizeProperty); }
			set { SetValue(Active_LabelFontSizeProperty, value); }
		}

		#endregion Active_LabelFontSize

		#region Active_LabelFontWeight

		public static readonly DependencyProperty Active_LabelFontWeightProperty = DependencyProperty.Register(
			"Active_LabelFontWeight",
			typeof(FontWeight?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelFontWeight(CustomRadialMenuItem element, FontWeight? value) {

			element.SetValue(Active_LabelFontWeightProperty, value);
		}

		public static FontWeight? GetActive_LabelFontWeight(CustomRadialMenuItem element) {
			return (FontWeight?)element.GetValue(Active_LabelFontWeightProperty);
		}

		public FontWeight? Active_LabelFontWeight {
			get { return (FontWeight?)GetValue(Active_LabelFontWeightProperty); }
			set { SetValue(Active_LabelFontWeightProperty, value); }
		}

		#endregion Active_LabelFontWeight

		#region Active_LabelForeground

		public static readonly DependencyProperty Active_LabelForegroundProperty = DependencyProperty.Register(
			"Active_LabelForeground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelForeground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_LabelForegroundProperty, value);
		}

		public static Brush GetActive_LabelForeground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Active_LabelForegroundProperty);
		}

		public Brush Active_LabelForeground {
			get { return (Brush)GetValue(Active_LabelForegroundProperty); }
			set { SetValue(Active_LabelForegroundProperty, value); }
		}

		#endregion Active_LabelForeground

		#region Active_LabelWidth

		public static readonly DependencyProperty Active_LabelWidthProperty = DependencyProperty.Register(
			"Active_LabelWidth",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelWidth(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_LabelWidthProperty, value);
		}

		public static double? GetActive_LabelWidth(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_LabelWidthProperty);
		}

		public double? Active_LabelWidth {
			get { return (double?)GetValue(Active_LabelWidthProperty); }
			set { SetValue(Active_LabelWidthProperty, value); }
		}

		#endregion Active_LabelWidth

		#region Active_LabelHeight

		public static readonly DependencyProperty Active_LabelHeightProperty = DependencyProperty.Register(
			"Active_LabelHeight",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelHeight(CustomRadialMenuItem element, double? value) {

			element.SetValue(Active_LabelHeightProperty, value);
		}

		public static double? GetActive_LabelHeight(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Active_LabelHeightProperty);
		}

		public double? Active_LabelHeight {
			get { return (double?)GetValue(Active_LabelHeightProperty); }
			set { SetValue(Active_LabelHeightProperty, value); }
		}

		#endregion Active_LabelHeight

		#region Active_LabelMargin

		public static readonly DependencyProperty Active_LabelMarginProperty = DependencyProperty.Register(
			"Active_LabelMargin",
			typeof(Thickness?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelMargin(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_LabelMarginProperty, value);
		}

		public static Thickness? GetActive_LabelMargin(CustomRadialMenuItem element) {
			return (Thickness?)element.GetValue(Active_LabelMarginProperty);
		}

		public Thickness? Active_LabelMargin {
			get { return (Thickness?)GetValue(Active_LabelMarginProperty); }
			set { SetValue(Active_LabelMarginProperty, value); }
		}

		#endregion Active_LabelMargin

		#region Active_LabelPadding

		public static readonly DependencyProperty Active_LabelPaddingProperty = DependencyProperty.Register(
			"Active_LabelPadding",
			typeof(Thickness?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetActive_LabelPadding(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Active_LabelPaddingProperty, value);
		}

		public static Thickness? GetActive_LabelPadding(CustomRadialMenuItem element) {
			return (Thickness?)element.GetValue(Active_LabelPaddingProperty);
		}

		public Thickness? Active_LabelContentPadding {
			get { return (Thickness?)GetValue(Active_LabelPaddingProperty); }
			set { SetValue(Active_LabelPaddingProperty, value); }
		}

		public Thickness? Active_LabelPadding {
			get { return (Thickness?)GetValue(Active_LabelPaddingProperty); }
			set { SetValue(Active_LabelPaddingProperty, value); }
		}

		#endregion Active_LabelPadding
	}
}
