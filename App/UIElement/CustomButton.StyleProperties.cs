using System;
using System.Windows;
using System.Windows.Media;

namespace Taction.UIElement {

	partial class CustomButton {

		#region Active_Size

		public static readonly DependencyProperty Active_SizeProperty = DependencyProperty.Register(
			"Active_Size",
			typeof(double),
			typeof(CustomButton)
		);

		public static void SetActive_Size(ToggleButton element, Brush value) {

			element.SetValue(Active_SizeProperty, value);
		}

		public static double GetActive_Size(ToggleButton element) {
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
			typeof(CustomButton)
		);

		public static void SetActive_Margin(ToggleButton element, Brush value) {

			element.SetValue(Active_MarginProperty, value);
		}

		public static Thickness GetActive_Margin(ToggleButton element) {
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
			typeof(CustomButton)
		);

		public static void SetActive_Padding(ToggleButton element, Brush value) {

			element.SetValue(Active_PaddingProperty, value);
		}

		public static Thickness GetActive_Padding(ToggleButton element) {
			return (Thickness)element.GetValue(Active_PaddingProperty);
		}

		public Thickness Active_ContentPadding {
			get { return (Thickness)GetValue(Active_PaddingProperty); }
			set { SetValue(Active_PaddingProperty, value); }
		}

		#endregion Active_Padding

		#region Active_BorderBrush

		public static readonly DependencyProperty Active_BorderBrushProperty = DependencyProperty.Register(
			"Active_BorderBrush",
			typeof(Brush),
			typeof(CustomButton)
		);

		public static void SetActive_BorderBrush(ToggleButton element, Brush value) {

			element.SetValue(Active_BorderBrushProperty, value);
		}

		public static Brush GetActive_BorderBrush(ToggleButton element) {
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
			typeof(CustomButton)
		);

		public static void SetActive_BorderThickness(ToggleButton element, Thickness value) {

			element.SetValue(Active_BorderThicknessProperty, value);
		}

		public static Thickness GetActive_BorderThickness(ToggleButton element) {
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
			typeof(CustomButton)
		);

		public static void SetActive_Background(ToggleButton element, Brush value) {

			element.SetValue(Active_BackgroundProperty, value);
		}

		public static Brush GetActive_Background(ToggleButton element) {
			return (Brush)element.GetValue(Active_BackgroundProperty);
		}

		public Brush Active_Background {
			get { return (Brush)GetValue(Active_BackgroundProperty); }
			set { SetValue(Active_BackgroundProperty, value); }
		}

		#endregion Active_Background

		#region Active_Content

		public static readonly DependencyProperty Active_ContentProperty = DependencyProperty.Register(
			"Active_Content",
			typeof(Object),
			typeof(CustomButton)
		);

		public static void SetActive_Content(ToggleButton element, Object value) {

			element.SetValue(Active_ContentProperty, value);
		}

		public static Object GetActive_Content(ToggleButton element) {
			return element.GetValue(Active_ContentProperty);
		}

		public Object Active_Content {
			get { return GetValue(Active_ContentProperty); }
			set { SetValue(Active_ContentProperty, value); }
		}

		#endregion Active_Content

		#region Active_FontSize

		public static readonly DependencyProperty Active_FontSizeProperty = DependencyProperty.Register(
			"Active_FontSize",
			typeof(double),
			typeof(CustomButton)
		);

		public static void SetActive_FontSize(ToggleButton element, double value) {

			element.SetValue(Active_FontSizeProperty, value);
		}

		public static double GetActive_FontSize(ToggleButton element) {
			return (double)element.GetValue(Active_FontSizeProperty);
		}

		public double Active_FontSize {
			get { return (double)GetValue(Active_FontSizeProperty); }
			set { SetValue(Active_FontSizeProperty, value); }
		}

		#endregion Active_FontSize

		#region Active_Foreground

		public static readonly DependencyProperty Active_ForegroundProperty = DependencyProperty.Register(
			"Active_Foreground",
			typeof(Brush),
			typeof(CustomButton)
		);

		public static void SetActive_Foreground(ToggleButton element, Brush value) {

			element.SetValue(Active_ForegroundProperty, value);
		}

		public static Brush GetActive_Foreground(ToggleButton element) {
			return (Brush)element.GetValue(Active_ForegroundProperty);
		}

		public Brush Active_Foreground {
			get { return (Brush)GetValue(Active_ForegroundProperty); }
			set { SetValue(Active_ForegroundProperty, value); }
		}

		#endregion Active_Foreground

		#region Active_FontFamily

		public static readonly DependencyProperty Active_FontFamilyProperty = DependencyProperty.Register(
			"Active_FontFamily",
			typeof(FontFamily),
			typeof(CustomButton)
		);

		public static void SetActive_FontFamily(ToggleButton element, FontFamily value) {

			element.SetValue(Active_FontFamilyProperty, value);
		}

		public static FontFamily GetActive_FontFamily(ToggleButton element) {
			return (FontFamily)element.GetValue(Active_FontFamilyProperty);
		}

		public FontFamily Active_FontFamily {
			get { return (FontFamily)GetValue(Active_FontFamilyProperty); }
			set { SetValue(Active_FontFamilyProperty, value); }
		}

		#endregion Active_FontFamily
	}
}
