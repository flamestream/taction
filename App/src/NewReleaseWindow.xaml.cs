using System.Windows;

namespace Taction {

	public partial class NewReleaseWindow : Window {

		public string ReleaseVersion { get; set; }
		public string HtmlUrl { get; set; }

		public string BodyText {

			get {

				return string.Format(
					"Version {0} has been released.\nCurrent version is {1}.\nWould you like to review its content?",
					ReleaseVersion,
					UpdateChecker.GetVersionTagName()
				);
			}
		}

		public NewReleaseWindow() {

			InitializeComponent();
			DataContext = this;
		}

		private void YesButton_Click(object sender, RoutedEventArgs e) {

			System.Diagnostics.Process.Start(HtmlUrl);
			Close();
		}

		private void NoButton_Click(object sender, RoutedEventArgs e) {

			var config = App.Instance.Config;
			config.State.SkipReleaseVersion = ReleaseVersion;
			config.Save();
			Close();
		}
	}
}
