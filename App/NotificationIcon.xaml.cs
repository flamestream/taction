using System.Windows;

namespace Taction {

	partial class NotificationIcon {

		private void ExitMenuItem_Click(object sender, System.Windows.RoutedEventArgs e) {

			Application.Current.Shutdown(0);
		}
	}
}
