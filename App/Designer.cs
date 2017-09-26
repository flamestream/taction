using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static ArtTouchPanel.Config;

namespace ArtTouchPanel {

	partial class MainWindow {

		internal static class Designer {

			public static void GenerateLayout(MainWindow window) {

				var configData = window.config.data;

				// Prepare intent
				var design = new Design();
				design.panel.Orientation = window.config.data.orientation;
				ProcessLayout(configData.items, design, window);

				// Setup
				window.buttonCommands = design.buttonCommands;

				// Make changes
				if (configData.orientation == Orientation.Vertical) {

					window.Width = configData.size;
					window.SizeToContent = SizeToContent.Height;

				} else {

					window.Height = configData.size;
					window.SizeToContent = SizeToContent.Width;
				}

				window.Opacity = configData.opacity;
				window.panel.Children.Add(design.panel);
			}

			private static void ProcessLayout(List<IPanelItemSpecs> specs, Design design, MainWindow window, StackPanel currentPanel = null) {

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

					} else if (info is PanelSpecs) {

						var panelInfo = (PanelSpecs)info;

						var newPanel = new StackPanel {
							Orientation = panelInfo.orientation
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

						var buttonSpecs = (MoverSpecs)info;
						var newButton = new Button {
						};
						newButton.TouchDown += window.Mover_TouchDown;
						newButton.TouchUp += window.Mover_TouchUp;
						// newButton.TouchLeave += window.Mover_TouchUp;
						newButton.TouchMove += window.Mover_TouchMove;

						if (currentPanel.Orientation == Orientation.Vertical)
							newButton.Height = buttonSpecs.size;
						else
							newButton.Width = buttonSpecs.size;

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
