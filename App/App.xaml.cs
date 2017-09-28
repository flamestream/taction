using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;

namespace Taction {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		private TaskbarIcon notificationIcon;

		protected override void OnStartup(StartupEventArgs e) {

			base.OnStartup(e);

			// Setup Notification icon
			var res = new ResourceDictionary();
			res.Source = new Uri(@"pack://application:,,,/NotificationIcon.xaml");
			// @TODO There must be a way to do this in xaml
			notificationIcon = (TaskbarIcon)res["Definition"];
			notificationIcon.Icon = Taction.Properties.Resources.Icon;
		}
	}
}
