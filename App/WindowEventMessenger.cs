using System;
using System.Windows;
using System.Windows.Interop;

namespace ArtTouchPanel {

	/// <summary>
	/// A helper to react to window messages not provided by WPF.
	/// </summary>
	internal class WindowEventMessenger {

		public event EventHandler OnExitSizeMove;

		public WindowEventMessenger(Window window) {

			var handle = new WindowInteropHelper(window).EnsureHandle();
			var source = HwndSource.FromHwnd(handle);
			source.AddHook(HandleMessage);
		}

		private IntPtr HandleMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {

			switch ((Win32.WM)msg) {

				case Win32.WM.WM_EXITSIZEMOVE:
					if (OnExitSizeMove != null)
						OnExitSizeMove.Invoke(this, EventArgs.Empty);
					break;
			}

			return IntPtr.Zero;
		}
	}
}
