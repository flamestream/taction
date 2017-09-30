using System.Collections.Generic;
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

					if (info is HoldButtonSpecs) {

						var spec = (HoldButtonSpecs)info;
						var newButton = new HoldButton {
							Content = spec.text != null ?
								spec.text :
								spec.keyCommand,
							KeyCommand = InputSimulatorHelper.ParseKeyCommand(spec.keyCommand)
						};

						if (currentPanel.Orientation == Orientation.Vertical)
							newButton.Height = spec.size;
						else
							newButton.Width = spec.size;

						currentPanel.Children.Add(newButton);

					} else if (info is TapButtonSpecs) {

						var spec = (TapButtonSpecs)info;
						var newButton = new TapButton {
							Content = spec.text != null ?
								spec.text :
								spec.keyCommand,
							KeyCommand = InputSimulatorHelper.ParseKeyCommand(spec.keyCommand)
						};

						if (currentPanel.Orientation == Orientation.Vertical)
							newButton.Height = spec.size;
						else
							newButton.Width = spec.size;

						currentPanel.Children.Add(newButton);

					} else if (info is ToggleButtonSpecs) {

						var spec = (ToggleButtonSpecs)info;
						var newButton = new ToggleButton {
							Content = spec.text != null ?
								spec.text :
								spec.keyCommand,
							KeyCommand = InputSimulatorHelper.ParseKeyCommand(spec.keyCommand)
						};

						if (currentPanel.Orientation == Orientation.Vertical)
							newButton.Height = spec.size;
						else
							newButton.Width = spec.size;

						currentPanel.Children.Add(newButton);

					} else if (info is PivotSpecs) {

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

					} else if (info is MoveButtonSpecs) {

						var spec = (MoveButtonSpecs)info;
						var newButton = new MoveButton {
							Content = spec.text
						};

						if (currentPanel.Orientation == Orientation.Vertical)
							newButton.Height = spec.size;
						else
							newButton.Width = spec.size;

						currentPanel.Children.Add(newButton);
					}
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
