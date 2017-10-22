using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Taction.UIElement {

	partial class CustomButton : Button {

		public Rect Boundaries { get; private set; }

		public CustomButton() {

			var res = new ResourceDictionary {
				Source = new Uri(@"pack://application:,,,/src/UIElement/Button.xaml")
			};
			Style = (Style)res["ButtonStyle"];
		}

		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {

			base.OnRenderSizeChanged(sizeInfo);

			UpdateBoundaries();

			Designer.AdjustGradientColor(this, Background);
			Designer.AdjustGradientColor(this, BorderBrush);
		}

		public void UpdateBoundaries() {

			Boundaries = GetBoundaries();
			Debug.Write(string.Format("Update boundaries {0}", Boundaries));
		}

		public Rect GetBoundaries() {

			var origin = TranslatePoint(new Point(), null);
			var bounds = new Rect(origin.X, origin.Y, ActualWidth, ActualHeight);

			return bounds;
		}
	}
}
