using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls.Primitives;
using Taction.UIElement;

namespace Taction {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		internal Config Config;
		internal ErrorLogger ErrorLogger;
		internal MainPanel MainPanel => (MainPanel)MainWindow;
		internal TaskbarIcon NotificationIcon { get; private set; }
		internal GlobalMouseHook GlobalMouseHook { get; private set; }
		internal InputSimulatorHelper InputSimulator { get; private set; }

		protected override void OnStartup(StartupEventArgs e) {

			base.OnStartup(e);

			// Ensure app data directory
			Directory.CreateDirectory(AppDataDir);

			// Initialize basic stuff
			GlobalMouseHook = new GlobalMouseHook();
			InputSimulator = new InputSimulatorHelper();
			Config = new Config();
			ErrorLogger = new ErrorLogger(ErrorFilePath, MaxErrorLogSize, ErrorLogTrimLineCount);

			// Setup Notification icon
			{
				var res = new ResourceDictionary {
					Source = new Uri(@"pack://application:,,,/NotificationIcon.xaml")
				};
				NotificationIcon = (TaskbarIcon)res["Definition"];
			}

			Config.LoadState();
			LoadSavedLayout(true);
		}

		protected override void OnExit(ExitEventArgs e) {

			GlobalMouseHook.Dispose();
		}

		public void ToggleEnable() {

			if (MainPanel.Visibility == Visibility.Visible) {

				MainPanel.Visibility = Visibility.Hidden;
				GlobalMouseHook.Disable();

			} else {

				MainPanel.Visibility = Visibility.Visible;
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

			NotificationIcon.ShowCustomBalloon(toast, PopupAnimation.Fade, NotificationToastCloseDelayTime);
		}

		public void ShowErrorToast(string body, string title = null) {

			if (title == null)
				title = Taction.Properties.Resources.DefaultErrorToastTitle;

			body = string.Format("{0} {1}", body, Taction.Properties.Resources.ToastErrorBodySuffix);

			ShowToast(body, title, (src, e) => Process.Start(ErrorFilePath));
		}

		public void MoveTo(double x, double y) {

			MainPanel.Left = x;
			MainPanel.Top = y;
			WindowManipulator.FitToNearestDesktop(MainPanel);

			Config.Save();
		}

		public void LoadDefaultLayout(bool prompt = false) {

			if (prompt) {

				var res = MessageBox.Show(
					MainPanel,
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
			Config.LoadLayout(json);

			// Update UI
			if (MainPanel != null)
				MainPanel.ReloadLayout();

			// Persist for later
			File.WriteAllText(App.FileLayoutPath, text, encoding);
		}

		public void PromptLoadLayout() {

			var initialDir = Config.State.FileDialogInitialDirectory;
			if (initialDir == null || !Directory.Exists(initialDir))
				initialDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			var openFileDialog = new OpenFileDialog {
				InitialDirectory = initialDir,
				Filter = string.Format(
					"Supported files|*{0};*.json|Taction bundle|*{0}|JSON layout|*.json",
					Taction.Properties.Resources.ConfigBundleFileExtension
				)
			};

			if (openFileDialog.ShowDialog(Application.Current.MainWindow) != true)
				return;

			Config.State.FileDialogInitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
			Config.Save();

			LoadLayout(openFileDialog.FileName);
		}

		public void LoadSavedLayout(bool useFallback = false) {

			string targetFile = (File.Exists(App.FileBundlePath)) ?
				App.FileBundlePath :
				(File.Exists(App.FileLayoutPath)) ?
					App.FileLayoutPath :
					null;

			if (targetFile != null) {

				try {

					Config.LoadLayout(targetFile);

				} catch (Exception e) {

					ErrorLogger.Log(e.ToString());
					LoadDefaultLayout();

					var backupFile = targetFile + ".bak";
					File.Delete(backupFile);
					File.Move(targetFile, backupFile);
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
				Config.LoadLayout(path);

				// Update UI
				MainPanel.ReloadLayout();

				// Remove previous file(s)
				File.Delete(App.FileLayoutPath);
				File.Delete(App.FileBundlePath);

				// Persist for resume
				var targetFile = (Path.GetExtension(path) == Taction.Properties.Resources.ConfigBundleFileExtension) ?
					App.FileBundlePath :
					App.FileLayoutPath;

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

				ErrorLogger.Log(msg);
				ShowErrorToast(errSummary);
			}
		}
	}
}
