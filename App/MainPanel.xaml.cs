using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Taction {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainPanel : Window {

		private InputSimulatorHelper inputSimulator { get; set; }
		private GlobalMouseHook globalMouseHook { get; set; }
		private bool isPassthrough { get; set; }
		private Dictionary<Button, KeyCommand> buttonCommands { get; set; }
		private WindowEventMessenger windowEventMessenger;

		private Config config {
			get {
				return ((App)Application.Current).config;
			}
		}

		public MainPanel() {

			InitializeComponent();
			inputSimulator = new InputSimulatorHelper();
			buttonCommands = new Dictionary<Button, KeyCommand>();

			globalMouseHook = new GlobalMouseHook(this);
			globalMouseHook.OnMouseLeaveBoundaries += HandleMouseLeaveBoundaries;

			windowEventMessenger = new WindowEventMessenger(this);
			windowEventMessenger.OnExitSizeMove += HandleExitSizeMove;

			ReloadLayout();

			Debug.WriteLine(string.Format("Position({0}, {1})", this.Left, this.Top));
			Debug.WriteLine(string.Format("Size: {0}, {1}", this.Width, this.Height));
		}

		private void ResetLayout() {

			globalMouseHook.Disable();
			SetPassthrough(false);
			panel.Children.Clear();
			buttonCommands.Clear();
		}

		private void ReloadLayout() {

			ResetLayout();

			if (config == null)
				return;

			// Set layout
			this.Visibility = Visibility.Visible;
			Designer.GenerateLayout(this);
		}

		/// <summary>
		/// Enable or disable pass-though using config values for opacity.
		/// </summary>
		/// <param name="isWanted">Flag indicating if pass-through is wanted</param>
		private void SetPassthrough(bool isWanted) {

			float opacity = isWanted ?
				config.layout.opacityHide :
				config.layout.opacity;

			SetPassthrough(isWanted, opacity);
		}

		/// <summary>
		/// Enable or disable pass-though.
		/// </summary>
		/// <param name="isWanted">Flag indicating if pass-through is wanted</param>
		/// <param name="opacity">Opacity value to set</param>
		private void SetPassthrough(bool isWanted, float opacity) {

			if (isWanted && !isPassthrough) {

				Win32.SetWindowExTransparent(this, true);
				isPassthrough = true;
				globalMouseHook.Enable();

				if (!config.layout.disableFadeAnimation)
					PlayFadeAnimation(opacity, config.layout.opacity);
				else if (opacity == 0)
					this.Visibility = Visibility.Hidden;
				else
					this.Opacity = opacity;

			} else if (!isWanted && isPassthrough) {

				this.Visibility = Visibility.Visible;

				Win32.SetWindowExTransparent(this, false);
				isPassthrough = false;

				if (!config.layout.disableFadeAnimation)
					PlayFadeAnimation(opacity, config.layout.opacityHide);
				else
					this.Opacity = opacity;
			}
		}

		/// <summary>
		/// Play panel fading animation.
		/// </summary>
		/// <param name="targetOpacity">Opacity end value.</param>
		/// <param name="plannedInitialOpacity">Opacity start value assuming no interruption.</param>
		private void PlayFadeAnimation(float targetOpacity, float plannedInitialOpacity) {

			// The animation may be interrupted and played in reverse.
			// Shorten based on interruption value.
			var duration = config.layout.fadeAnimationTime
				- Math.Abs(this.Opacity - plannedInitialOpacity)
				/ Math.Abs(plannedInitialOpacity - targetOpacity)
				* config.layout.fadeAnimationTime;

			var animation = new DoubleAnimation {
				To = targetOpacity,
				Duration = TimeSpan.FromMilliseconds(duration),
				EasingFunction = new QuinticEase()
			};

			var sb = new Storyboard();
			sb.Children.Add(animation);
			Storyboard.SetTarget(sb, this);
			Storyboard.SetTargetProperty(sb, new PropertyPath(OpacityProperty));

			sb.Begin();
		}

		private void HandleMouseLeaveBoundaries(object sender, GlobalMouseHook.EventArgs e) {

			globalMouseHook.Disable();
			SetPassthrough(false);
		}

		private void HandleExitSizeMove(object sender, EventArgs e) {

			WindowManipulator.FitToNearestDesktop(this);
			config.state.x = this.Left;
			config.state.y = this.Top;
			config.Save();
			Debug.WriteLine(string.Format("{0}, {1}", this.Left, this.Top));
		}

		protected override void OnClosing(CancelEventArgs e) {

			base.OnClosing(e);

			if (globalMouseHook != null)
				globalMouseHook.Dispose();
		}

		protected override void OnActivated(EventArgs e) {

			base.OnActivated(e);
			Win32.CancelActivation(this);
		}

		private void Button_TouchDown(object sender, TouchEventArgs e) {

			e.Handled = true;

			Button btn = (Button)sender;
			btn.FontWeight = FontWeight.FromOpenTypeWeight(500);

			if (!buttonCommands.TryGetValue(btn, out KeyCommand keyCommand))
				return;

			if (keyCommand.isPressWanted)
				inputSimulator.SimulateKeyPress(keyCommand.keyCodes);
			else
				inputSimulator.SimulateKeyDown(keyCommand.keyCodes);
		}

		private void Button_TouchUp(object sender, TouchEventArgs e) {

			e.Handled = true;

			Button btn = (Button)sender;
			btn.FontWeight = FontWeight.FromOpenTypeWeight(200);

			if (!buttonCommands.TryGetValue(btn, out KeyCommand keyCommand))
				return;

			if (!keyCommand.isPressWanted)
				inputSimulator.SimulateKeyUp(keyCommand.keyCodes);
		}

		private void Window_MouseMove(object sender, MouseEventArgs e) {

			// Hide config check
			if (config.layout.disableHide)
				return;

			// Touch/Pen promotion check
			if (e.StylusDevice != null)
				return;

			// When the window has stopped dragging, the position is reset to (0,0)
			// Until it goes back to where the cursor resulted to. Ignore that case.
			var p = e.GetPosition(this);
			if (p.X == 0 && p.Y == 0)
				return;

			Debug.WriteLine("Hide Panel (mouse)");
			SetPassthrough(true);
		}

		private void Window_PreviewStylusInAirMove(object sender, StylusEventArgs e) {

			// Prevent mouse move event
			e.Handled = true;

			if (!config.layout.disableHide) {

				Debug.WriteLine("Hide Panel (pen)");
				SetPassthrough(true);
			}
		}
	}
}
