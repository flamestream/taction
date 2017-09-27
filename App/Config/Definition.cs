using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace ArtTouchPanel {

	partial class Config {

		/// <summary>
		/// Configuration root definition
		/// </summary>
		public class Data : PanelSpecs {

			private float _opacity;
			private float _opacityHide;
			private uint _fadeAnimationTime;

			[DefaultValue(1)]
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
			public float opacity {
				get => _opacity;
				set {
					if (value < 0) value = 0;
					else if (value > 1) value = 1;
					_opacity = value;
				}
			}

			[DefaultValue(0)]
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
			public float opacityHide {
				get => _opacityHide;
				set {
					if (value < 0) value = 0;
					else if (value > 1) value = 1;
					_opacityHide = value;
				}
			}

			public bool disableHide { get; set; }

			[DefaultValue(500)]
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
			public uint fadeAnimationTime {
				get => _fadeAnimationTime;
				set {
					if (value < 0) value = 0;
					_fadeAnimationTime = value;
				}
			}

			public bool disableFadeAnimation { get; set; }
		}

		public interface IPanelItemSpecs {

			int size { get; set; }
		}

		public class ButtonSpecs : IPanelItemSpecs {

			public int size { get; set; }
			public string text { get; set; }

			[JsonProperty("command")]
			public string keyCommand { get; set; }
		}

		public class PanelSpecs : IPanelItemSpecs {

			public int size { get; set; }

			[JsonConverter(typeof(PanelOrientationConverter))]
			public System.Windows.Controls.Orientation orientation { get; set; }

			[JsonConverter(typeof(PanelItemsConverter))]
			public List<IPanelItemSpecs> items;
		}

		public class MoverSpecs : IPanelItemSpecs {

			public int size { get; set; }

			[DefaultValue("☰☰")]
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
			public string text { get; set; }
		}
	}
}
