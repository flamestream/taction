using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Taction.Attribute;

namespace Taction {

	partial class MainPanel {

		internal static class Designer {

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
						window.BorderThickness = layoutData.Border.Thickness;

					if (layoutData.Border.Color != null)
						window.BorderBrush = layoutData.Border.Color;
				}

				// Setup main panel
				var panel = new StackPanel {
					Orientation = config.Layout.Orientation,
					Margin = layoutData.Margin,
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

			private static void ProcessLayout(List<IPanelItemSpecs> specsList, StackPanel currentPanel) {

				if (specsList == null)
					return;

				foreach (var specs in specsList) {

					double width, height;
					var specsType = specs.GetType();
					var attr = (AssociatedClassAttribute)specsType.GetCustomAttributes(typeof(AssociatedClassAttribute), true)[0];
					var itemType = attr.Value;
					var item = (FrameworkElement)Activator.CreateInstance(itemType, specs);

					// Set size
					if (currentPanel.Orientation == Orientation.Vertical) {

						width = currentPanel.Width;
						height = item.Height = specs.Size;

					} else {

						width = item.Width = specs.Size;
						height = currentPanel.Height;
					}

					// Set margin
					if (specs.Margin != null)
						item.Margin = specs.Margin;

					// Set button-exclusive properties
					if (specs is IButtonSpecs)
						ApplyStyle((ContentControl)item, (IButtonSpecs)specs, currentPanel);

					// Add to tree
					currentPanel.Children.Add(item);

					// Special: Panel handling
					if (item is StackPanel) {

						var childPanel = (StackPanel)item;
						childPanel.Orientation = currentPanel.Orientation == Orientation.Horizontal ?
							Orientation.Vertical :
							Orientation.Horizontal;

						// Process Children
						ProcessLayout(((PivotSpecs)specs).Items, childPanel);

					} else if (item is UIElement.MoveButton) {

						var origin = item.TranslatePoint(new Point(), null);
						var bounds = new Rect(origin.X, origin.Y, width, height);
						App.Instance.OutBoundaries.Add(bounds);
					}
				}
			}

			private static void ApplyStyle(ContentControl item, IButtonSpecs specs, StackPanel panel) {

				// Set content
				if (specs.Content != null)
					item.Content = specs.Content;

				// Set margin
				if (specs.Padding != null)
					item.Padding = specs.Padding;

				// Set Text
				if (specs.TextStyle != null) {
					
					item.FontSize = specs.TextStyle.Size;

					if (specs.TextStyle.Color != null)
						item.Foreground = specs.TextStyle.Color;

					if (specs.TextStyle.Font != null)
						item.FontFamily = specs.TextStyle.Font;
				}

				// Set background
				if (specs.Color != null) {

					item.Background = specs.Color;
					AdjustGradientColor(item.Background, item, panel);
				}

				// Set border
				if (specs.Border != null) {

					if (specs.Border.Thickness != null)
						item.BorderThickness = specs.Border.Thickness;

					if (specs.Border.Color != null) {

						item.BorderBrush = specs.Border.Color;
						AdjustGradientColor(item.BorderBrush, item, panel);
					}
				}
			}

			private static void AdjustGradientColor(Brush brush, ContentControl item, StackPanel panel) {

				// Type heck
				if (!(brush is LinearGradientBrush))
					return;

				var gradientBrush = (LinearGradientBrush)brush;

				var ratio = (panel.Orientation == Orientation.Vertical) ?
					panel.Width / item.Height :
					item.Width / panel.Height;
				
				var displacement = (ratio - 1) * 0.5;
				
				gradientBrush.StartPoint = new Point(gradientBrush.StartPoint.X, ratio * gradientBrush.StartPoint.Y - displacement);
				gradientBrush.EndPoint = new Point(gradientBrush.EndPoint.X, ratio * gradientBrush.EndPoint.Y - displacement);
			}
		}
	}
}
