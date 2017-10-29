using RadialMenu.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

			// Set initial state (Nullify designer state)
			RadialMenu.IsOpen = false;
			RadialMenu.IsVisibleChanged += (s, e) => SetVisibility((bool)e.NewValue);
		}

		public RadialMenuWindow(RadialMenuSpecs specs) : this() {

			var items = new List<RadialMenuItem>();

			foreach (var itemSpecs in specs.Items)
				items.Add(new CustomRadialMenuItem(itemSpecs, specs.DefaultItemStyle));

			RadialMenu.HalfShiftedItems = specs.HalfShiftedItems;
			RadialMenu.Items = items;
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

		public void SetVisibility(bool isWanted, bool isSkipAnimationWanted = true) {

			WinApi.SetWindowExTransparent(this, !isWanted);
			RadialMenu.IsOpen = isWanted;

			if (!isSkipAnimationWanted)
				return;

			Visibility = isWanted ? Visibility.Visible : Visibility.Collapsed;
		}

		public ICommand CloseCommand {
			get {
				return new RelayCommand(() => RadialMenu.IsOpen = false);
			}
		}
	}
}
