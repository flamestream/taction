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

			RadialMenu.IsOpen = true;
		}

		public void SetVisibility(bool isWanted) {

			WinApi.SetWindowExTransparent(this, !isWanted);
			Visibility = isWanted ? Visibility.Visible : Visibility.Hidden;
		}

		public ICommand CloseCommand {
			get {
				return new RelayCommand(() => RadialMenu.IsOpen = false);
			}
		}
	}
}
