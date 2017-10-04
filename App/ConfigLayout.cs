using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using Taction.Attribute;
using Taction.JsonConverter;
using Taction.UIElement;

namespace Taction {

	/// <summary>
	/// Configuration root definition
	/// </summary>
	internal class ConfigLayout {

		private float _opacity;
		private float _opacityHide;
		private uint _fadeAnimationTime;

		public int size { get; set; }

		[JsonConverter(typeof(PanelItemsConverter))]
		public List<IPanelItemSpecs> items { get; set; }

		[JsonConverter(typeof(OrientationConverter))]
		public System.Windows.Controls.Orientation orientation { get; set; }

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

	public interface IButtonSpecs : IPanelItemSpecs {

		TextSpecs text { get; set; }
		BorderSpecs border { get; set; }

		[JsonConverter(typeof(BrushConverter))]
		System.Windows.Media.Brush color { get; set; }
	}

	public interface ICommandButtonSpecs : IButtonSpecs {

		[JsonProperty("command")]
		string keyCommand { get; set; }
	}

	[AssociatedClass(typeof(StackPanel))]
	[JsonStringTypeValue("pivot")]
	public class PivotSpecs : IPanelItemSpecs {

		public int size { get; set; }

		[JsonConverter(typeof(PanelItemsConverter))]
		public List<IPanelItemSpecs> items { get; set; }
	}

	[AssociatedClass(typeof(HoldButton))]
	[JsonStringTypeValue("hold")]
	public class HoldButtonSpecs : ICommandButtonSpecs {

		public int size { get; set; }
		public TextSpecs text { get; set; }
		public BorderSpecs border { get; set; }
		public string keyCommand { get; set; }
		public System.Windows.Media.Brush color { get; set; }
	}

	[AssociatedClass(typeof(TapButton))]
	[JsonStringTypeValue("tap")]
	public class TapButtonSpecs : ICommandButtonSpecs {

		public int size { get; set; }
		public TextSpecs text { get; set; }
		public BorderSpecs border { get; set; }

		[JsonProperty("command")]
		public string keyCommand { get; set; }

		[JsonConverter(typeof(BrushConverter))]
		public System.Windows.Media.Brush color { get; set; }
	}

	[AssociatedClass(typeof(ToggleButton))]
	[JsonStringTypeValue("toggle")]
	public class ToggleButtonSpecs : ICommandButtonSpecs {

		public int size { get; set; }
		public TextSpecs text { get; set; }
		public BorderSpecs border { get; set; }

		[JsonProperty("command")]
		public string keyCommand { get; set; }

		[JsonConverter(typeof(BrushConverter))]
		public System.Windows.Media.Brush color { get; set; }
	}

	[AssociatedClass(typeof(MoveButton))]
	[JsonStringTypeValue("move")]
	public class MoveButtonSpecs : IButtonSpecs {

		public int size { get; set; }
		public TextSpecs text { get; set; }
		public BorderSpecs border { get; set; }

		[JsonConverter(typeof(BrushConverter))]
		public System.Windows.Media.Brush color { get; set; }
	}

	public class TextSpecs {

		public string value { get; set; }
		public double size { get; set; }

		[JsonConverter(typeof(BrushConverter))]
		public System.Windows.Media.Brush color { get; set; }

		[JsonConverter(typeof(FontFamilyConverter))]
		public System.Windows.Media.FontFamily font { get; set; }
	}

	public class BorderSpecs {

		[JsonConverter(typeof(BrushConverter))]
		public System.Windows.Media.Brush color { get; set; }

		[JsonConverter(typeof(ThicknessConverter))]
		public System.Windows.Thickness thickness { get; set; }
	}
}
