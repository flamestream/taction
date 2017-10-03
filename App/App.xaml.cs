using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using Taction.CustomUIElement;

namespace Taction {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		#region -- Application Settings/Constants --

		internal const int NotificationToastCloseDelayTime = 5000;
		internal const int NotificationToastSecondaryCloseDelayTime = 500;
		internal const int MaxErrorLogSize = 2 * 1024;
		internal const int ErrorLogTrimLineCount = 500;

		#endregion -- Application Settings/Constants --

		#region -- Static Properties --

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

		#endregion -- Static Properties --

		internal Config config;
		internal ErrorLogger errorLogger;
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
			errorLogger = new ErrorLogger(ErrorFilePath, MaxErrorLogSize, ErrorLogTrimLineCount);

			// Setup Notification icon
			{
				var res = new ResourceDictionary {
					Source = new Uri(@"pack://application:,,,/NotificationIcon.xaml")
				};
				notificationIcon = (TaskbarIcon)res["Definition"];
			}

			config.LoadState();
			LoadSavedLayout(true);
		}

		protected override void OnExit(ExitEventArgs e) {

			globalMouseHook.Dispose();
		}

		public void ToggleEnable() {

			if (panel.Visibility == Visibility.Visible) {

				panel.Visibility = Visibility.Hidden;
				globalMouseHook.Disable();

			} else {

				panel.Visibility = Visibility.Visible;
			}
		}

		public void ShowToast(string body, string title, EventHandler clickHandler = null) {

			var toast = new NotificationToast {
				Title = title,
				Body = body,
				SecondaryCloseDelayTime = NotificationToastSecondaryCloseDelayTime
			};

			if (clickHandler != null)
				toast.Click += clickHandler;

			notificationIcon.ShowCustomBalloon(toast, PopupAnimation.Fade, NotificationToastCloseDelayTime);
		}

		public void ShowErrorToast(string body, string title = null) {

			if (title == null)
				title = Taction.Properties.Resources.DefaultErrorToastTitle;

			body = string.Format("{0} {1}", body, Taction.Properties.Resources.ToastErrorBodySuffix);

			ShowToast(body, title, (src, e) => Process.Start(ErrorFilePath));
		}

		public void MoveTo(double x, double y) {

			panel.Left = x;
			panel.Top = y;
			WindowManipulator.FitToNearestDesktop(panel);

			config.Save();
		}

		public void LoadDefaultLayout(bool prompt = false) {

			if (prompt) {

				var res = MessageBox.Show(
					panel,
					"This will reset your current layout.",
					Taction.Properties.Resources.AppName,
					MessageBoxButton.OKCancel,
					MessageBoxImage.Asterisk
				);

				if (res != MessageBoxResult.OK)
					return;
			}

			var encoding = System.Text.Encoding.UTF8;
			var text = encoding.GetString(Taction.Properties.Resources.DefaultConfigLayoutJson);
			var json = JObject.Parse(text);

			// Load and validate
			this.config.LoadLayout(json);

			// Update UI
			if (this.panel != null)
				this.panel.ReloadLayout();

			// Persist for later
			File.WriteAllText(Config.FileLayoutPath, text, encoding);
		}

		public void PromptLoadLayout() {

			var initialDir = config.state.fileDialogInitialDirectory;
			if (initialDir == null || !Directory.Exists(initialDir))
				initialDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			var openFileDialog = new OpenFileDialog {
				InitialDirectory = initialDir,
				Filter = string.Format(
					"Taction Config (*{0})|*{0}|JSON files (*.json)|*.json",
					Taction.Properties.Resources.ConfigBundleFileExtension
				)
			};

			if (openFileDialog.ShowDialog(Application.Current.MainWindow) != true)
				return;

			config.state.fileDialogInitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
			config.Save();

			LoadLayout(openFileDialog.FileName);
		}

		public void LoadSavedLayout(bool useFallback = false) {

			string targetFile = (File.Exists(Config.FileBundlePath)) ?
				Config.FileBundlePath :
				(File.Exists(Config.FileLayoutPath)) ?
					Config.FileLayoutPath :
					null;

			if (targetFile != null) {

				try {

					config.LoadLayout(targetFile);

				} catch (Exception e) {

					errorLogger.Log(e.ToString());
					LoadDefaultLayout();
					File.Move(targetFile, targetFile + ".bak");
					ShowErrorToast("Problem loading saved layout. Reverting to default.");
				}

			} else if (useFallback) {

				LoadDefaultLayout();

			} else {

				ShowToast("Could not load saved layout.", Taction.Properties.Resources.DefaultErrorToastTitle);
			}
		}

		public void LoadLayout(string path) {

			string errSummary = "Config load error.";
			string errDetails = null;
			try {

				// Load and validate
				this.config.LoadLayout(path);

				// Update UI
				this.panel.ReloadLayout();

				// Remove previous file(s)
				File.Delete(Config.FileLayoutPath);
				File.Delete(Config.FileBundlePath);

				// Persist for resume
				var targetFile = (Path.GetExtension(path) == Taction.Properties.Resources.ConfigBundleFileExtension) ?
					Config.FileBundlePath :
					Config.FileLayoutPath;

				File.Copy(path, targetFile, true);

			} catch (FormatException e) {

				errSummary = "Config validation error.";
				errDetails = e.Message;

			} catch (Newtonsoft.Json.JsonReaderException e) {

				errSummary = "Config syntax error.";
				errDetails = e.Message;

			} catch (Exception err) {

				errDetails = err.ToString();
			}

			if (errDetails != null) {

				var msg = errSummary + Environment.NewLine + errDetails;

				this.errorLogger.Log(msg);
				this.ShowErrorToast(errSummary);
			}
		}
	}
}
