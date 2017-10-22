using System;
using System.Windows;
using System.Windows.Media;

namespace Taction.UIElement {

	internal interface ICustomStylizable {

		Brush Active_Background { get; set; }
		Brush Active_BorderBrush { get; set; }
		Thickness Active_BorderThickness { get; set; }
		Object Active_Content { get; set; }
		Brush Active_Foreground { get; set; }
		FontFamily Active_FontFamily { get; set; }
		double Active_FontSize { get; set; }
		FontWeight Active_FontWeight { get; set; }
		Thickness Active_Margin { get; set; }
		Thickness Active_Padding { get; set; }
		double Active_Size { get; set; }
	}
}
