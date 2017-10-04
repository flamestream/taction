using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Taction {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainPanel : Window {

		private App App => (App)Application.Current;
		private Config Config => App.Config;

		private bool IsPassthrough { get; set; }
		private WindowEventNotifier WindowEventMessenger { get; set; }

		public MainPanel() {

			InitializeComponent();

			WindowEventMessenger = new WindowEventNotifier(this);

			// Add event handlers
			SizeChanged += HandleSizeChanged;
			App.GlobalMouseHook.OnMouseLeaveBoundaries += HandleMouseLeaveBoundaries;
			WindowEventMessenger.OnExitSizeMove += HandleExitSizeMove;

			ReloadLayout();
		}

		public void ReloadLayout() {

			ClearLayout();
			Designer.GenerateLayout(this);
			WindowManipulator.FitToNearestDesktop(this);
		}

		private void ClearLayout() {

			container.Children.Clear();
			SetPassthrough(false);
			Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Enable or disable pass-through using config values for opacity.
		/// </summary>
		/// <param name="isWanted">Flag indicating if pass-through is wanted</param>
		private void SetPassthrough(bool isWanted) {

			float opacity = isWanted ?
				Config.Layout.opacityHide :
				Config.Layout.opacity;

			SetPassthrough(isWanted, opacity);
		}

		/// <summary>
		/// Enable or disable pass-though.
		/// </summary>
		/// <param name="isWanted">Flag indicating if pass-through is wanted</param>
		/// <param name="opacity">Opacity value to set</param>
		private void SetPassthrough(bool isWanted, float opacity) {

			if (isWanted && !IsPassthrough) {

				WinApi.SetWindowExTransparent(this, true);
				IsPassthrough = true;
				App.GlobalMouseHook.Enable();

				if (!Config.Layout.disableFadeAnimation)
					PlayFadeAnimation(opacity, Config.Layout.opacity);
				else if (opacity == 0)
					Visibility = Visibility.Hidden;
				else
					Opacity = opacity;

			} else if (!isWanted && IsPassthrough) {

				Visibility = Visibility.Visible;

				WinApi.SetWindowExTransparent(this, false);
				IsPassthrough = false;

				if (!Config.Layout.disableFadeAnimation)
					PlayFadeAnimation(opacity, Config.Layout.opacityHide);
				else
					Opacity = opacity;
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
			var duration = Config.Layout.fadeAnimationTime
				- Math.Abs(Opacity - plannedInitialOpacity)
				/ Math.Abs(plannedInitialOpacity - targetOpacity)
				* Config.Layout.fadeAnimationTime;

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

			App.GlobalMouseHook.Disable();
			SetPassthrough(false);
		}

		private void HandleExitSizeMove(object sender, EventArgs e) {

			WindowManipulator.FitToNearestDesktop(this);
			Config.State.x = Left;
			Config.State.y = Top;
			Config.Save();
			Debug.WriteLine(string.Format("{0}, {1}", Left, Top));
		}

		private void HandleSizeChanged(object sender, SizeChangedEventArgs e) {

			WindowManipulator.FitToNearestDesktop(this, e.NewSize);
		}

		protected override void OnActivated(EventArgs e) {

			base.OnActivated(e);
			WinApi.CancelActivation(this);
		}

		private void Window_MouseMove(object sender, MouseEventArgs e) {

			// Hide config check
			if (Config.Layout.disableHide)
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

			if (!Config.Layout.disableHide) {

				Debug.WriteLine("Hide Panel (pen)");
				SetPassthrough(true);
			}
		}
	}
}
