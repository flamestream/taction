using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
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

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("size")]
		public double Size { get; set; }

		[JsonProperty("margin")]
		[JsonConverter(typeof(ThicknessJsonConverter))]
		public Thickness? Margin { get; set; }

		[JsonProperty("border")]
		public BorderSpecs Border { get; set; }

		[JsonProperty("color")]
		[JsonConverter(typeof(BrushJsonConverter))]
		public Brush Color { get; set; }

		[JsonProperty("items")]
		[JsonConverter(typeof(PanelItemListJsonConverter))]
		public List<IPanelItemSpecs> Items { get; set; }

		[JsonProperty("orientation")]
		[JsonConverter(typeof(OrientationJsonConverter))]
		public System.Windows.Controls.Orientation Orientation { get; set; }

		[DefaultValue(1)]
		[JsonProperty("opacity", DefaultValueHandling = DefaultValueHandling.Populate)]
		public float Opacity {
			get => _opacity;
			set {
				if (value < 0) value = 0;
				else if (value > 1) value = 1;
				_opacity = value;
			}
		}

		[DefaultValue(0)]
		[JsonProperty("opacity-hide", DefaultValueHandling = DefaultValueHandling.Populate)]
		public float OpacityHide {
			get => _opacityHide;
			set {
				if (value < 0) value = 0;
				else if (value > 1) value = 1;
				_opacityHide = value;
			}
		}

		[JsonProperty("disable-hide")]
		public bool DisableHide { get; set; }

		[DefaultValue(500)]
		[JsonProperty("fade-animation-time", DefaultValueHandling = DefaultValueHandling.Populate)]
		public uint FadeAnimationTime {
			get => _fadeAnimationTime;
			set {
				if (value < 0) value = 0;
				_fadeAnimationTime = value;
			}
		}

		[JsonProperty("disable-fade-animation")]
		public bool DisableFadeAnimation { get; set; }

		[JsonProperty("default-base-style")]
		public StyleSpecs DefaultBaseStyle { get; set; }

		[JsonProperty("default-active-style")]
		public StyleSpecs DefaultActiveStyle { get; set; }
	}

	public interface IPanelItemSpecs {

		[JsonProperty("size")]
		double Size { get; set; }
	}

	public interface IButtonSpecs : IPanelItemSpecs {

		[JsonProperty("base-style")]
		StyleSpecs BaseStyle { get; set; }

		[JsonProperty("active-style")]
		StyleSpecs ActiveStyle { get; set; }
	}

	public interface ICommandButtonSpecs : IButtonSpecs {

		[JsonProperty("command")]
		[JsonConverter(typeof(KeyCommandListJsonConverter))]
		KeyCommand KeyCommand { get; set; }
	}

	[AssociatedClass(typeof(CustomStackPanel))]
	[JsonStringTypeValue("pivot")]
	public class PivotSpecs : IPanelItemSpecs {

		public double Size { get; set; }
		public Thickness? Margin { get; set; }

		[JsonProperty("items")]
		[JsonConverter(typeof(PanelItemListJsonConverter))]
		public List<IPanelItemSpecs> Items { get; set; }
	}

	[AssociatedClass(typeof(HoldButton))]
	[JsonStringTypeValue("hold")]
	public class HoldButtonSpecs : ICommandButtonSpecs {

		public double Size { get; set; }
		public StyleSpecs BaseStyle { get; set; }
		public StyleSpecs ActiveStyle { get; set; }
		public KeyCommand KeyCommand { get; set; }
	}

	[AssociatedClass(typeof(TapButton))]
	[JsonStringTypeValue("tap")]
	public class TapButtonSpecs : ICommandButtonSpecs {

		public double Size { get; set; }
		public StyleSpecs BaseStyle { get; set; }
		public StyleSpecs ActiveStyle { get; set; }
		public KeyCommand KeyCommand { get; set; }
	}

	[AssociatedClass(typeof(CustomToggleButton))]
	[JsonStringTypeValue("toggle")]
	public class ToggleButtonSpecs : ICommandButtonSpecs {

		public double Size { get; set; }
		public StyleSpecs BaseStyle { get; set; }
		public StyleSpecs ActiveStyle { get; set; }
		public KeyCommand KeyCommand { get; set; }
	}

	[AssociatedClass(typeof(MoveButton))]
	[JsonStringTypeValue("move")]
	public class MoveButtonSpecs : IButtonSpecs {

		public double Size { get; set; }
		public StyleSpecs BaseStyle { get; set; }
		public StyleSpecs ActiveStyle { get; set; }
	}

	public class StyleSpecs {

		[JsonProperty("margin")]
		[JsonConverter(typeof(ThicknessJsonConverter))]
		public Thickness? Margin { get; set; }

		[JsonProperty("padding")]
		[JsonConverter(typeof(ThicknessJsonConverter))]
		public Thickness? ContentPadding { get; set; }

		[JsonProperty("border")]
		public BorderSpecs Border { get; set; }

		[JsonProperty("color")]
		[JsonConverter(typeof(BrushJsonConverter))]
		public Brush Color { get; set; }

		[JsonProperty("content")]
		[JsonConverter(typeof(ContentJsonConverter))]
		public Object Content { get; set; }

		[JsonProperty("text-style")]
		public TextStyleSpecs TextStyle { get; set; }
	}

	public class TextStyleSpecs {

		[JsonProperty("font-size")]
		public double? FontSize { get; set; }

		[JsonProperty("color")]
		[JsonConverter(typeof(BrushJsonConverter))]
		public Brush Color { get; set; }

		[JsonProperty("font-family")]
		[JsonConverter(typeof(FontFamilyJsonConverter))]
		public FontFamily FontFamily { get; set; }

		[JsonProperty("font-weight")]
		[JsonConverter(typeof(FontWeightJsonConverter))]
		public FontWeight? FontWeight { get; set; }
	}

	public class BorderSpecs {

		[JsonProperty("color")]
		[JsonConverter(typeof(BrushJsonConverter))]
		public Brush Color { get; set; }

		[JsonProperty("thickness")]
		[JsonConverter(typeof(ThicknessJsonConverter))]
		public Thickness? Thickness { get; set; }
	}
}
