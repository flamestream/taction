using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using static ArtTouchPanel.Win32;

namespace ArtTouchPanel {

	internal class GlobalMouseHook : IDisposable {

		public enum EventSource {
			Mouse,
			Pen,
			Touch
		}

		public struct EventArgs {
			public EventSource source;
		}

		public delegate void GlobalMouseEventHandler(object sender, EventArgs args);

		private Window appWindow;
		private HookProc proc;
		private IntPtr hookId = IntPtr.Zero;
		private bool isInAppBoundaries;

		public event GlobalMouseEventHandler OnMouseLeaveBoundaries;

		public GlobalMouseHook(Window window) {

			appWindow = window;
			proc = HookCallback;
		}

		public void Enable() {

			// Already hooked check
			if (hookId != IntPtr.Zero)
				return;

			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule) {
				hookId = SetWindowsHookEx(HookType.WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		public void Disable() {

			// Hooked check
			if (hookId == IntPtr.Zero)
				return;

			UnhookWindowsHookEx(hookId);
			hookId = IntPtr.Zero;
		}

		private bool IsInAppBoundaries(Point screenCoords) {

			var appWidth = appWindow.Width;
			var appHeight = appWindow.Height;
			var appCoords = appWindow.PointFromScreen(screenCoords);
			var pointAppX = appCoords.X;
			var pointAppY = appCoords.Y;

			return !(pointAppX < 0 || pointAppY < 0 || pointAppX > appWidth || pointAppY > appHeight);
		}

		/// <summary>
		/// Distinguish between touch, pen, or mouse pointer events.
		/// https://msdn.microsoft.com/en-us/library/windows/desktop/ms703320(v=vs.85).aspx
		/// </summary>
		/// <param name="hookData">Hook data to check</param>
		/// <returns>Identified mouse event source</returns>
		private static EventSource GetMouseEventSource(MSLLHOOKSTRUCT hookData) {

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
		/// @TODO Minimize usage or find another way.
		/// </summary>
		/// <param name="code"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		private IntPtr HookCallback(int code, IntPtr wParam, IntPtr lParam) {

			if (OnMouseLeaveBoundaries != null && code >= 0 && WM.WM_MOUSEMOVE == (WM)wParam) {

				MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
				Point coords = new Point(hookStruct.pt.x, hookStruct.pt.y);

				// Boundary check
				var isNowInAppBoundaries = IsInAppBoundaries(coords);
				if (isInAppBoundaries && !isNowInAppBoundaries) {

					EventSource source = GetMouseEventSource(hookStruct);
					OnMouseLeaveBoundaries.Invoke(null, new EventArgs {
						source = source
					});
				}

				isInAppBoundaries = isNowInAppBoundaries;
			}

			return CallNextHookEx(hookId, code, wParam, lParam);
		}

		public void Dispose() {

			Disable();
		}
	}
}
