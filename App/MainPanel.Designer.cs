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

				// Prepare intent
				var panel = new StackPanel {
					Orientation = config.Layout.Orientation
				};
				
				// Make changes
				if (layoutData.Orientation == Orientation.Vertical) {

					window.Width = panel.Width = layoutData.Size;
					window.SizeToContent = SizeToContent.Height;
					
					if (layoutData.Border != null && layoutData.Border.Thickness != null)
						window.Width += layoutData.Border.Thickness.Left + layoutData.Border.Thickness.Right;

				} else {

					window.Height = panel.Height = layoutData.Size;
					window.SizeToContent = SizeToContent.Width;

					if (layoutData.Border != null && layoutData.Border.Thickness != null)
						window.Height += layoutData.Border.Thickness.Top + layoutData.Border.Thickness.Bottom;
				}

				window.Opacity = layoutData.Opacity;
				window.Container.Children.Add(panel);

				if (layoutData.Border != null) {

					if (layoutData.Border.Thickness != null)
						window.BorderThickness = layoutData.Border.Thickness;

					if (layoutData.Border.Color != null)
						window.BorderBrush = layoutData.Border.Color;
				}

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

						var origin = item.TranslatePoint(new Point(0.0, 0.0), null);
						
						var bounds = new Rect(origin.X, origin.Y, width, height);
						App.Instance.OutBoundaries.Add(bounds);
					}
				}
			}

			private static void ApplyStyle(ContentControl item, IButtonSpecs specs, StackPanel panel) {

				// Set Text
				if (specs.Text != null) {

					item.Content = specs.Text.Value;

					if (specs.Text.Color != null)
						item.Foreground = specs.Text.Color;

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
