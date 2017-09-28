using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Taction {

	internal class MoverButton : Button {

		public MoverButton() {

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
