using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Taction.UIElement {

	public partial class NotificationToast : UserControl {

		// Delay used when user mouseover the ballon to close,
		// which is different from the close delay used when the
		// toast has first appeared (Set from TaskBarIcon.ShowCustomBalloon)
		public int SecondaryCloseDelayTime { get; set; }

		public event EventHandler Click;

		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
			"Title", typeof(string), typeof(NotificationToast)
		);

		public static readonly DependencyProperty BodyProperty = DependencyProperty.Register(
			"Body", typeof(string), typeof(NotificationToast)
		);

		// Shorthands
		public TaskbarIcon ParentTaskbarIcon => TaskbarIcon.GetParentTaskbarIcon(this);

		public string Title {
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public string Body {
			get { return (string)GetValue(BodyProperty); }
			set { SetValue(BodyProperty, value); }
		}

		private bool isClosing;
		private DateTime CloseTime;

		public NotificationToast() {

			InitializeComponent();

			// Initialize icon
			var icon = Taction.Properties.Resources.Icon;
			var src = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			IconImage.Source = src;

			TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
		}

		protected void ScheduleClose(int closeDelay = 0) {

			Dispatcher.Invoke(new Action(async () => {

				// Declare intent
				CloseTime = DateTime.Now.AddMilliseconds(closeDelay);
				await Task.Delay(closeDelay);

				// Override check
				if (CloseTime == null || DateTime.Now < CloseTime) return;

				ParentTaskbarIcon.CloseBalloon();

			}), DispatcherPriority.ContextIdle);
		}

		protected override void OnMouseDown(MouseButtonEventArgs e) {

			base.OnMouseDown(e);

			ParentTaskbarIcon.CloseBalloon();

			if (Click != null && e.LeftButton == MouseButtonState.Pressed)
				Click.Invoke(this, EventArgs.Empty);
		}

		protected override void OnMouseEnter(MouseEventArgs e) {

			base.OnMouseEnter(e);

			if (isClosing) return;

			// Prevent closing
			CloseTime = CloseTime.AddMilliseconds(SecondaryCloseDelayTime);
			ParentTaskbarIcon.ResetBalloonCloseTimer();
		}

		protected override void OnMouseLeave(MouseEventArgs e) {

			base.OnMouseLeave(e);

			// Start back close timer
			ScheduleClose(SecondaryCloseDelayTime);
		}

		private void OnBalloonClosing(object sender, RoutedEventArgs e) {

			e.Handled = true; //suppresses the popup from being closed immediately
			isClosing = true;
		}

		private void OnFadeOutCompleted(object sender, EventArgs e) {
			Popup pp = (Popup)Parent;
			pp.IsOpen = false;
		}
	}
}
