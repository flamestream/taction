using System.Windows;

namespace Taction {

	partial class NotificationIcon {

		private void ExitMenuItem_Click(object sender, System.Windows.RoutedEventArgs e) {

			Application.Current.Shutdown(0);
		}

		private void ResetPosition_Click(object sender, RoutedEventArgs e) {

			var panel = (MainPanel)Application.Current.MainWindow;
			panel.Left = 0;
			panel.Top = 0;
		}
	}
}
