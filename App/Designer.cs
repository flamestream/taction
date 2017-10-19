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

			App.Instance.OutBoundaries.Clear();

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
				Margin = layoutData.Margin.Value,
			};
			if (layoutData.Orientation == Orientation.Vertical) {
				panel.Width = layoutData.Size;
			} else {
				panel.Height = panel.Height = layoutData.Size;
			}
			window.Container.Children.Add(panel);

			// Compute children items
			ProcessLayout(layoutData.Items, panel);

			// Set position
			window.Left = stateData.X;
			window.Top = stateData.Y;
		}

		private static void ProcessLayout(List<IPanelItemSpecs> specsList, StackPanel panel) {

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
					ApplyStyle((ContentControl)item, (IButtonSpecs)specs, panel);

				// Add to tree
				panel.Children.Add(item);

				// Special: Panel handling
				if (item is StackPanel) {

					var childPanel = (StackPanel)item;
					childPanel.Orientation = panel.Orientation == Orientation.Horizontal ?
						Orientation.Vertical :
						Orientation.Horizontal;

					// Process Children
					ProcessLayout(((PivotSpecs)specs).Items, childPanel);

				} else if (item is UIElement.MoveButton) {

					var origin = item.TranslatePoint(new Point(), null);
					var bounds = new Rect(origin.X, origin.Y, item.ActualWidth, item.ActualHeight);
					App.Instance.OutBoundaries.Add(bounds);
				}
			}
		}

		private static void ApplyStyle(ContentControl item, IButtonSpecs specs, StackPanel panel) {

			var baseStyle = specs.BaseStyle;

			if (baseStyle != null) {

				if (baseStyle.Color != null)
					item.Background = baseStyle.Color;

				if (baseStyle.Content != null)
					item.Content = baseStyle.Content;

				if (baseStyle.Margin != null)
					item.Margin = baseStyle.Margin.Value;

				if (baseStyle.ContentPadding != null)
					item.Padding = baseStyle.ContentPadding.Value;

				if (baseStyle.Border != null) {

					if (baseStyle.Border.Color != null)
						item.BorderBrush = baseStyle.Border.Color;

					if (baseStyle.Border.Thickness != null)
						item.BorderThickness = baseStyle.Border.Thickness.Value;
				}

				if (baseStyle.TextStyle != null) {

					if (baseStyle.TextStyle.Color != null)
						item.Foreground = baseStyle.TextStyle.Color;

					if (baseStyle.TextStyle.Font != null)
						item.FontFamily = baseStyle.TextStyle.Font;

					if (baseStyle.TextStyle.Size != null)
						item.FontSize = baseStyle.TextStyle.Size.Value;
				}
			}

			if (item is ICustomStylizable) {

				var i = (ICustomStylizable)item;

				// Set default
				i.Active_Background = item.Background;
				i.Active_BorderBrush = item.BorderBrush;
				i.Active_BorderThickness = item.BorderThickness;
				i.Active_Content = item.Content;
				i.Active_FontFamily = item.FontFamily;
				i.Active_FontSize = item.FontSize;
				i.Active_Foreground = item.Foreground;
				i.Active_Margin = item.Margin;

				// Set active style
				var activeStyle = specs.ActiveStyle;
				if (specs.ActiveStyle != null) {

					if (activeStyle.Color != null)
						i.Active_Background = activeStyle.Color;

					if (activeStyle.Content != null)
						i.Active_Content = activeStyle.Content;

					if (activeStyle.Margin != null)
						i.Active_Margin = activeStyle.Margin.Value;

					if (activeStyle.Border != null) {

						if (activeStyle.Border.Color != null)
							i.Active_BorderBrush = activeStyle.Border.Color;

						if (activeStyle.Border.Thickness != null)
							i.Active_BorderThickness = activeStyle.Border.Thickness.Value;
					}

					if (activeStyle.TextStyle != null) {

						if (activeStyle.TextStyle.Color != null)
							i.Active_Foreground = activeStyle.TextStyle.Color;

						if (activeStyle.TextStyle.Font != null)
							i.Active_FontFamily = activeStyle.TextStyle.Font;

						if (activeStyle.TextStyle.Size != null)
							i.Active_FontSize = activeStyle.TextStyle.Size.Value;
					}
				}
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
