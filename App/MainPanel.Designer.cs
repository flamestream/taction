using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Taction.Attribute;

namespace Taction {

	partial class MainPanel {

		internal static class Designer {

			public static void GenerateLayout(MainPanel window) {

				var config = ((App)App.Current).Config;

				var layoutData = config.Layout;
				var stateData = config.State;

				// Prepare intent
				var panel = new StackPanel {
					Orientation = config.Layout.orientation
				};
				ProcessLayout(layoutData.items, panel);

				// Make changes
				if (layoutData.orientation == Orientation.Vertical) {

					window.Width = layoutData.size;
					window.SizeToContent = SizeToContent.Height;

				} else {

					window.Height = layoutData.size;
					window.SizeToContent = SizeToContent.Width;
				}

				window.Opacity = layoutData.opacity;
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
						item.Height = specs.size;
					else
						item.Width = specs.size;

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
						ProcessLayout(((PivotSpecs)specs).items, childPanel);
					}
				}
			}

			private static void ApplyStyle(System.Windows.Controls.ContentControl item, IButtonSpecs specs, StackPanel panel) {

				// Set Text
				if (specs.text != null) {

					item.Content = specs.text.value;

					if (specs.text.color != null)
						item.Foreground = specs.text.color;

				}

				// Set background
				if (specs.color != null)
					item.Background = specs.color;

				// Set border
				if (specs.border != null) {

					if (specs.border.thickness != null)
						item.BorderThickness = specs.border.thickness;

					if (specs.border.color != null)
						item.BorderBrush = specs.border.color;
				}
			}
		}
	}
}
