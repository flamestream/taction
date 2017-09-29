using System;
using System.Windows;
using System.Windows.Interop;

namespace Taction {

	/// <summary>
	/// A helper to react to window messages not provided by WPF.
	/// </summary>
	internal class WindowEventNotifier {

		public event EventHandler OnExitSizeMove;

		public WindowEventNotifier(Window window) {

			var handle = new WindowInteropHelper(window).EnsureHandle();
			var source = HwndSource.FromHwnd(handle);
			source.AddHook(HandleMessage);
		}

		private IntPtr HandleMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {

			switch ((WinApi.WM)msg) {

				case WinApi.WM.WM_EXITSIZEMOVE:
					if (OnExitSizeMove != null)
						OnExitSizeMove.Invoke(this, EventArgs.Empty);
					break;
			}

			return IntPtr.Zero;
		}
	}
}
