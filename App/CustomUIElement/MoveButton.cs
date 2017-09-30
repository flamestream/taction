using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Taction.Config;

namespace Taction.CustomUIElement {

	internal class MoveButton : Button {

		public MoveButton(IPanelItemSpecs specs, StackPanel panel = null) {

			var s = (MoveButtonSpecs)specs;

			this.Content = s.text;

			if (panel == null || panel.Orientation == Orientation.Vertical)
				this.Height = s.size;
			else
				this.Width = s.size;

			Style style = new Style();

			// Display move cursor on mouse over
			{
				Setter setter = new Setter {
					Property = CursorProperty,
					Value = Cursors.SizeAll
				};

				Trigger trigger = new Trigger {
					Property = IsMouseOverProperty,
					Value = true
				};
				trigger.Setters.Add(setter);

				style.Triggers.Add(trigger);
			}

			Style = style;
		}

		protected override void OnMouseDown(MouseButtonEventArgs e) {

			base.OnMouseDown(e);

			if (e.ChangedButton != MouseButton.Left)
				return;

			Window.GetWindow(this).DragMove();

			e.Handled = true;
		}

		protected override void OnMouseMove(MouseEventArgs e) {

			base.OnMouseMove(e);

			// Prevent hide due to mouse/pen cursor
			e.Handled = true;
		}
	}
}
