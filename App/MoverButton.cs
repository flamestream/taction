using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArtTouchPanel {

	internal class MoverButton : Button {

		public MoverButton() {

			Style style = new Style();

			// Display move cursor on mouse over
			{
				Setter setter = new Setter();
				setter.Property = CursorProperty;
				setter.Value = Cursors.SizeAll;

				Trigger trigger = new Trigger();
				trigger.Property = UIElement.IsMouseOverProperty;
				trigger.Value = true;
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
		}
	}
}
