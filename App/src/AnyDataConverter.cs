using System;
using System.Globalization;
using System.Windows.Data;

namespace Taction {

	public class AnyDataConverter : IMultiValueConverter {

		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

			foreach (var v in values) {

				if (v == null)
					continue;

				if (v is bool)
					if ((bool)v)
						return true;
					else
						continue;

				// Any non-null non-numeric object is considered true
				double num;
				if (!double.TryParse(v.ToString(), out num))
					return true;

				// Non-zero number is considered truthy
				if (num != 0)
					return true;
			}

			return false;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {

			throw new NotImplementedException();
		}
	}
}
