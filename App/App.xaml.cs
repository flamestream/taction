using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Drawing;
using System.IO;
using System.Windows;

namespace ArtTouchPanel {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		private TaskbarIcon notificationIcon;

		protected override void OnStartup(StartupEventArgs e) {

			base.OnStartup(e);

			var rd = new ResourceDictionary();
			rd.Source = new Uri(@"pack://application:,,,/NotificationIcon.xaml");
			var r = Resources["MainIcon"];

			notificationIcon = (TaskbarIcon)rd["Definition"];
			Icon i = ArtTouchPanel.Properties.Resources.Icon;
			notificationIcon.Icon = i;
		}
	}
}
