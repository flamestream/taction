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

			// Cancel is ignored on shutdown. This prevents crash on attempting to change visibility of actually closing window.
			if (App.Instance.ShutdownMode == ShutdownMode.OnExplicitShutdown)
				return;

			SetVisibility(false);
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
				return new RelayCommand(() => SetVisibility(false, false));
			}
		}
	}
}
