using System;
using System.Windows;
using WpfScreenHelper;

namespace Taction {

	internal static class WindowManipulator {

		public static void FitToNearestDesktop(Window window) {

			// Use current size
			var size = new Size {
				Width = window.Width,
				Height = window.Height
			};

			FitToNearestDesktop(window, size);
		}

		public static void FitToNearestDesktop(Window window, Size newSize) {

			var screen = Screen.FromHandle(WinApi.GetHandle(window));
			var desktop = screen.WorkingArea;

			var dWidth = desktop.Width;
			var dHeight = desktop.Height;
			var dTop = desktop.Top;
			var dLeft = desktop.Left;
			var dRight = desktop.Right;
			var dBottom = desktop.Bottom;

			var wWidth = newSize.Width;
			var wHeight = newSize.Height;
			var wTop = window.Top;
			var wLeft = window.Left;
			var wRight = wLeft + wWidth;
			var wBottom = wTop + wHeight;

			// Just in case the window is bigger than the desktop
			var excessWidth = Math.Max(0, wWidth - dWidth);
			var excessHeight = Math.Max(0, wHeight - dHeight);

			// Adjust Y
			if (wTop < dTop - excessHeight) {
				window.Top = dTop;
			} else if (wBottom > dBottom + excessHeight) {
				window.Top = dBottom - wHeight;
			}

			// Adjust X
			if (wLeft < dLeft - excessWidth) {
				window.Left = dLeft;
			} else if (wRight > dRight + excessWidth) {
				window.Left = dRight - wWidth;
			}
		}
	}
}
