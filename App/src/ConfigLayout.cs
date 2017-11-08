using Newtonsoft.Json;
using PropertyChanged;
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

		[JsonProperty("disable-radial-menu-animation")]
		public bool DisableRadialMenuAnimation { get; set; }

		[JsonProperty("default-button-style")]
		public ButtonStyleSetSpecs DefaultButtonStyle { get; set; }

		[JsonProperty("default-radial-menu-item-style")]
		public RadialMenuItemStyleSetSpecs DefaultRadialMenuItemStyle { get; set; }

		[JsonProperty("default-radial-menu-central-item-style")]
		public RadialMenuCentralItemStyleSetSpecs DefaultRadialMenuCentralItemStyle { get; set; }
	}

	public interface IPanelItemSpecs {

		[JsonProperty("size")]
		double Size { get; set; }
	}

	public interface IButtonSpecs : IPanelItemSpecs {

		[JsonProperty("style")]
		ButtonStyleSetSpecs Style { get; set; }
	}

	public class ButtonStyleSetSpecs : IPanelItemSpecs {

		public double Size { get; set; }

		[JsonProperty("base")]
		public ButtonStyleSpecs Base { get; set; }

		[JsonProperty("active")]
		public ButtonStyleSpecs Active { get; set; }

		public ButtonStyleSetSpecs() {

			Base = new ButtonStyleSpecs();
			Active = new ButtonStyleSpecs();
		}
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
		public ButtonStyleSetSpecs Style { get; set; }
		public KeyCommand KeyCommand { get; set; }
	}

	[AssociatedClass(typeof(TapButton))]
	[JsonStringTypeValue("tap")]
	public class TapButtonSpecs : ICommandButtonSpecs {

		public double Size { get; set; }
		public ButtonStyleSetSpecs Style { get; set; }
		public KeyCommand KeyCommand { get; set; }
	}

	[AssociatedClass(typeof(CustomToggleButton))]
	[JsonStringTypeValue("toggle")]
	public class ToggleButtonSpecs : ICommandButtonSpecs {

		public double Size { get; set; }
		public ButtonStyleSetSpecs Style { get; set; }
		public KeyCommand KeyCommand { get; set; }
	}

	[AssociatedClass(typeof(MoveButton))]
	[JsonStringTypeValue("move")]
	public class MoveButtonSpecs : IButtonSpecs {

		public double Size { get; set; }
		public ButtonStyleSetSpecs Style { get; set; }
	}

	[AssociatedClass(typeof(RadialMenuButton))]
	[JsonStringTypeValue("radial-menu")]
	public class RadialMenuButtonSpecs : IButtonSpecs {

		public double Size { get; set; }
		public ButtonStyleSetSpecs Style { get; set; }

		[JsonProperty("radial-menu")]
		public RadialMenuSpecs RadialMenuSpecs { get; set; }
	}

	public class ButtonStyleSpecs : INotifyPropertyChanged {

		[JsonProperty("margin")]
		[JsonConverter(typeof(ThicknessJsonConverter))]
		public Thickness? Margin { get; set; }

		[JsonProperty("padding")]
		[JsonConverter(typeof(ThicknessJsonConverter))]
		public Thickness? Padding { get; set; }

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

		[JsonProperty("opacity")]
		public double? Opacity { get; set; }

		public ButtonStyleSpecs() {

			Border = new BorderSpecs();
			TextStyle = new TextStyleSpecs();
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class TextStyleSpecs : INotifyPropertyChanged {

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

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class BorderSpecs : INotifyPropertyChanged {

		[JsonProperty("color")]
		[JsonConverter(typeof(BrushJsonConverter))]
		public Brush Color { get; set; }

		[JsonProperty("thickness")]
		[JsonConverter(typeof(ThicknessJsonConverter))]
		public Thickness? Thickness { get; set; }

		[JsonProperty("radius")]
		[JsonConverter(typeof(CornerRadiusJsonConverter))]
		public CornerRadius? Radius { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class RadialMenuSpecs : INotifyPropertyChanged {

		[JsonProperty("half-shifted-items")]
		public bool HalfShiftedItems { get; set; }

		[JsonProperty("default-item-style")]
		public RadialMenuItemStyleSetSpecs DefaultItemStyle { get; set; }

		[JsonProperty("central-item-style")]
		public RadialMenuCentralItemStyleSetSpecs CentralItemSpecs { get; set; }

		[JsonProperty("items")]
		public List<RadialMenuItemSpecs> Items { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class RadialMenuItemSpecs {

		[JsonProperty("command")]
		[JsonConverter(typeof(KeyCommandListJsonConverter))]
		public KeyCommand KeyCommand { get; set; }

		[JsonProperty("style")]
		public RadialMenuItemStyleSetSpecs Style { get; set; }
	}

	public class RadialMenuItemStyleSetSpecs {

		[JsonProperty("base")]
		public RadialMenuItemStyleSpecs Base { get; set; }

		[JsonProperty("active")]
		public RadialMenuItemStyleSpecs Active { get; set; }
	}

	public class RadialMenuItemStyleSpecs {

		[JsonProperty("label")]
		public RadialMenuItemLabelSpecs LabelSpecs { get; set; }

		[JsonProperty("inner-edge")]
		public RadialMenuItemEdgeSpecs InnerEdgeSpecs { get; set; }

		[JsonProperty("outer-edge")]
		public RadialMenuItemEdgeSpecs OuterEdgeSpecs { get; set; }
	}

	public class RadialMenuItemLabelSpecs : ButtonStyleSpecs {

		[JsonProperty("size")]
		public double? Size { get; set; }

		[JsonProperty("start-distance")]
		public double? StartDistance { get; set; }
	}

	public class RadialMenuItemEdgeSpecs {

		[JsonProperty("size")]
		public double? Size { get; set; }

		[JsonProperty("start-distance")]
		public double? StartDistance { get; set; }

		[JsonProperty("color")]
		[JsonConverter(typeof(BrushJsonConverter))]
		public Brush Color { get; set; }

		[JsonProperty("border")]
		public BorderSpecs Border { get; set; }
	}

	public class RadialMenuCentralItemStyleSetSpecs {

		[JsonProperty("base")]
		public RadialMenuCentralItemStyleSpecs Base { get; set; }

		[JsonProperty("active")]
		public RadialMenuCentralItemStyleSpecs Active { get; set; }
	}

	public class RadialMenuCentralItemStyleSpecs {

		[JsonProperty("size")]
		public double? Size { get; set; }

		[JsonProperty("content")]
		[JsonConverter(typeof(ContentJsonConverter))]
		public Object Content { get; set; }

		[JsonProperty("text-style")]
		public TextStyleSpecs TextStyle { get; set; }

		[JsonProperty("color")]
		[JsonConverter(typeof(BrushJsonConverter))]
		public Brush Color { get; set; }

		[JsonProperty("border")]
		public BorderSpecs Border { get; set; }

		[JsonProperty("padding")]
		[JsonConverter(typeof(ThicknessJsonConverter))]
		public Thickness? Padding { get; set; }
	}
}
