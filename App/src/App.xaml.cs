using System;
using System.IO;
using System.Windows;

namespace Taction {

	public partial class App : Application {

		internal MainPanel MainPanel => MainWindow as MainPanel;
		internal Config Config { get; private set; }
		internal GlobalMouseHook GlobalMouseHook { get; private set; }
		internal InputSimulatorHelper InputSimulator { get; private set; }

		private ErrorLogger ErrorLogger { get; set; }
		private NotificationIcon NotificationIcon { get; set; }
		private UpdateChecker UpdateChecker { get; set; }

		protected override void OnStartup(StartupEventArgs ev) {

			base.OnStartup(ev);

			// Only exit application when on explicit endpoint
			Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

			// Ensure app data directory
			Directory.CreateDirectory(AppDataDir);
			Directory.CreateDirectory(FontDir);

			// Initialize basic stuff
			GlobalMouseHook = new GlobalMouseHook();
			InputSimulator = new InputSimulatorHelper();
			Config = new Config();
			ErrorLogger = new ErrorLogger(ErrorFilePath, MaxErrorLogSize, ErrorLogTrimLineCount);
			UpdateChecker = new UpdateChecker(ReleaseApi);
			NotificationIcon = new NotificationIcon();

			// Setup Updater
			UpdateChecker.OnUpdateAvailable += UpdateChecker_OnUpdateAvailable;

			Config.LoadState();
			LoadSavedLayout(true);

			MainWindow = new MainPanel();
			MainWindow.Show();

			CheckForUpdates();
		}

		protected override void OnExit(ExitEventArgs e) {

			InputSimulator.ReleaseAllKeys();
			GlobalMouseHook.Dispose();
			NotificationIcon.Dispose();
		}

		private void UpdateChecker_OnUpdateAvailable(object sender, UpdateChecker.ReleaseInfoResponse releaseInfo) {

			// Update notification menu
			NotificationIcon.CheckUpdateMenuItem.Header = string.Format("New release available ({0})", releaseInfo.TagName);
			//NotificationIcon.CheckUpdateMenuItem.FontWeight = FontWeights.Bold;

			// Version skip check
			if (Config.State.SkipReleaseVersion == releaseInfo.TagName)
				return;

			// Nag limit check
			var lastCheck = Config.State.LastUpdateNagDate;
			if (lastCheck != default(DateTime)) {

				var nextCheck = lastCheck.AddMilliseconds(UpdateNagWaitTime);
				if (nextCheck > DateTime.Now)
					return;
			}

			Config.State.LastUpdateNagDate = DateTime.Now;
			Config.Save();

			// Show update window
			var window = new NewReleaseWindow() {
				ReleaseVersion = releaseInfo.TagName,
				HtmlUrl = releaseInfo.HtmlUrl,
			};

			window.Owner = MainWindow;
			window.ShowDialog();
		}
	}
}
