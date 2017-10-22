using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace Taction {

	partial class Designer {

		public static BitmapImage ColorizeImage(BitmapImage src, System.Windows.Media.Color shade) {

			return Bitmap2BitmapImage(ColorShade(BitmapImage2Bitmap(src), shade));
		}

		// From https://softwarebydefault.com/2013/04/13/bitmap-color-shading/
		public static Bitmap ColorShade(this Bitmap sourceBitmap, System.Windows.Media.Color shade) {

			BitmapData sourceData = sourceBitmap.LockBits(
				new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb
			);

			byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

			Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

			sourceBitmap.UnlockBits(sourceData);

			var redShade = shade.R;
			var greenShade = shade.G;
			var blueShade = shade.B;

			byte blue = 0;
			byte green = 0;
			byte red = 0;

			for (int k = 0; k + 4 < pixelBuffer.Length; k += 4) {

				blue = Math.Min(pixelBuffer[k], blueShade);
				green = Math.Min(pixelBuffer[k + 1], greenShade);
				red = Math.Min(pixelBuffer[k + 2], redShade);

				if (blue < 0) { blue = 0; }

				if (green < 0) { green = 0; }

				if (red < 0) { red = 0; }

				pixelBuffer[k] = (byte)blue;
				pixelBuffer[k + 1] = (byte)green;
				pixelBuffer[k + 2] = (byte)red;
			}

			Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

			BitmapData resultData = resultBitmap.LockBits(
				new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
				ImageLockMode.WriteOnly,
				PixelFormat.Format32bppArgb
			);

			Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
			resultBitmap.UnlockBits(resultData);

			return resultBitmap;
		}

		public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage) {

			return new Bitmap(bitmapImage.StreamSource);
		}

		public static BitmapImage Bitmap2BitmapImage(this Bitmap bitmap) {

			using (var memory = new MemoryStream()) {

				bitmap.Save(memory, ImageFormat.Png);
				memory.Position = 0;

				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				bitmapImage.Freeze();

				return bitmapImage;
			}
		}
	}
}
