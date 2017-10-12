using System.Windows;

namespace Taction {

	partial class NotificationIcon {

		private void ExitMenuItem_Click(object sender, System.Windows.RoutedEventArgs e) {

			App.Instance.Shutdown(0);
		}

		private void ResetPositionMenuItem_Click(object sender, RoutedEventArgs e) {

			App.Instance.MoveTo(0, 0);
		}

		private void LoadLayoutMenuItem_Click(object sender, RoutedEventArgs e) {

			App.Instance.PromptLoadLayout();
		}

		private void ResetLayoutMenuItem_Click(object sender, RoutedEventArgs e) {

			App.Instance.LoadDefaultLayout(true);
		}

		private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e) {

			App.Instance.ToggleEnable();
		}
	}
}
