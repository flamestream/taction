using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;

namespace Taction {

	partial class NotificationIcon {

		private App app => (App)Application.Current;

		private void ExitMenuItem_Click(object sender, System.Windows.RoutedEventArgs e) {

			Application.Current.Shutdown(0);
		}

		private void ResetPositionMenuItem_Click(object sender, RoutedEventArgs e) {

			this.app.MoveTo(0, 0);
		}

		private void LoadLayoutMenuItem_Click(object sender, RoutedEventArgs e) {

			var openFileDialog = new OpenFileDialog {
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
				Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
			};

			if (openFileDialog.ShowDialog(Application.Current.MainWindow) != true)
				return;

			this.app.LoadLayout(openFileDialog.FileName);
		}

		private void ResetLayoutMenuItem_Click(object sender, RoutedEventArgs e) {

			var json = JObject.Parse(System.Text.Encoding.UTF8.GetString(Properties.Resources.DefaultConfigLayoutJson));
			this.app.LoadDefaultLayout();
		}
	}
}
