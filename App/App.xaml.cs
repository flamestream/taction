using Hardcodet.Wpf.TaskbarNotification;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows;

namespace Taction {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		internal Config config;
		internal MainPanel panel => (MainPanel)MainWindow;
		internal TaskbarIcon notificationIcon { get; private set; }
		internal GlobalMouseHook globalMouseHook { get; private set; }
		internal InputSimulatorHelper inputSimulator { get; set; }

		protected override void OnStartup(StartupEventArgs e) {

			base.OnStartup(e);

			// Ensure app data directory
			Directory.CreateDirectory(AppDataDir);

			// Initialize basic stuff
			globalMouseHook = new GlobalMouseHook();
			inputSimulator = new InputSimulatorHelper();
			config = new Config();

			// Setup Notification icon
			{
				var res = new ResourceDictionary {
					Source = new Uri(@"pack://application:,,,/NotificationIcon.xaml")
				};
				// @TODO There must be a way to do this in xaml
				notificationIcon = (TaskbarIcon)res["Definition"];
				notificationIcon.Icon = Taction.Properties.Resources.Icon;
			}

			// Load config
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

					file.WriteLine(string.Format(@"[{0}]", DateTime.Now));
					file.WriteLine(errMsg);
					file.Close();
				}
			}
		}

		protected override void OnExit(ExitEventArgs e) {

			globalMouseHook.Dispose();
		}

		public void MoveTo(double x, double y) {

			panel.Left = 0;
			panel.Top = 0;
			WindowManipulator.FitToNearestDesktop(panel);

			config.Save();
		}

		public void LoadDefaultLayout() {

			var encoding = System.Text.Encoding.UTF8;
			var text = encoding.GetString(Taction.Properties.Resources.DefaultConfigLayoutJson);
			var json = JObject.Parse(text);

			// Load and validate
			this.config.LoadLayout(json);

			// Update UI
			this.panel.ReloadLayout();

			// Persist for later
			File.WriteAllText(Config.FileLayoutPath, text, encoding);
		}

		public void LoadLayout(string path) {

			// Load and validate
			this.config.LoadLayout(path);

			// Update UI
			this.panel.ReloadLayout();

			// Persist for later
			File.Copy(path, Config.FileLayoutPath, true);
		}

		// -- STATIC PROPERTIES --

		private static string _AppDataDir;
		private static string _ErrorFilePath;

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
	}
}
