using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Taction.Attribute;

namespace Taction {

	partial class MainPanel {

		internal static class Designer {

			public static void GenerateLayout(MainPanel window) {

				var config = App.Instance.Config;

				var layoutData = config.Layout;
				var stateData = config.State;

				// Prepare intent
				var panel = new StackPanel {
					Orientation = config.Layout.Orientation
				};
				ProcessLayout(layoutData.Items, panel);

				// Make changes
				if (layoutData.Orientation == Orientation.Vertical) {

					window.Width = layoutData.Size;
					window.SizeToContent = SizeToContent.Height;

				} else {

					window.Height = layoutData.Size;
					window.SizeToContent = SizeToContent.Width;
				}

				window.Opacity = layoutData.Opacity;
				window.container.Children.Add(panel);

				// Set position
				window.Left = stateData.x;
				window.Top = stateData.y;
			}

			private static void ProcessLayout(List<IPanelItemSpecs> specsList, StackPanel currentPanel) {

				if (specsList == null)
					return;

				foreach (var specs in specsList) {

					var specsType = specs.GetType();
					var attr = (AssociatedClassAttribute)specsType.GetCustomAttributes(typeof(AssociatedClassAttribute), true)[0];
					var itemType = attr.Value;
					var item = (FrameworkElement)Activator.CreateInstance(itemType, specs);

					// Set size
					if (currentPanel == null || currentPanel.Orientation == Orientation.Vertical)
						item.Height = specs.Size;
					else
						item.Width = specs.Size;

					// Set button-exclusive properties
					if (specs is IButtonSpecs)
						ApplyStyle((System.Windows.Controls.ContentControl)item, (IButtonSpecs)specs, currentPanel);

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
					}
				}
			}

			private static void ApplyStyle(System.Windows.Controls.ContentControl item, IButtonSpecs specs, StackPanel panel) {

				// Set Text
				if (specs.Text != null) {

					item.Content = specs.Text.Value;

					if (specs.Text.Color != null)
						item.Foreground = specs.Text.Color;

				}

				// Set background
				if (specs.Color != null)
					item.Background = specs.Color;

				// Set border
				if (specs.Border != null) {

					if (specs.Border.Thickness != null)
						item.BorderThickness = specs.Border.Thickness;

					if (specs.Border.Color != null)
						item.BorderBrush = specs.Border.Color;
				}
			}
		}
	}
}
