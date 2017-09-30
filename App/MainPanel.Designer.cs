using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using static Taction.Config;

namespace Taction {

	partial class MainPanel {

		internal static class Designer {

			public static void GenerateLayout(MainPanel window) {

				var config = ((App)App.Current).config;

				var layoutData = config.layout;
				var stateData = config.state;

				// Prepare intent
				var design = new Design();
				design.panel.Orientation = config.layout.orientation;
				ProcessLayout(layoutData.items, design, window);

				// Setup
				window.buttonCommands = design.buttonCommands;

				// Make changes
				if (layoutData.orientation == Orientation.Vertical) {

					window.Width = layoutData.size;
					window.SizeToContent = SizeToContent.Height;

				} else {

					window.Height = layoutData.size;
					window.SizeToContent = SizeToContent.Width;
				}

				window.Opacity = layoutData.opacity;
				window.panel.Children.Add(design.panel);

				// Set position
				window.Left = stateData.x;
				window.Top = stateData.y;
			}

			private static void ProcessLayout(List<IPanelItemSpecs> specs, Design design, MainPanel window, StackPanel currentPanel = null) {

				if (specs == null)
					return;

				if (currentPanel == null)
					currentPanel = design.panel;

				foreach (var info in specs) {

					//var type = typeof(IPanelItemSpecs);
					//var types = AppDomain.CurrentDomain.GetAssemblies()
					//	.SelectMany(s => s.GetTypes())
					//	.Where(p => p.IsClass && type.IsAssignableFrom(p));

					var type = info.GetType();

					// SPECIAL CASE
					// @NOTE No direct link from button and panel
					if (type == typeof(PivotSpecs)) {

						var spec = (PivotSpecs)info;

						var newPanel = new StackPanel {
							Orientation = currentPanel.Orientation == Orientation.Horizontal ?
								Orientation.Vertical :
								Orientation.Horizontal
						};

						if (newPanel.Orientation != currentPanel.Orientation) {
							if (currentPanel.Orientation == Orientation.Vertical)
								newPanel.Height = spec.size;
							else
								newPanel.Width = spec.size;
						}

						currentPanel.Children.Add(newPanel);

						ProcessLayout(spec.items, design, window, newPanel);
						return;
					}

					// Generic handling; Expect content control
					var attr = (JsonPanelItemSpecsClassAttribute)type.GetCustomAttributes(typeof(JsonPanelItemSpecsClassAttribute), true)[0];
					var item = (ContentControl)Activator.CreateInstance(attr.value, info, currentPanel);

					currentPanel.Children.Add(item);
				}
			}

			public class Design {

				public Dictionary<ButtonBase, KeyCommand> buttonCommands;
				public StackPanel panel;

				public Design() {

					buttonCommands = new Dictionary<ButtonBase, KeyCommand>();
					panel = new StackPanel();
				}
			}
		}
	}
}
