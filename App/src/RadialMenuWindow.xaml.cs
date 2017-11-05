using RadialMenu.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Taction.UIElement;
using System.ComponentModel;
using System.Windows.Media;

namespace Taction {

	public partial class RadialMenuWindow : Window {

		public RadialMenuWindow() {

			InitializeComponent();
			DataContext = this;

			// Sync window visibility with radial menu's
			RadialMenu.IsVisibleChanged += (s, e) => SetVisibility((bool)e.NewValue);

			// Style center
			ApplyDefaultStyle();
		}

		public RadialMenuWindow(RadialMenuSpecs specs) : this() {

			// Generate layout
			var items = new List<RadialMenuItem>();
			foreach (var itemSpecs in specs.Items)
				items.Add(new CustomRadialMenuItem(itemSpecs, specs.DefaultItemStyle));

			RadialMenu.Items = items;

			// Style center
			SpreadBaseStyle();

			// Initial visibility of the radial menu
			RadialMenu.HalfShiftedItems = specs.HalfShiftedItems;
			RadialMenu.IsOpen = App.Instance.Config.Layout.DisableRadialMenuAnimation;
		}

		/// <summary>
		/// Prevent this window from being focused.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnActivated(EventArgs e) {

			base.OnActivated(e);
			WinApi.CancelActivation(this);
		}

		/// <summary>
		/// Right-click or middle-click closes window.
		/// For force-close in case the layout borks.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreviewMouseDown(MouseButtonEventArgs e) {

			base.OnPreviewMouseDown(e);
			if (e.RightButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed) {

				SetVisibility(false, false);
				e.Handled = true;
			}
		}

		/// <summary>
		/// Hide window instead of closing.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e) {

			base.OnClosing(e);
			e.Cancel = true;

			// Cancel is ignored on shutdown.
			// This prevents crash on attempting to change visibility of actually closing window.
			if (App.Instance.ShutdownMode == ShutdownMode.OnExplicitShutdown)
				return;

			SetVisibility(false);
		}

		/// <summary>
		/// Sets visibility if the window.
		/// Click-through is tied to visibility so the user can immediately interact with
		/// the underlying application while there is a closing animation, if any.
		/// </summary>
		/// <param name="isWanted"></param>
		/// <param name="isSkipAnimationWanted"></param>
		public void SetVisibility(bool isWanted, bool isSkipAnimationWanted = true) {

			WinApi.SetWindowExTransparent(this, !isWanted);

			var isAnimationDisabled = App.Instance.Config.Layout.DisableRadialMenuAnimation;

			if (!isAnimationDisabled)
				RadialMenu.IsOpen = isWanted;

			if (!isAnimationDisabled && !isSkipAnimationWanted)
				return;

			Visibility = isWanted ? Visibility.Visible : Visibility.Collapsed;
		}

		public ICommand CloseCommand {
			get {
				return new RelayCommand(() => SetVisibility(false, false));
			}
		}

		public void ApplyDefaultStyle() {

			// Base style
			var Default_CenterContent = TryFindResource("Default_CenterContent");
			if (Default_CenterContent != null)
				Base_CenterContent = Default_CenterContent;

			var Default_CenterBackground = TryFindResource("Default_CenterBackground") as Brush;
			if (Default_CenterBackground != null)
				Base_CenterBackground = Default_CenterBackground;

			var Default_CenterForeground = TryFindResource("Default_CenterForeground") as Brush;
			if (Default_CenterForeground != null)
				Base_CenterForeground = Default_CenterForeground;

			var Default_CenterFontFamily = TryFindResource("Default_CenterFontFamily") as FontFamily;
			if (Default_CenterFontFamily != null)
				Base_CenterFontFamily = Default_CenterFontFamily;

			var Default_CenterFontSize = TryFindResource("Default_CenterFontSize") as double?;
			if (Default_CenterFontSize != null)
				Base_CenterFontSize = Default_CenterFontSize;

			var Default_CenterFontWeight = TryFindResource("Default_CenterFontWeight") as FontWeight?;
			if (Default_CenterFontWeight != null)
				Base_CenterFontWeight = Default_CenterFontWeight;

			var Default_CenterHeight = TryFindResource("Default_CenterHeight") as double?;
			if (Default_CenterHeight != null)
				Base_CenterHeight = Default_CenterHeight;

			var Default_CenterWidth = TryFindResource("Default_CenterWidth") as double?;
			if (Default_CenterWidth != null)
				Base_CenterWidth = Default_CenterWidth;

			var Default_CenterBorderBrush = TryFindResource("Default_CenterBorderBrush") as Brush;
			if (Default_CenterBorderBrush != null)
				Base_CenterBorderBrush = Default_CenterBorderBrush;

			var Default_CenterBorderThickness = TryFindResource("Default_CenterBorderThickness") as double?;
			if (Default_CenterBorderThickness != null)
				Base_CenterBorderThickness = Default_CenterBorderThickness;

			// Active style
			var Default_Active_CenterContent = TryFindResource("Default_Active_CenterContent");
			if (Default_Active_CenterContent != null)
				Active_CenterContent = Default_Active_CenterContent;

			var Default_Active_CenterBackground = TryFindResource("Default_Active_CenterBackground") as Brush;
			if (Default_Active_CenterBackground != null)
				Active_CenterBackground = Default_Active_CenterBackground;

			var Default_Active_CenterForeground = TryFindResource("Default_Active_CenterForeground") as Brush;
			if (Default_Active_CenterForeground != null)
				Active_CenterForeground = Default_Active_CenterForeground;

			var Default_Active_CenterFontFamily = TryFindResource("Default_Active_CenterFontFamily") as FontFamily;
			if (Default_Active_CenterFontFamily != null)
				Active_CenterFontFamily = Default_Active_CenterFontFamily;

			var Default_Active_CenterFontSize = TryFindResource("Default_Active_CenterFontSize") as double?;
			if (Default_Active_CenterFontSize != null)
				Active_CenterFontSize = Default_Active_CenterFontSize;

			var Default_Active_CenterFontWeight = TryFindResource("Default_Active_CenterFontWeight") as FontWeight?;
			if (Default_Active_CenterFontWeight != null)
				Active_CenterFontWeight = Default_Active_CenterFontWeight;

			var Default_Active_CenterHeight = TryFindResource("Default_Active_CenterHeight") as double?;
			if (Default_Active_CenterHeight != null)
				Active_CenterHeight = Default_Active_CenterHeight;

			var Default_Active_CenterWidth = TryFindResource("Default_Active_CenterWidth") as double?;
			if (Default_Active_CenterWidth != null)
				Active_CenterWidth = Default_Active_CenterWidth;

			var Default_Active_CenterBorderBrush = TryFindResource("Default_Active_CenterBorderBrush") as Brush;
			if (Default_Active_CenterBorderBrush != null)
				Active_CenterBorderBrush = Default_Active_CenterBorderBrush;

			var Default_Active_CenterBorderThickness = TryFindResource("Default_Active_CenterBorderThickness") as double?;
			if (Default_Active_CenterBorderThickness != null)
				Active_CenterBorderThickness = Default_Active_CenterBorderThickness;
		}

		public void SpreadBaseStyle() {

			if (Active_CenterContent == null)
				Active_CenterContent = Base_CenterContent;
			if (Active_CenterBackground == null)
				Active_CenterBackground = Base_CenterBackground;
			if (Active_CenterForeground == null)
				Active_CenterForeground = Base_CenterForeground;
			if (Active_CenterFontSize == null)
				Active_CenterFontSize = Base_CenterFontSize;
			if (Active_CenterFontFamily == null)
				Active_CenterFontFamily = Base_CenterFontFamily;
			if (Active_CenterFontWeight == null)
				Active_CenterFontWeight = Base_CenterFontWeight;
			if (Active_CenterHeight == null)
				Active_CenterHeight = Base_CenterHeight;
			if (Active_CenterWidth == null)
				Active_CenterWidth = Base_CenterWidth;
			if (Active_CenterBorderBrush == null)
				Active_CenterBorderBrush = Base_CenterBorderBrush;
			if (Active_CenterBorderThickness == null)
				Active_CenterBorderThickness = Base_CenterBorderThickness;
		}

		#region Base_CenterContent

		public static readonly DependencyProperty Base_CenterContentProperty = DependencyProperty.Register(
			"Base_CenterContent",
			typeof(object),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterContent(RadialMenu.Controls.RadialMenu element, object value) {

			element.SetValue(Base_CenterContentProperty, value);
		}

		public static object GetBase_CenterContent(RadialMenu.Controls.RadialMenu element) {
			return (object)element.GetValue(Base_CenterContentProperty);
		}

		public object Base_CenterContent {
			get { return (object)GetValue(Base_CenterContentProperty); }
			set { SetValue(Base_CenterContentProperty, value); }
		}

		#endregion Base_CenterContent

		#region Base_CenterWidth

		public static readonly DependencyProperty Base_CenterWidthProperty = DependencyProperty.Register(
			"Base_CenterWidth",
			typeof(double?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterWidth(RadialMenu.Controls.RadialMenu element, double? value) {

			element.SetValue(Base_CenterWidthProperty, value);
		}

		public static double? GetBase_CenterWidth(RadialMenu.Controls.RadialMenu element) {
			return (double?)element.GetValue(Base_CenterWidthProperty);
		}

		public double? Base_CenterWidth {
			get { return (double?)GetValue(Base_CenterWidthProperty); }
			set { SetValue(Base_CenterWidthProperty, value); }
		}

		#endregion Base_CenterWidth

		#region Base_CenterHeight

		public static readonly DependencyProperty Base_CenterHeightProperty = DependencyProperty.Register(
			"Base_CenterHeight",
			typeof(double?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterHeight(RadialMenu.Controls.RadialMenu element, double? value) {

			element.SetValue(Base_CenterHeightProperty, value);
		}

		public static double? GetBase_CenterHeight(RadialMenu.Controls.RadialMenu element) {
			return (double?)element.GetValue(Base_CenterHeightProperty);
		}

		public double? Base_CenterHeight {
			get { return (double?)GetValue(Base_CenterHeightProperty); }
			set { SetValue(Base_CenterHeightProperty, value); }
		}

		#endregion Base_CenterHeight

		#region Base_CenterBackground

		public static readonly DependencyProperty Base_CenterBackgroundProperty = DependencyProperty.Register(
			"Base_CenterBackground",
			typeof(Brush),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterBackground(RadialMenu.Controls.RadialMenu element, Brush value) {

			element.SetValue(Base_CenterBackgroundProperty, value);
		}

		public static Brush GetBase_CenterBackground(RadialMenu.Controls.RadialMenu element) {
			return (Brush)element.GetValue(Base_CenterBackgroundProperty);
		}

		public Brush Base_CenterBackground {
			get { return (Brush)GetValue(Base_CenterBackgroundProperty); }
			set { SetValue(Base_CenterBackgroundProperty, value); }
		}

		#endregion Base_CenterBackground

		#region Base_CenterForeground

		public static readonly DependencyProperty Base_CenterForegroundProperty = DependencyProperty.Register(
			"Base_CenterForeground",
			typeof(Brush),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterForeground(RadialMenu.Controls.RadialMenu element, Brush value) {

			element.SetValue(Base_CenterForegroundProperty, value);
		}

		public static Brush GetBase_CenterForeground(RadialMenu.Controls.RadialMenu element) {
			return (Brush)element.GetValue(Base_CenterForegroundProperty);
		}

		public Brush Base_CenterForeground {
			get { return (Brush)GetValue(Base_CenterForegroundProperty); }
			set { SetValue(Base_CenterForegroundProperty, value); }
		}

		#endregion Base_CenterForeground

		#region Base_CenterBorderBrush

		public static readonly DependencyProperty Base_CenterBorderBrushProperty = DependencyProperty.Register(
			"Base_CenterBorderBrush",
			typeof(Brush),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterBorderBrush(RadialMenu.Controls.RadialMenu element, Brush value) {

			element.SetValue(Base_CenterBorderBrushProperty, value);
		}

		public static Brush GetBase_CenterBorderBrush(RadialMenu.Controls.RadialMenu element) {
			return (Brush)element.GetValue(Base_CenterBorderBrushProperty);
		}

		public Brush Base_CenterBorderBrush {
			get { return (Brush)GetValue(Base_CenterBorderBrushProperty); }
			set { SetValue(Base_CenterBorderBrushProperty, value); }
		}

		#endregion Base_CenterBorderBrush

		#region Base_CenterBorderThickness

		public static readonly DependencyProperty Base_CenterBorderThicknessProperty = DependencyProperty.Register(
			"Base_CenterBorderThickness",
			typeof(double?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterBorderThickness(RadialMenu.Controls.RadialMenu element, double? value) {

			element.SetValue(Base_CenterBorderThicknessProperty, value);
		}

		public static double? GetBase_CenterBorderThickness(RadialMenu.Controls.RadialMenu element) {
			return (double?)element.GetValue(Base_CenterBorderThicknessProperty);
		}

		public double? Base_CenterBorderThickness {
			get { return (double?)GetValue(Base_CenterBorderThicknessProperty); }
			set { SetValue(Base_CenterBorderThicknessProperty, value); }
		}

		#endregion Base_CenterBorderThickness

		#region Base_CenterFontFamily

		public static readonly DependencyProperty Base_CenterFontFamilyProperty = DependencyProperty.Register(
			"Base_CenterFontFamily",
			typeof(FontFamily),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterFontFamily(RadialMenu.Controls.RadialMenu element, FontFamily value) {

			element.SetValue(Base_CenterFontFamilyProperty, value);
		}

		public static FontFamily GetBase_CenterFontFamily(RadialMenu.Controls.RadialMenu element) {
			return (FontFamily)element.GetValue(Base_CenterFontFamilyProperty);
		}

		public FontFamily Base_CenterFontFamily {
			get { return (FontFamily)GetValue(Base_CenterFontFamilyProperty); }
			set { SetValue(Base_CenterFontFamilyProperty, value); }
		}

		#endregion Base_CenterFontFamily

		#region Base_CenterFontSize

		public static readonly DependencyProperty Base_CenterFontSizeProperty = DependencyProperty.Register(
			"Base_CenterFontSize",
			typeof(double?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterFontSize(RadialMenu.Controls.RadialMenu element, double? value) {

			element.SetValue(Base_CenterFontSizeProperty, value);
		}

		public static double? GetBase_CenterFontSize(RadialMenu.Controls.RadialMenu element) {
			return (double?)element.GetValue(Base_CenterFontSizeProperty);
		}

		public double? Base_CenterFontSize {
			get { return (double?)GetValue(Base_CenterFontSizeProperty); }
			set { SetValue(Base_CenterFontSizeProperty, value); }
		}

		#endregion Base_CenterFontSize

		#region Base_CenterFontWeight

		public static readonly DependencyProperty Base_CenterFontWeightProperty = DependencyProperty.Register(
			"Base_CenterFontWeight",
			typeof(FontWeight?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetBase_CenterFontWeight(RadialMenu.Controls.RadialMenu element, FontWeight? value) {

			element.SetValue(Base_CenterFontWeightProperty, value);
		}

		public static FontWeight? GetBase_CenterFontWeight(RadialMenu.Controls.RadialMenu element) {
			return (FontWeight?)element.GetValue(Base_CenterFontWeightProperty);
		}

		public FontWeight? Base_CenterFontWeight {
			get { return (FontWeight?)GetValue(Base_CenterFontWeightProperty); }
			set { SetValue(Base_CenterFontWeightProperty, value); }
		}

		#endregion Base_CenterFontWeight

		#region Active_CenterContent

		public static readonly DependencyProperty Active_CenterContentProperty = DependencyProperty.Register(
			"Active_CenterContent",
			typeof(object),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterContent(RadialMenu.Controls.RadialMenu element, object value) {

			element.SetValue(Active_CenterContentProperty, value);
		}

		public static object GetActive_CenterContent(RadialMenu.Controls.RadialMenu element) {
			return (object)element.GetValue(Active_CenterContentProperty);
		}

		public object Active_CenterContent {
			get { return (object)GetValue(Active_CenterContentProperty); }
			set { SetValue(Active_CenterContentProperty, value); }
		}

		#endregion Active_CenterContent

		#region Active_CenterWidth

		public static readonly DependencyProperty Active_CenterWidthProperty = DependencyProperty.Register(
			"Active_CenterWidth",
			typeof(double?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterWidth(RadialMenu.Controls.RadialMenu element, double? value) {

			element.SetValue(Active_CenterWidthProperty, value);
		}

		public static double? GetActive_CenterWidth(RadialMenu.Controls.RadialMenu element) {
			return (double?)element.GetValue(Active_CenterWidthProperty);
		}

		public double? Active_CenterWidth {
			get { return (double?)GetValue(Active_CenterWidthProperty); }
			set { SetValue(Active_CenterWidthProperty, value); }
		}

		#endregion Active_CenterWidth

		#region Active_CenterHeight

		public static readonly DependencyProperty Active_CenterHeightProperty = DependencyProperty.Register(
			"Active_CenterHeight",
			typeof(double?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterHeight(RadialMenu.Controls.RadialMenu element, double? value) {

			element.SetValue(Active_CenterHeightProperty, value);
		}

		public static double? GetActive_CenterHeight(RadialMenu.Controls.RadialMenu element) {
			return (double?)element.GetValue(Active_CenterHeightProperty);
		}

		public double? Active_CenterHeight {
			get { return (double?)GetValue(Active_CenterHeightProperty); }
			set { SetValue(Active_CenterHeightProperty, value); }
		}

		#endregion Active_CenterHeight

		#region Active_CenterBackground

		public static readonly DependencyProperty Active_CenterBackgroundProperty = DependencyProperty.Register(
			"Active_CenterBackground",
			typeof(Brush),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterBackground(RadialMenu.Controls.RadialMenu element, Brush value) {

			element.SetValue(Active_CenterBackgroundProperty, value);
		}

		public static Brush GetActive_CenterBackground(RadialMenu.Controls.RadialMenu element) {
			return (Brush)element.GetValue(Active_CenterBackgroundProperty);
		}

		public Brush Active_CenterBackground {
			get { return (Brush)GetValue(Active_CenterBackgroundProperty); }
			set { SetValue(Active_CenterBackgroundProperty, value); }
		}

		#endregion Active_CenterBackground

		#region Active_CenterForeground

		public static readonly DependencyProperty Active_CenterForegroundProperty = DependencyProperty.Register(
			"Active_CenterForeground",
			typeof(Brush),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterForeground(RadialMenu.Controls.RadialMenu element, Brush value) {

			element.SetValue(Active_CenterForegroundProperty, value);
		}

		public static Brush GetActive_CenterForeground(RadialMenu.Controls.RadialMenu element) {
			return (Brush)element.GetValue(Active_CenterForegroundProperty);
		}

		public Brush Active_CenterForeground {
			get { return (Brush)GetValue(Active_CenterForegroundProperty); }
			set { SetValue(Active_CenterForegroundProperty, value); }
		}

		#endregion Active_CenterForeground

		#region Active_CenterBorderBrush

		public static readonly DependencyProperty Active_CenterBorderBrushProperty = DependencyProperty.Register(
			"Active_CenterBorderBrush",
			typeof(Brush),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterBorderBrush(RadialMenu.Controls.RadialMenu element, Brush value) {

			element.SetValue(Active_CenterBorderBrushProperty, value);
		}

		public static Brush GetActive_CenterBorderBrush(RadialMenu.Controls.RadialMenu element) {
			return (Brush)element.GetValue(Active_CenterBorderBrushProperty);
		}

		public Brush Active_CenterBorderBrush {
			get { return (Brush)GetValue(Active_CenterBorderBrushProperty); }
			set { SetValue(Active_CenterBorderBrushProperty, value); }
		}

		#endregion Active_CenterBorderBrush

		#region Active_CenterBorderThickness

		public static readonly DependencyProperty Active_CenterBorderThicknessProperty = DependencyProperty.Register(
			"Active_CenterBorderThickness",
			typeof(double?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterBorderThickness(RadialMenu.Controls.RadialMenu element, double? value) {

			element.SetValue(Active_CenterBorderThicknessProperty, value);
		}

		public static double? GetActive_CenterBorderThickness(RadialMenu.Controls.RadialMenu element) {
			return (double?)element.GetValue(Active_CenterBorderThicknessProperty);
		}

		public double? Active_CenterBorderThickness {
			get { return (double?)GetValue(Active_CenterBorderThicknessProperty); }
			set { SetValue(Active_CenterBorderThicknessProperty, value); }
		}

		#endregion Active_CenterBorderThickness

		#region Active_CenterFontFamily

		public static readonly DependencyProperty Active_CenterFontFamilyProperty = DependencyProperty.Register(
			"Active_CenterFontFamily",
			typeof(FontFamily),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterFontFamily(RadialMenu.Controls.RadialMenu element, FontFamily value) {

			element.SetValue(Active_CenterFontFamilyProperty, value);
		}

		public static FontFamily GetActive_CenterFontFamily(RadialMenu.Controls.RadialMenu element) {
			return (FontFamily)element.GetValue(Active_CenterFontFamilyProperty);
		}

		public FontFamily Active_CenterFontFamily {
			get { return (FontFamily)GetValue(Active_CenterFontFamilyProperty); }
			set { SetValue(Active_CenterFontFamilyProperty, value); }
		}

		#endregion Active_CenterFontFamily

		#region Active_CenterFontSize

		public static readonly DependencyProperty Active_CenterFontSizeProperty = DependencyProperty.Register(
			"Active_CenterFontSize",
			typeof(double?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterFontSize(RadialMenu.Controls.RadialMenu element, double? value) {

			element.SetValue(Active_CenterFontSizeProperty, value);
		}

		public static double? GetActive_CenterFontSize(RadialMenu.Controls.RadialMenu element) {
			return (double?)element.GetValue(Active_CenterFontSizeProperty);
		}

		public double? Active_CenterFontSize {
			get { return (double?)GetValue(Active_CenterFontSizeProperty); }
			set { SetValue(Active_CenterFontSizeProperty, value); }
		}

		#endregion Active_CenterFontSize

		#region Active_CenterFontWeight

		public static readonly DependencyProperty Active_CenterFontWeightProperty = DependencyProperty.Register(
			"Active_CenterFontWeight",
			typeof(FontWeight?),
			typeof(RadialMenu.Controls.RadialMenu)
		);

		public static void SetActive_CenterFontWeight(RadialMenu.Controls.RadialMenu element, FontWeight? value) {

			element.SetValue(Active_CenterFontWeightProperty, value);
		}

		public static FontWeight? GetActive_CenterFontWeight(RadialMenu.Controls.RadialMenu element) {
			return (FontWeight?)element.GetValue(Active_CenterFontWeightProperty);
		}

		public FontWeight? Active_CenterFontWeight {
			get { return (FontWeight?)GetValue(Active_CenterFontWeightProperty); }
			set { SetValue(Active_CenterFontWeightProperty, value); }
		}

		#endregion Active_CenterFontWeight
	}
}
