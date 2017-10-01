using System.Windows;

namespace Taction {

	partial class NotificationIcon {

		private App App => (App)Application.Current;

		private void ExitMenuItem_Click(object sender, System.Windows.RoutedEventArgs e) {

			App.Shutdown(0);
		}

		private void ResetPositionMenuItem_Click(object sender, RoutedEventArgs e) {

			App.MoveTo(0, 0);
		}

		private void LoadLayoutMenuItem_Click(object sender, RoutedEventArgs e) {

			App.PromptLoadLayout();
		}

		private void ResetLayoutMenuItem_Click(object sender, RoutedEventArgs e) {

			App.LoadDefaultLayout(true);
		}

		private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e) {

			App.ToggleEnable();
		}
	}
}
