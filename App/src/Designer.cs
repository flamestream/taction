using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Taction.Attribute;
using Taction.UIElement;

namespace Taction {

	internal static partial class Designer {

		public static void GenerateLayout(MainPanel window) {

			var config = App.Instance.Config;

			var layoutData = config.Layout;
			var stateData = config.State;

			// Setup window
			window.Opacity = layoutData.Opacity;
			window.Background = layoutData.Color;

			window.BorderThickness = default(Thickness);
			window.BorderBrush = null;
			if (layoutData.Border != null) {

				if (layoutData.Border.Thickness != null)
					window.BorderThickness = layoutData.Border.Thickness.Value;

				if (layoutData.Border.Color != null)
					window.BorderBrush = layoutData.Border.Color;
			}

			// Setup main panel
			var panel = new StackPanel {
				Orientation = config.Layout.Orientation,
			};

			if (layoutData.Margin != null)
				panel.Margin = layoutData.Margin.Value;

			if (layoutData.Orientation == Orientation.Vertical) {
				panel.Width = layoutData.Size;
			} else {
				panel.Height = panel.Height = layoutData.Size;
			}
			window.Container.Children.Add(panel);

			// Compute children items
			ProcessLayout(layoutData, layoutData.Items, panel);

			// Set position
			window.Left = stateData.X;
			window.Top = stateData.Y;
		}

		private static void ProcessLayout(ConfigLayout layout, List<IPanelItemSpecs> specsList, StackPanel panel) {

			if (specsList == null)
				return;

			foreach (var specs in specsList) {

				var specsType = specs.GetType();
				var attr = (AssociatedClassAttribute)specsType.GetCustomAttributes(typeof(AssociatedClassAttribute), true)[0];
				var itemType = attr.Value;
				var item = (FrameworkElement)Activator.CreateInstance(itemType, specs);

				// Set size
				if (panel.Orientation == Orientation.Vertical) {

					item.Height = specs.Size;

				} else {

					item.Width = specs.Size;
				}

				// Set base style
				if (specs is IButtonSpecs)
					ApplyStyle(layout, (ContentControl)item, (IButtonSpecs)specs, panel);

				// Add to tree
				panel.Children.Add(item);

				// Special: Panel handling
				if (item is StackPanel childPanel) {

					childPanel.Orientation = panel.Orientation == Orientation.Horizontal ?
						Orientation.Vertical :
						Orientation.Horizontal;

					// Process Children
					ProcessLayout(layout, ((PivotSpecs)specs).Items, childPanel);
				}
			}
		}

		private static void ApplyBaseStyle(ContentControl item, StyleSpecs style) {

			if (style == null)
				return;

			if (style.Color != null)
				item.Background = style.Color;

			if (style.Content != null)
				item.Content = style.Content;

			if (style.Margin != null)
				item.Margin = style.Margin.Value;

			if (style.ContentPadding != null)
				item.Padding = style.ContentPadding.Value;

			if (style.Border != null) {

				if (style.Border.Color != null)
					item.BorderBrush = style.Border.Color;

				if (style.Border.Thickness != null)
					item.BorderThickness = style.Border.Thickness.Value;
			}

			if (style.TextStyle != null) {

				if (style.TextStyle.Color != null)
					item.Foreground = style.TextStyle.Color;

				if (style.TextStyle.FontFamily != null)
					item.FontFamily = style.TextStyle.FontFamily;

				if (style.TextStyle.FontSize != null)
					item.FontSize = style.TextStyle.FontSize.Value;

				if (style.TextStyle.FontWeight != null)
					item.FontWeight = style.TextStyle.FontWeight.Value;
			}
		}

		private static void ApplyActiveStyle(ICustomStylizable item, StyleSpecs style) {

			if (style == null)
				return;

			if (style.Color != null)
				item.Active_Background = style.Color;

			if (style.Content != null)
				item.Active_Content = style.Content;

			if (style.Margin != null)
				item.Active_Margin = style.Margin.Value;

			if (style.Border != null) {

				if (style.Border.Color != null)
					item.Active_BorderBrush = style.Border.Color;

				if (style.Border.Thickness != null)
					item.Active_BorderThickness = style.Border.Thickness.Value;
			}

			if (style.TextStyle != null) {

				if (style.TextStyle.Color != null)
					item.Active_Foreground = style.TextStyle.Color;

				if (style.TextStyle.FontFamily != null)
					item.Active_FontFamily = style.TextStyle.FontFamily;

				if (style.TextStyle.FontSize != null)
					item.Active_FontSize = style.TextStyle.FontSize.Value;

				if (style.TextStyle.FontWeight != null)
					item.Active_FontWeight = style.TextStyle.FontWeight.Value;
			}
		}

		private static void ApplyStyle(ConfigLayout layout, ContentControl item, IButtonSpecs specs, StackPanel panel) {

			// Set base style
			ApplyBaseStyle(item, layout.DefaultBaseStyle);
			ApplyBaseStyle(item, specs.BaseStyle);

			if (item is ICustomStylizable i) {

				// Copy base
				i.Active_Background = item.Background;
				i.Active_BorderBrush = item.BorderBrush;
				i.Active_BorderThickness = item.BorderThickness;
				i.Active_Content = item.Content;
				i.Active_FontFamily = item.FontFamily;
				i.Active_FontSize = item.FontSize;
				i.Active_Foreground = item.Foreground;
				i.Active_Margin = item.Margin;

				// Set active style
				ApplyActiveStyle(i, layout.DefaultActiveStyle);
				ApplyActiveStyle(i, specs.ActiveStyle);
			}
		}

		public static void AdjustGradientColor(ContentControl item, Brush brush) {

			// Type heck
			if (!(brush is LinearGradientBrush))
				return;

			var gradientBrush = (LinearGradientBrush)brush;

			// Angle check
			if (Point.Subtract(gradientBrush.StartPoint, gradientBrush.EndPoint).Length == 1)
				return;

			var ratio = item.ActualWidth / item.ActualHeight;

			var displacement = (ratio - 1) * 0.5;

			gradientBrush.StartPoint = new Point(gradientBrush.StartPoint.X, ratio * gradientBrush.StartPoint.Y - displacement);
			gradientBrush.EndPoint = new Point(gradientBrush.EndPoint.X, ratio * gradientBrush.EndPoint.Y - displacement);
		}
	}
}
