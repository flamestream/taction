using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace Taction {

	internal class GlobalMouseHook : IDisposable {

		public enum EventSource {
			Mouse,
			Pen,
			Touch
		}

		public struct EventArgs {
			public EventSource source;
		}

		private bool _isInAppBoundaries;
		private IntPtr HookId = IntPtr.Zero;
		private WinApi.HookProc HookProcDelegate;

		public delegate void GlobalMouseEventHandler(object sender, EventArgs args);

		public event GlobalMouseEventHandler OnMouseLeaveBoundaries;

		public GlobalMouseHook() {

			// Ensure this is not garbage collected
			HookProcDelegate = HookCallback;
		}

		public void Enable() {

			// Already hooked check
			if (HookId != IntPtr.Zero)
				return;

			// Activation is assumed to be in boundary
			_isInAppBoundaries = true;

			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule) {
				HookId = WinApi.SetWindowsHookEx(WinApi.HookType.WH_MOUSE_LL, HookProcDelegate, WinApi.GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		public void Disable() {

			// Hooked check
			if (HookId == IntPtr.Zero)
				return;

			WinApi.UnhookWindowsHookEx(HookId);
			HookId = IntPtr.Zero;
		}

		private bool IsInAppBoundaries(Point screenCoords) {

			var appWindow = App.Instance.MainWindow;
			if (appWindow == null)
				return false;

			var appWidth = appWindow.Width;
			var appHeight = appWindow.Height;
			var appCoords = appWindow.PointFromScreen(screenCoords);
			var pointAppX = appCoords.X;
			var pointAppY = appCoords.Y;

			// App window boundary check
			if (pointAppX < 0 || pointAppY < 0 || pointAppX > appWidth || pointAppY > appHeight)
				return false;

			// Special out boundary checks
			if (App.Instance.MainPanel.IsMouseInMoveButton(appCoords))
				return false;

			return true;
		}

		/// <summary>
		/// Distinguish between touch, pen, or mouse pointer events.
		/// https://msdn.microsoft.com/en-us/library/windows/desktop/ms703320(v=vs.85).aspx
		/// </summary>
		/// <param name="hookData">Hook data to check</param>
		/// <returns>Identified mouse event source</returns>
		private static EventSource GetMouseEventSource(WinApi.MSLLHOOKSTRUCT hookData) {

			uint extra = (uint)hookData.dwExtraInfo;
			bool isTouchOrPen = ((extra & 0xFFFFFF00) == 0xFF515700);
			if (!isTouchOrPen)
				return EventSource.Mouse;

			bool isTouch = ((extra & 0x00000080) == 0x00000080);

			return isTouch ? EventSource.Touch : EventSource.Pen;
		}

		/// <summary>
		/// Intercepts mouse events, giving the option to change or swallow them.
		/// This is gives extremely bad experience if/when the app hangs (i.e. debugger break),
		/// as mouse events won't be processed, rendering mouse unusable until CallNextHookEx
		/// is called. Fortunately, the OS will fix this by probably unregistering
		/// the hook...
		/// @TODO Minimize usage or find anternative.
		/// </summary>
		/// <param name="code"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		private IntPtr HookCallback(int code, IntPtr wParam, IntPtr lParam) {

			if (OnMouseLeaveBoundaries != null && code >= 0 && WinApi.WM.WM_MOUSEMOVE == (WinApi.WM)wParam) {

				WinApi.MSLLHOOKSTRUCT hookStruct = (WinApi.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(WinApi.MSLLHOOKSTRUCT));
				Point coords = new Point(hookStruct.pt.x, hookStruct.pt.y);

				// Boundary check
				var isNowInAppBoundaries = IsInAppBoundaries(coords);
				if (_isInAppBoundaries && !isNowInAppBoundaries) {

					EventSource source = GetMouseEventSource(hookStruct);
					OnMouseLeaveBoundaries.Invoke(null, new EventArgs {
						source = source
					});
				}

				_isInAppBoundaries = isNowInAppBoundaries;
			}

			return WinApi.CallNextHookEx(HookId, code, wParam, lParam);
		}

		public void Dispose() {

			Disable();
		}
	}
}
