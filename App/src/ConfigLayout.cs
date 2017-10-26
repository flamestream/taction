﻿using Newtonsoft.Json;
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

		[JsonProperty("default-button-style")]
		public ButtonStyleSetSpecs DefaultButtonStyle { get; set; }
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

	public class ButtonStyleSpecs {

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

		[JsonProperty("radius")]
		[JsonConverter(typeof(CornerRadiusJsonConverter))]
		public CornerRadius? Radius { get; set; }
	}

	public class RadialMenuSpecs {

		[JsonProperty("central-item-style")]
		public RadialMenuCentralItemSpecs CenterSpecs { get; set; }

		[JsonProperty("active-central-item-style")]
		public RadialMenuCentralItemSpecs ActiveCenterSpecs { get; set; }

		[JsonProperty("default-inner-edge-style")]
		public RadialMenuItemEdgeSpecs DefaultInnerEdgeSpecs { get; set; }

		[JsonProperty("default-active-inner-edge-style")]
		public RadialMenuItemEdgeSpecs DefaultActiveInnerEdgeSpecs { get; set; }

		[JsonProperty("default-outer-edge-style")]
		public RadialMenuItemEdgeSpecs DefaultOuterEdgeSpecs { get; set; }

		[JsonProperty("default-active-outer-edge-style")]
		public RadialMenuItemEdgeSpecs DefaultActiveOuterEdgeSpecs { get; set; }

		[JsonProperty("items")]
		public List<RadialMenuItemSpecs> Items { get; set; }
	}

	public class RadialMenuCentralItemSpecs : ButtonStyleSpecs {

		[JsonProperty("size")]
		public double Size { get; set; }
	}

	public class RadialMenuItemSpecs {

		[JsonProperty("inner-edge")]
		public RadialMenuItemEdgeSpecs InnerEdgeSpecs { get; set; }

		[JsonProperty("active-inner-edge")]
		public RadialMenuItemEdgeSpecs ActiveInnerEdgeSpecs { get; set; }

		[JsonProperty("outer-edge")]
		public RadialMenuItemEdgeSpecs OuterEdgeSpecs { get; set; }

		[JsonProperty("active-outer-edge")]
		public RadialMenuItemEdgeSpecs ActiveOuterEdgeSpecs { get; set; }

		[JsonProperty("content")]
		public RadialMenuItemContentSpecs ContentSpecs { get; set; }

		[JsonProperty("active-content")]
		public RadialMenuItemContentSpecs ActiveContentSpecs { get; set; }
	}

	public class RadialMenuItemEdgeSpecs {

		[JsonProperty("size")]
		public double Size { get; set; }

		[JsonProperty("start-distance")]
		public double StartDistance { get; set; }

		[JsonProperty("color")]
		[JsonConverter(typeof(BrushJsonConverter))]
		public Brush Color { get; set; }

		[JsonProperty("border")]
		public BorderSpecs Border { get; set; }
	}

	public class RadialMenuItemContentSpecs : ButtonStyleSpecs {

		[JsonProperty("size")]
		public double Size { get; set; }
	}
}