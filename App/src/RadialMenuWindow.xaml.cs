using RadialMenu.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Taction.UIElement;
using System.ComponentModel;

namespace Taction {

	/// <summary>
	/// Interaction logic for RadialMenuWindow.xaml
	/// </summary>
	public partial class RadialMenuWindow : Window {

		public RadialMenuWindow() {

			InitializeComponent();
			DataContext = this;

			RadialMenu.IsVisibleChanged += (s, e) => SetVisibility((bool)e.NewValue);
		}

		public RadialMenuWindow(RadialMenuSpecs specs) : this() {

			var items = new List<RadialMenuItem>();

			foreach (var itemSpecs in specs.Items)
				items.Add(new CustomRadialMenuItem(itemSpecs, specs.DefaultItemStyle));

			RadialMenu.HalfShiftedItems = specs.HalfShiftedItems;
			RadialMenu.Items = items;
			RadialMenu.IsOpen = App.Instance.Config.Layout.DisableRadialMenuAnimation;
		}

		protected override void OnActivated(EventArgs e) {

			base.OnActivated(e);
			WinApi.CancelActivation(this);
		}

		protected override void OnClosing(CancelEventArgs e) {

			base.OnClosing(e);
			e.Cancel = true;
			SetVisibility(false);
		}

		protected override void OnPreviewStylusInRange(StylusEventArgs e) {
			base.OnPreviewStylusInRange(e);
			Debug.WriteLine(string.Format("grid in range {0}", e.GetPosition(this)));
		}

		protected override void OnPreviewStylusInAirMove(StylusEventArgs e) {
			base.OnPreviewStylusInAirMove(e);
			Debug.WriteLine(string.Format("grid preview air {0}", e.GetPosition(this)));
		}

		protected override void OnStylusInAirMove(StylusEventArgs e) {
			base.OnStylusInAirMove(e);
			Debug.WriteLine(string.Format("grid air {0}", e.GetPosition(this)));
		}

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
				return new RelayCommand(() => SetVisibility(false));
			}
		}
	}
}
