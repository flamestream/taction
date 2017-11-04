using System;
using System.Windows;
using System.Windows.Media;

namespace Taction.UIElement {

	public partial class CustomRadialMenuItem {

		#region Base_BorderBrush

		public static readonly DependencyProperty Base_BorderBrushProperty = DependencyProperty.Register(
			"Base_BorderBrush",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_BorderBrush(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_BorderBrushProperty, value);
		}

		public static Brush GetBase_BorderBrush(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Base_BorderBrushProperty);
		}

		public Brush Base_BorderBrush {
			get { return (Brush)GetValue(Base_BorderBrushProperty); }
			set { SetValue(Base_BorderBrushProperty, value); }
		}

		#endregion Base_BorderBrush

		#region Base_BorderThickness

		public static readonly DependencyProperty Base_BorderThicknessProperty = DependencyProperty.Register(
			"Base_BorderThickness",
			typeof(Thickness?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_BorderThickness(CustomRadialMenuItem element, Thickness? value) {

			element.SetValue(Base_BorderThicknessProperty, value);
		}

		public static Thickness? GetBase_BorderThickness(CustomRadialMenuItem element) {
			return (Thickness?)element.GetValue(Base_BorderThicknessProperty);
		}

		public Thickness? Base_BorderThickness {
			get { return (Thickness?)GetValue(Base_BorderThicknessProperty); }
			set { SetValue(Base_BorderThicknessProperty, value); }
		}

		#endregion Base_BorderThickness

		#region Base_Background

		public static readonly DependencyProperty Base_BackgroundProperty = DependencyProperty.Register(
			"Base_Background",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_Background(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_BackgroundProperty, value);
		}

		public static Brush GetBase_Background(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Base_BackgroundProperty);
		}

		public Brush Base_Background {
			get { return (Brush)GetValue(Base_BackgroundProperty); }
			set { SetValue(Base_BackgroundProperty, value); }
		}

		#endregion Base_Background

		#region Base_ContentRadius

		public static readonly DependencyProperty Base_ContentRadiusProperty = DependencyProperty.Register(
			"Base_ContentRadius",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_ContentRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_ContentRadiusProperty, value);
		}

		public static double? GetBase_ContentRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_ContentRadiusProperty);
		}

		public double? Base_ContentRadius {
			get { return (double?)GetValue(Base_ContentRadiusProperty); }
			set { SetValue(Base_ContentRadiusProperty, value); }
		}

		#endregion Base_ContentRadius

		#region Base_InnerRadius

		public static readonly DependencyProperty Base_InnerRadiusProperty = DependencyProperty.Register(
			"Base_InnerRadius",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_InnerRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_InnerRadiusProperty, value);
		}

		public static double? GetBase_InnerRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_InnerRadiusProperty);
		}

		public double? Base_InnerRadius {
			get { return (double?)GetValue(Base_InnerRadiusProperty); }
			set { SetValue(Base_InnerRadiusProperty, value); }
		}

		#endregion Base_InnerRadius

		#region Base_OuterRadius

		public static readonly DependencyProperty Base_OuterRadiusProperty = DependencyProperty.Register(
			"Base_OuterRadius",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_OuterRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_OuterRadiusProperty, value);
		}

		public static double? GetBase_OuterRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_OuterRadiusProperty);
		}

		public double? Base_OuterRadius {
			get { return (double?)GetValue(Base_OuterRadiusProperty); }
			set { SetValue(Base_OuterRadiusProperty, value); }
		}

		#endregion Base_OuterRadius

		#region Base_EdgeBorderBrush

		public static readonly DependencyProperty Base_EdgeBorderBrushProperty = DependencyProperty.Register(
			"Base_EdgeBorderBrush",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_EdgeBorderBrush(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_EdgeBorderBrushProperty, value);
		}

		public static Brush GetBase_EdgeBorderBrush(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Base_EdgeBorderBrushProperty);
		}

		public Brush Base_EdgeBorderBrush {
			get { return (Brush)GetValue(Base_EdgeBorderBrushProperty); }
			set { SetValue(Base_EdgeBorderBrushProperty, value); }
		}

		#endregion Base_EdgeBorderBrush

		#region Base_EdgeBorderThickness

		public static readonly DependencyProperty Base_EdgeBorderThicknessProperty = DependencyProperty.Register(
			"Base_EdgeBorderThickness",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_EdgeBorderThickness(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_EdgeBorderThicknessProperty, value);
		}

		public static double? GetBase_EdgeBorderThickness(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_EdgeBorderThicknessProperty);
		}

		public double? Base_EdgeBorderThickness {
			get { return (double?)GetValue(Base_EdgeBorderThicknessProperty); }
			set { SetValue(Base_EdgeBorderThicknessProperty, value); }
		}

		#endregion Base_EdgeBorderThickness

		#region Base_EdgeBackground

		public static readonly DependencyProperty Base_EdgeBackgroundProperty = DependencyProperty.Register(
			"Base_EdgeBackground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_EdgeBackground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_EdgeBackgroundProperty, value);
		}

		public static Brush GetBase_EdgeBackground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Base_EdgeBackgroundProperty);
		}

		public Brush Base_EdgeBackground {
			get { return (Brush)GetValue(Base_EdgeBackgroundProperty); }
			set { SetValue(Base_EdgeBackgroundProperty, value); }
		}

		#endregion Base_EdgeBackground

		#region Base_EdgeInnerRadius

		public static readonly DependencyProperty Base_EdgeInnerRadiusProperty = DependencyProperty.Register(
			"Base_EdgeInnerRadius",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_EdgeInnerRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_EdgeInnerRadiusProperty, value);
		}

		public static double? GetBase_EdgeInnerRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_EdgeInnerRadiusProperty);
		}

		public double? Base_EdgeInnerRadius {
			get { return (double?)GetValue(Base_EdgeInnerRadiusProperty); }
			set { SetValue(Base_EdgeInnerRadiusProperty, value); }
		}

		#endregion Base_EdgeInnerRadius

		#region Base_EdgeOuterRadius

		public static readonly DependencyProperty Base_EdgeOuterRadiusProperty = DependencyProperty.Register(
			"Base_EdgeOuterRadius",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_EdgeOuterRadius(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_EdgeOuterRadiusProperty, value);
		}

		public static double? GetBase_EdgeOuterRadius(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_EdgeOuterRadiusProperty);
		}

		public double? Base_EdgeOuterRadius {
			get { return (double?)GetValue(Base_EdgeOuterRadiusProperty); }
			set { SetValue(Base_EdgeOuterRadiusProperty, value); }
		}

		#endregion Base_EdgeOuterRadius

		#region Base_LabelContent

		public static readonly DependencyProperty Base_LabelContentProperty = DependencyProperty.Register(
			"Base_LabelContent",
			typeof(Object),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelContent(CustomRadialMenuItem element, Object value) {

			element.SetValue(Base_LabelContentProperty, value);
		}

		public static Object GetBase_LabelContent(CustomRadialMenuItem element) {
			return (Object)element.GetValue(Base_LabelContentProperty);
		}

		public Object Base_LabelContent {
			get { return (Object)GetValue(Base_LabelContentProperty); }
			set { SetValue(Base_LabelContentProperty, value); }
		}

		#endregion Base_LabelContent

		#region Base_LabelBackground

		public static readonly DependencyProperty Base_LabelBackgroundProperty = DependencyProperty.Register(
			"Base_LabelBackground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelBackground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_LabelBackgroundProperty, value);
		}

		public static Brush GetBase_LabelBackground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Base_LabelBackgroundProperty);
		}

		public Brush Base_LabelBackground {
			get { return (Brush)GetValue(Base_LabelBackgroundProperty); }
			set { SetValue(Base_LabelBackgroundProperty, value); }
		}

		#endregion Base_LabelBackground

		#region Base_LabelCornerRadius

		public static readonly DependencyProperty Base_LabelCornerRadiusProperty = DependencyProperty.Register(
			"Base_LabelCornerRadius",
			typeof(CornerRadius?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelCornerRadius(CustomRadialMenuItem element, CornerRadius? value) {

			element.SetValue(Base_LabelCornerRadiusProperty, value);
		}

		public static CornerRadius? GetBase_LabelCornerRadius(CustomRadialMenuItem element) {
			return (CornerRadius?)element.GetValue(Base_LabelCornerRadiusProperty);
		}

		public CornerRadius? Base_LabelCornerRadius {
			get { return (CornerRadius?)GetValue(Base_LabelCornerRadiusProperty); }
			set { SetValue(Base_LabelCornerRadiusProperty, value); }
		}

		#endregion Base_LabelCornerRadius

		#region Base_LabelBorderBrush

		public static readonly DependencyProperty Base_LabelBorderBrushProperty = DependencyProperty.Register(
			"Base_LabelBorderBrush",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelBorderBrush(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_LabelBorderBrushProperty, value);
		}

		public static Brush GetBase_LabelBorderBrush(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Base_LabelBorderBrushProperty);
		}

		public Brush Base_LabelBorderBrush {
			get { return (Brush)GetValue(Base_LabelBorderBrushProperty); }
			set { SetValue(Base_LabelBorderBrushProperty, value); }
		}

		#endregion Base_LabelBorderBrush

		#region Base_LabelBorderThickness

		public static readonly DependencyProperty Base_LabelBorderThicknessProperty = DependencyProperty.Register(
			"Base_LabelBorderThickness",
			typeof(Thickness?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelBorderThickness(CustomRadialMenuItem element, Thickness? value) {

			element.SetValue(Base_LabelBorderThicknessProperty, value);
		}

		public static Thickness? GetBase_LabelBorderThickness(CustomRadialMenuItem element) {
			return (Thickness?)element.GetValue(Base_LabelBorderThicknessProperty);
		}

		public Thickness? Base_LabelBorderThickness {
			get { return (Thickness?)GetValue(Base_LabelBorderThicknessProperty); }
			set { SetValue(Base_LabelBorderThicknessProperty, value); }
		}

		#endregion Base_LabelBorderThickness

		#region Base_LabelFontFamily

		public static readonly DependencyProperty Base_LabelFontFamilyProperty = DependencyProperty.Register(
			"Base_LabelFontFamily",
			typeof(FontFamily),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelFontFamily(CustomRadialMenuItem element, FontFamily value) {

			element.SetValue(Base_LabelFontFamilyProperty, value);
		}

		public static FontFamily GetBase_LabelFontFamily(CustomRadialMenuItem element) {
			return (FontFamily)element.GetValue(Base_LabelFontFamilyProperty);
		}

		public FontFamily Base_LabelFontFamily {
			get { return (FontFamily)GetValue(Base_LabelFontFamilyProperty); }
			set { SetValue(Base_LabelFontFamilyProperty, value); }
		}

		#endregion Base_LabelFontFamily

		#region Base_LabelFontSize

		public static readonly DependencyProperty Base_LabelFontSizeProperty = DependencyProperty.Register(
			"Base_LabelFontSize",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelFontSize(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_LabelFontSizeProperty, value);
		}

		public static double? GetBase_LabelFontSize(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_LabelFontSizeProperty);
		}

		public double? Base_LabelFontSize {
			get { return (double?)GetValue(Base_LabelFontSizeProperty); }
			set { SetValue(Base_LabelFontSizeProperty, value); }
		}

		#endregion Base_LabelFontSize

		#region Base_LabelFontWeight

		public static readonly DependencyProperty Base_LabelFontWeightProperty = DependencyProperty.Register(
			"Base_LabelFontWeight",
			typeof(FontWeight?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelFontWeight(CustomRadialMenuItem element, FontWeight? value) {

			element.SetValue(Base_LabelFontWeightProperty, value);
		}

		public static FontWeight? GetBase_LabelFontWeight(CustomRadialMenuItem element) {
			return (FontWeight?)element.GetValue(Base_LabelFontWeightProperty);
		}

		public FontWeight? Base_LabelFontWeight {
			get { return (FontWeight?)GetValue(Base_LabelFontWeightProperty); }
			set { SetValue(Base_LabelFontWeightProperty, value); }
		}

		#endregion Base_LabelFontWeight

		#region Base_LabelForeground

		public static readonly DependencyProperty Base_LabelForegroundProperty = DependencyProperty.Register(
			"Base_LabelForeground",
			typeof(Brush),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelForeground(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_LabelForegroundProperty, value);
		}

		public static Brush GetBase_LabelForeground(CustomRadialMenuItem element) {
			return (Brush)element.GetValue(Base_LabelForegroundProperty);
		}

		public Brush Base_LabelForeground {
			get { return (Brush)GetValue(Base_LabelForegroundProperty); }
			set { SetValue(Base_LabelForegroundProperty, value); }
		}

		#endregion Base_LabelForeground

		#region Base_LabelWidth

		public static readonly DependencyProperty Base_LabelWidthProperty = DependencyProperty.Register(
			"Base_LabelWidth",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelWidth(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_LabelWidthProperty, value);
		}

		public static double? GetBase_LabelWidth(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_LabelWidthProperty);
		}

		public double? Base_LabelWidth {
			get { return (double?)GetValue(Base_LabelWidthProperty); }
			set { SetValue(Base_LabelWidthProperty, value); }
		}

		#endregion Base_LabelWidth

		#region Base_LabelHeight

		public static readonly DependencyProperty Base_LabelHeightProperty = DependencyProperty.Register(
			"Base_LabelHeight",
			typeof(double?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelHeight(CustomRadialMenuItem element, double? value) {

			element.SetValue(Base_LabelHeightProperty, value);
		}

		public static double? GetBase_LabelHeight(CustomRadialMenuItem element) {
			return (double?)element.GetValue(Base_LabelHeightProperty);
		}

		public double? Base_LabelHeight {
			get { return (double?)GetValue(Base_LabelHeightProperty); }
			set { SetValue(Base_LabelHeightProperty, value); }
		}

		#endregion Base_LabelHeight

		#region Base_LabelMargin

		public static readonly DependencyProperty Base_LabelMarginProperty = DependencyProperty.Register(
			"Base_LabelMargin",
			typeof(Thickness?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelMargin(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_LabelMarginProperty, value);
		}

		public static Thickness? GetBase_LabelMargin(CustomRadialMenuItem element) {
			return (Thickness?)element.GetValue(Base_LabelMarginProperty);
		}

		public Thickness? Base_LabelMargin {
			get { return (Thickness?)GetValue(Base_LabelMarginProperty); }
			set { SetValue(Base_LabelMarginProperty, value); }
		}

		#endregion Base_LabelMargin

		#region Base_LabelPadding

		public static readonly DependencyProperty Base_LabelPaddingProperty = DependencyProperty.Register(
			"Base_LabelPadding",
			typeof(Thickness?),
			typeof(CustomRadialMenuItem)
		);

		public static void SetBase_LabelPadding(CustomRadialMenuItem element, Brush value) {

			element.SetValue(Base_LabelPaddingProperty, value);
		}

		public static Thickness? GetBase_LabelPadding(CustomRadialMenuItem element) {
			return (Thickness?)element.GetValue(Base_LabelPaddingProperty);
		}

		public Thickness? Base_LabelContentPadding {
			get { return (Thickness?)GetValue(Base_LabelPaddingProperty); }
			set { SetValue(Base_LabelPaddingProperty, value); }
		}

		public Thickness? Base_LabelPadding {
			get { return (Thickness?)GetValue(Base_LabelPaddingProperty); }
			set { SetValue(Base_LabelPaddingProperty, value); }
		}

		#endregion Base_LabelPadding
	}
}
