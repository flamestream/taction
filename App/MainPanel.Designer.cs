using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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

					if (info is ButtonSpecs) {

						var buttonSpecs = (ButtonSpecs)info;
						var newButton = new Button {
							Content = buttonSpecs.text != null ?
								buttonSpecs.text :
								buttonSpecs.keyCommand
						};
						newButton.TouchDown += window.Button_TouchDown;
						newButton.TouchUp += window.Button_TouchUp;

						if (currentPanel.Orientation == Orientation.Vertical)
							newButton.Height = buttonSpecs.size;
						else
							newButton.Width = buttonSpecs.size;

						currentPanel.Children.Add(newButton);
						design.buttonCommands.Add(newButton, InputSimulatorHelper.ParseKeyCommand(buttonSpecs.keyCommand));

					} else if (info is PivotSpecs) {

						var panelInfo = (PivotSpecs)info;

						var newPanel = new StackPanel {
							Orientation = currentPanel.Orientation == Orientation.Horizontal ?
								Orientation.Vertical :
								Orientation.Horizontal
						};

						if (newPanel.Orientation != currentPanel.Orientation) {
							if (currentPanel.Orientation == Orientation.Vertical)
								newPanel.Height = panelInfo.size;
							else
								newPanel.Width = panelInfo.size;
						}

						currentPanel.Children.Add(newPanel);

						ProcessLayout(panelInfo.items, design, window, newPanel);

					} else if (info is MoverSpecs) {

						var moverSpecs = (MoverSpecs)info;
						var newButton = new MoverButton {
							Content = moverSpecs.text
						};

						if (currentPanel.Orientation == Orientation.Vertical)
							newButton.Height = moverSpecs.size;
						else
							newButton.Width = moverSpecs.size;

						currentPanel.Children.Add(newButton);
					}
				}
			}

			public class Design {

				public Dictionary<Button, KeyCommand> buttonCommands;
				public StackPanel panel;

				public Design() {

					buttonCommands = new Dictionary<Button, KeyCommand>();
					panel = new StackPanel();
				}
			}
		}
	}
}
