using Newtonsoft.Json;

namespace Taction {

	internal class ConfigState {

		[JsonProperty("x")]
		public double X { get; set; }

		[JsonProperty("y")]
		public double Y { get; set; }

		[JsonProperty("file-dialog-initial-directory")]
		public string FileDialogInitialDirectory { get; set; }
	}
}
