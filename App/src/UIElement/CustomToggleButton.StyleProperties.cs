using System;
using System.Windows;
using System.Windows.Media;

namespace Taction.UIElement {

	partial class CustomToggleButton : ICustomStylizable {

		#region CornerRadius

		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
			"CornerRadius",
			typeof(CornerRadius),
			typeof(CustomToggleButton)
		);

		public static void SetCornerRadius(CustomButton element, CornerRadius value) {

			element.SetValue(CornerRadiusProperty, value);
		}

		public static CornerRadius GetCornerRadius(CustomButton element) {
			return (CornerRadius)element.GetValue(CornerRadiusProperty);
		}

		public CornerRadius CornerRadius {
			get { return (CornerRadius)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		#endregion CornerRadius

		#region Active_Background

		public static readonly DependencyProperty Active_BackgroundProperty = DependencyProperty.Register(
			"Active_Background",
			typeof(Brush),
			typeof(CustomToggleButton)
		);

		public static void SetActive_Background(CustomToggleButton element, Brush value) {

			element.SetValue(Active_BackgroundProperty, value);
		}

		public static Brush GetActive_Background(CustomToggleButton element) {
			return (Brush)element.GetValue(Active_BackgroundProperty);
		}

		public Brush Active_Background {
			get { return (Brush)GetValue(Active_BackgroundProperty); }
			set { SetValue(Active_BackgroundProperty, value); }
		}

		#endregion Active_Background

		#region Active_BorderBrush

		public static readonly DependencyProperty Active_BorderBrushProperty = DependencyProperty.Register(
			"Active_BorderBrush",
			typeof(Brush),
			typeof(CustomToggleButton)
		);

		public static void SetActive_BorderBrush(CustomToggleButton element, Brush value) {

			element.SetValue(Active_BorderBrushProperty, value);
		}

		public static Brush GetActive_BorderBrush(CustomToggleButton element) {
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
			typeof(CustomToggleButton)
		);

		public static void SetActive_BorderThickness(CustomToggleButton element, Thickness value) {

			element.SetValue(Active_BorderThicknessProperty, value);
		}

		public static Thickness GetActive_BorderThickness(CustomToggleButton element) {
			return (Thickness)element.GetValue(Active_BorderThicknessProperty);
		}

		public Thickness Active_BorderThickness {
			get { return (Thickness)GetValue(Active_BorderThicknessProperty); }
			set { SetValue(Active_BorderThicknessProperty, value); }
		}

		#endregion Active_BorderThickness

		#region Active_Content

		public static readonly DependencyProperty Active_ContentProperty = DependencyProperty.Register(
			"Active_Content",
			typeof(Object),
			typeof(CustomToggleButton)
		);

		public static void SetActive_Content(CustomToggleButton element, Object value) {

			element.SetValue(Active_ContentProperty, value);
		}

		public static Object GetActive_Content(CustomToggleButton element) {
			return (Object)element.GetValue(Active_ContentProperty);
		}

		public Object Active_Content {
			get { return (Object)GetValue(Active_ContentProperty); }
			set { SetValue(Active_ContentProperty, value); }
		}

		#endregion Active_Content

		#region Active_CornerRadius

		public static readonly DependencyProperty Active_CornerRadiusProperty = DependencyProperty.Register(
			"Active_CornerRadius",
			typeof(CornerRadius),
			typeof(CustomToggleButton)
		);

		public static void SetActive_CornerRadius(CustomButton element, CornerRadius value) {

			element.SetValue(Active_CornerRadiusProperty, value);
		}

		public static CornerRadius GetActive_CornerRadius(CustomButton element) {
			return (CornerRadius)element.GetValue(Active_CornerRadiusProperty);
		}

		public CornerRadius Active_CornerRadius {
			get { return (CornerRadius)GetValue(Active_CornerRadiusProperty); }
			set { SetValue(Active_CornerRadiusProperty, value); }
		}

		#endregion Active_CornerRadius

		#region Active_FontFamily

		public static readonly DependencyProperty Active_FontFamilyProperty = DependencyProperty.Register(
			"Active_FontFamily",
			typeof(FontFamily),
			typeof(CustomToggleButton)
		);

		public static void SetActive_FontFamily(CustomToggleButton element, FontFamily value) {

			element.SetValue(Active_FontFamilyProperty, value);
		}

		public static FontFamily GetActive_FontFamily(CustomToggleButton element) {
			return (FontFamily)element.GetValue(Active_FontFamilyProperty);
		}

		public FontFamily Active_FontFamily {
			get { return (FontFamily)GetValue(Active_FontFamilyProperty); }
			set { SetValue(Active_FontFamilyProperty, value); }
		}

		#endregion Active_FontFamily

		#region Active_FontSize

		public static readonly DependencyProperty Active_FontSizeProperty = DependencyProperty.Register(
			"Active_FontSize",
			typeof(double),
			typeof(CustomToggleButton)
		);

		public static void SetActive_FontSize(CustomToggleButton element, double value) {

			element.SetValue(Active_FontSizeProperty, value);
		}

		public static double GetActive_FontSize(CustomToggleButton element) {
			return (double)element.GetValue(Active_FontSizeProperty);
		}

		public double Active_FontSize {
			get { return (double)GetValue(Active_FontSizeProperty); }
			set { SetValue(Active_FontSizeProperty, value); }
		}

		#endregion Active_FontSize

		#region Active_FontWeight

		public static readonly DependencyProperty Active_FontWeightProperty = DependencyProperty.Register(
			"Active_FontWeight",
			typeof(FontWeight),
			typeof(CustomToggleButton)
		);

		public static void SetActive_FontWeight(CustomToggleButton element, FontWeight value) {

			element.SetValue(Active_FontWeightProperty, value);
		}

		public static FontWeight GetActive_FontWeight(CustomToggleButton element) {
			return (FontWeight)element.GetValue(Active_FontWeightProperty);
		}

		public FontWeight Active_FontWeight {
			get { return (FontWeight)GetValue(Active_FontWeightProperty); }
			set { SetValue(Active_FontWeightProperty, value); }
		}

		#endregion Active_FontWeight

		#region Active_Foreground

		public static readonly DependencyProperty Active_ForegroundProperty = DependencyProperty.Register(
			"Active_Foreground",
			typeof(Brush),
			typeof(CustomToggleButton)
		);

		public static void SetActive_Foreground(CustomToggleButton element, Brush value) {

			element.SetValue(Active_ForegroundProperty, value);
		}

		public static Brush GetActive_Foreground(CustomToggleButton element) {
			return (Brush)element.GetValue(Active_ForegroundProperty);
		}

		public Brush Active_Foreground {
			get { return (Brush)GetValue(Active_ForegroundProperty); }
			set { SetValue(Active_ForegroundProperty, value); }
		}

		#endregion Active_Foreground

		#region Active_Size

		public static readonly DependencyProperty Active_SizeProperty = DependencyProperty.Register(
			"Active_Size",
			typeof(double),
			typeof(CustomToggleButton)
		);

		public static void SetActive_Size(CustomToggleButton element, Brush value) {

			element.SetValue(Active_SizeProperty, value);
		}

		public static double GetActive_Size(CustomToggleButton element) {
			return (double)element.GetValue(Active_SizeProperty);
		}

		public double Active_Size {
			get { return (double)GetValue(Active_SizeProperty); }
			set { SetValue(Active_SizeProperty, value); }
		}

		#endregion Active_Size

		#region Active_Margin

		public static readonly DependencyProperty Active_MarginProperty = DependencyProperty.Register(
			"Active_Margin",
			typeof(Thickness),
			typeof(CustomToggleButton)
		);

		public static void SetActive_Margin(CustomToggleButton element, Brush value) {

			element.SetValue(Active_MarginProperty, value);
		}

		public static Thickness GetActive_Margin(CustomToggleButton element) {
			return (Thickness)element.GetValue(Active_MarginProperty);
		}

		public Thickness Active_Margin {
			get { return (Thickness)GetValue(Active_MarginProperty); }
			set { SetValue(Active_MarginProperty, value); }
		}

		#endregion Active_Margin

		#region Active_Padding

		public static readonly DependencyProperty Active_PaddingProperty = DependencyProperty.Register(
			"Active_Padding",
			typeof(Thickness),
			typeof(CustomToggleButton)
		);

		public static void SetActive_Padding(CustomToggleButton element, Brush value) {

			element.SetValue(Active_PaddingProperty, value);
		}

		public static Thickness GetActive_Padding(CustomToggleButton element) {
			return (Thickness)element.GetValue(Active_PaddingProperty);
		}

		public Thickness Active_ContentPadding {
			get { return (Thickness)GetValue(Active_PaddingProperty); }
			set { SetValue(Active_PaddingProperty, value); }
		}

		public Thickness Active_Padding {
			get { return (Thickness)GetValue(Active_MarginProperty); }
			set { SetValue(Active_MarginProperty, value); }
		}

		#endregion Active_Padding
	}
}
