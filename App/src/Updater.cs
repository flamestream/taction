using Newtonsoft.Json;
using System;
using System.Net;

namespace Taction {

	internal static class Updater {

		public static async void Check() {

			try {

				var app = App.Instance;
				var config = app.Config;
				var lastCheck = config.State.LastUpdateCheck;

				// Last check date check
				if (lastCheck != default(DateTime)) {

					var nextCheck = lastCheck.AddDays(1);
					if (nextCheck > DateTime.Now)
						return;
				}

				using (var client = new WebClient()) {

					// Github API needs this
					client.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)";

					var json = client.DownloadString("https://api.github.com/repos/flamestream/taction/releases/latest");
					var releaseInfo = JsonConvert.DeserializeObject<ReleaseInfo>(json);

					config.State.LastUpdateCheck = DateTime.Now;
					config.Save();

					// Skip check
					if (config.State.SkipReleaseVersion == releaseInfo.TagName)
						return;

					// Same version check
					var c = app.GetVersionTagName();
					if (app.GetVersionTagName() == releaseInfo.TagName)
						return;

					var window = new NewReleaseWindow() {
						ReleaseVersion = releaseInfo.TagName,
						HtmlUrl = releaseInfo.HtmlUrl,
					};

					window.Owner = app.MainWindow;
					window.ShowDialog();
				}

			} catch (Exception e) {

				App.Instance.ErrorLogger.Log(e.Message);
			}
		}

		public class ReleaseInfo {

			[JsonProperty("html_url")]
			public string HtmlUrl { get; set; }

			[JsonProperty("tag_name")]
			public string TagName { get; set; }
		}
	}
}
