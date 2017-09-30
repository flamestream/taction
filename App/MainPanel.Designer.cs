using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Taction.CustomAttribute;
using static Taction.Config;

namespace Taction {

	partial class MainPanel {

		internal static class Designer {

			public static void GenerateLayout(MainPanel window) {

				var config = ((App)App.Current).config;

				var layoutData = config.layout;
				var stateData = config.state;

				// Prepare intent
				var panel = new StackPanel {
					Orientation = config.layout.orientation
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
					var itemType = attr.value;
					var item = (UIElement)Activator.CreateInstance(itemType, specs, currentPanel);

					currentPanel.Children.Add(item);

					// Special when panel
					if (item is StackPanel)
						ProcessLayout(((PivotSpecs)specs).items, (StackPanel)item);
				}
			}
		}
	}
}
