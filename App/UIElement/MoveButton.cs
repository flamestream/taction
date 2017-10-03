using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Taction.Config;

namespace Taction.UIElement {

	internal class MoveButton : Button {

		public MoveButton(IPanelItemSpecs specs, System.Windows.Controls.StackPanel panel = null) {

			var s = (MoveButtonSpecs)specs;

			// Set Text
			if (s.text != null) {

				this.Content = s.text.value;

				if (s.text.color != null)
					this.Foreground = s.text.color;

			} else {

				this.Content = "==";
			}

			// Set background
			if (s.color != null)
				Background = s.color;

			// Set border
			if (s.border != null) {

				if (s.border.thickness != null)
					BorderThickness = s.border.thickness;

				if (s.border.color != null)
					BorderBrush = s.border.color;
			}

			// Set size
			if (panel == null || panel.Orientation == Orientation.Vertical)
				this.Height = s.size;
			else
				this.Width = s.size;

			// Set behaviours
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
