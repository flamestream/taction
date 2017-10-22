using System;
using System.Windows;
using WpfScreenHelper;

namespace Taction {

	internal static class WindowManipulator {

		public static void FitToNearestDesktop(Window window) {

			// Use current size
			var size = new Size {
				Width = window.ActualWidth,
				Height = window.ActualHeight
			};

			FitToNearestDesktop(window, size);
		}

		public static Rect PixelToPoint(Window window, Rect src, bool isAbsoluteWanted = false) {

			var originPx = new Point();
			var startPx = new Point(src.Left, src.Top);
			var endPx = new Point(src.Right, src.Bottom);

			var originPt = window.PointFromScreen(originPx);
			var startPt = window.PointFromScreen(startPx);
			var endPt = window.PointFromScreen(endPx);

			// Adjust to absolute
			if (isAbsoluteWanted) {

				startPt = (Point)Point.Subtract(startPt, originPt);
				endPt = (Point)Point.Subtract(endPt, originPt);
			}

			return new Rect(startPt, endPt);
		}

		public static void FitToNearestDesktop(Window window, Size newSize) {

			if (!window.IsVisible)
				return;

			var screen = Screen.FromHandle(WinApi.GetHandle(window));
			var desktopBounds = PixelToPoint(window, screen.WorkingArea, true);

			var dWidth = desktopBounds.Width;
			var dHeight = desktopBounds.Height;
			var dTop = desktopBounds.Top;
			var dLeft = desktopBounds.Left;
			var dRight = desktopBounds.Right;
			var dBottom = desktopBounds.Bottom;

			var wWidth = newSize.Width;
			var wHeight = newSize.Height;
			var wTop = window.Top;
			var wLeft = window.Left;
			var wRight = wLeft + wWidth;
			var wBottom = wTop + wHeight;

			// Just in case the window is bigger than the desktop
			var excessWidth = Math.Max(0, wWidth - dWidth);
			var excessHeight = Math.Max(0, wHeight - dHeight);

			// Adjust X
			if (wLeft < dLeft - excessWidth) {
				window.Left = dLeft;
			} else if (wRight > dRight + excessWidth) {
				window.Left = dRight - wWidth;
			}

			// Adjust Y
			if (wTop < dTop - excessHeight) {
				window.Top = dTop;
			} else if (wBottom > dBottom + excessHeight) {
				window.Top = dBottom - wHeight;
			}
		}
	}
}
