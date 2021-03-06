﻿using System.Windows;
using System.Windows.Input;

namespace Taction.UIElement {

	internal class MoveButton : StyleButton {

		public MoveButton(MoveButtonSpecs specs) : base(specs.Style) {

			SetResourceReference(StyleProperty, typeof(StyleButton));

			// Special default text
			specs.Style.Base.Content = specs.Style.Base.Content ?? "☰☰";
			specs.Style.Active.Content = specs.Style.Active.Content ?? specs.Style.Base.Content;

			// Force move cursor
			{
				var setter = new Setter {
					Property = CursorProperty,
					Value = Cursors.SizeAll
				};

				// Style is locked, so copy to modify
				var s = new Style(GetType(), Style);
				s.Setters.Add(setter);
				Style = s;
			}
		}

		protected override void OnMouseDown(MouseButtonEventArgs e) {

			base.OnMouseDown(e);

			// Press event is never triggered, so manually trigger it
			IsPressed = true;

			if (e.ChangedButton != MouseButton.Left)
				return;

			e.Handled = true;

			var window = Window.GetWindow(this) as MainPanel;
			var lastMoved = window.LastMoved;
			window.DragMove();

			// DragMove is blocking, so this works
			IsPressed = false;

			// Collapse if not moved
			if (lastMoved == window.LastMoved)
				App.Instance.MainPanel.ToggleCollapse(this);
		}

		protected override void OnMouseMove(MouseEventArgs e) {

			base.OnMouseMove(e);

			// Prevent hide due to mouse/pen cursor
			e.Handled = true;
		}
	}
}
