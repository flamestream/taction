using Newtonsoft.Json;
using System;

namespace Taction {

	internal class ConfigState {

		[JsonProperty("x")]
		public double X { get; set; }

		[JsonProperty("y")]
		public double Y { get; set; }

		[JsonProperty("file-dialog-initial-directory")]
		public string FileDialogInitialDirectory { get; set; }

		[JsonProperty("last-update-nag-date")]
		public DateTime LastUpdateNagDate { get; set; }

		[JsonProperty("skip-release-tag-name")]
		public string SkipReleaseVersion { get; set; }
	}
}
