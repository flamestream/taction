using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace Taction {

	internal class UpdateChecker {

		public Uri Uri { get; private set; }
		public ReleaseInfoResponse ReleaseInfo { get; private set; }

		public delegate void UpdateAvailableEventHandler(object sender, ReleaseInfoResponse releaseInfo);

		public event UpdateAvailableEventHandler OnUpdateAvailable;

		public UpdateChecker(string apiUri) {

			Uri = new Uri(apiUri);
		}

		public async void Run() {

			await Task.Run(() => {

				ReleaseInfo = RequestInfo();

				// Same version check
				if (GetVersionTagName() == ReleaseInfo.TagName)
					return;

				// Subscriber check
				if (OnUpdateAvailable == null)
					return;

				// Trigger event on main thread
				Application.Current.Dispatcher.Invoke(() => {
					OnUpdateAvailable.Invoke(this, ReleaseInfo);
				});
			});
		}

		public ReleaseInfoResponse RequestInfo() {

			try {

				using (var client = new WebClient()) {

					// Github API needs this
					client.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)";

					var json = client.DownloadString(Uri);
					return JsonConvert.DeserializeObject<ReleaseInfoResponse>(json);
				}

			} catch (Exception e) {

				App.Instance.ErrorLogger.Log(e.Message);
			}

			return null;
		}

		public static string GetVersionTagName() {

			var version = App.Instance.GetVersion();
			return string.Format("v{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
		}

		public class ReleaseInfoResponse {

			[JsonProperty("html_url")]
			public string HtmlUrl { get; set; }

			[JsonProperty("tag_name")]
			public string TagName { get; set; }
		}
	}
}
