using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.IO;
using System.Windows;

namespace Taction {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		internal TaskbarIcon notificationIcon { get; private set; }
		internal Config config { get; private set; }

		/// <summary>
		/// Cached app data directory
		/// </summary>
		public static string AppDataDir {
			get {
				if (_AppDataDir == null) {

					_AppDataDir = string.Format(@"{0}\{1}",
						Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
						Taction.Properties.Resources.AppName
					);
				}

				return _AppDataDir;
			}
		}

		/// <summary>
		/// Cached error file path of the config file.
		/// </summary>
		public static string ErrorFilePath {
			get {
				if (_ErrorFilePath == null) {

					_ErrorFilePath = string.Format(@"{0}\{1}",
						App.AppDataDir,
						Taction.Properties.Resources.ConfigErrorFileName
					);
				}

				return _ErrorFilePath;
			}
		}

		private static string _AppDataDir;
		private static string _ErrorFilePath;

		protected override void OnStartup(StartupEventArgs e) {

			base.OnStartup(e);

			// Create app data
			Directory.CreateDirectory(AppDataDir);

			// Setup Notification icon
			var res = new ResourceDictionary();
			res.Source = new Uri(@"pack://application:,,,/NotificationIcon.xaml");
			// @TODO There must be a way to do this in xaml
			notificationIcon = (TaskbarIcon)res["Definition"];
			notificationIcon.Icon = Taction.Properties.Resources.Icon;

			// Load config
			config = new Config();
			string errTitle = Taction.Properties.Resources.DefaultNotificationBubbleErrorTitle;
			string errMsg = null;
			try {
				config.Load();
			} catch (FileNotFoundException err) {
				errTitle = "Config load error";
				errMsg = err.Message;
			} catch (FormatException err) {
				errTitle = "Config validation error";
				errMsg = err.Message;
			} catch (Newtonsoft.Json.JsonReaderException err) {
				errTitle = "Config syntax error";
				errMsg = err.Message;
			} catch (Exception err) {
				errMsg = string.Format(err.Message);
			}

			if (errMsg != null) {

				// Display alert
				notificationIcon.ShowBalloonTip(
					errTitle,
					Taction.Properties.Resources.DefaultNotificationBubbleErrorMessage,
					BalloonIcon.Error
				);

				using (var file = new StreamWriter(ErrorFilePath, true)) {

					file.WriteLine(string.Format(@"[{0}]", DateTime.UtcNow));
					file.WriteLine(errMsg);
					file.Close();
				}
			}
		}
	}
}
