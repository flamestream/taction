using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Taction {

	internal class UpdateChecker {

		public bool IsRunning { get; private set; }
		public CancellationTokenSource CancelToken { get; private set; }
		public Uri Uri { get; private set; }
		public ReleaseInfoResponse ReleaseInfo { get; private set; }

		public delegate void UpdateAvailableEventHandler(object sender, ReleaseInfoResponse releaseInfo);

		public event UpdateAvailableEventHandler OnUpdateAvailable;

		public UpdateChecker(string apiUri) {

			Uri = new Uri(apiUri);
		}

		private async void Run(int interval, CancellationToken token) {

			while (true) {

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

				await Task.Delay(interval, token);

				if (token.IsCancellationRequested)
					return;
			}
		}

		public void Start(int? interval = null) {

			Stop();

			if (interval == null)
				interval = App.UpdateCheckInterval;

			CancelToken = new CancellationTokenSource();
			Run(interval.Value, CancelToken.Token);
		}

		public void Stop() {

			if (CancelToken != null)
				CancelToken.Cancel();
		}

		public ReleaseInfoResponse RequestInfo() {

			try {

				using (var client = new WebClient()) {

					// Github API needs this
					client.Headers["User-Agent"] = App.UpdateCheckerUserAgent;

					var json = client.DownloadString(Uri);
					return JsonConvert.DeserializeObject<ReleaseInfoResponse>(json);
				}

			} catch (Exception e) {

				App.Instance.LogError(e.Message);
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
