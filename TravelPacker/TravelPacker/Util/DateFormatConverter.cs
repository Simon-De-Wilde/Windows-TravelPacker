using System;
using Windows.UI.Xaml.Data;

namespace TravelPacker.Util {
	public class DateFormatConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, string language) {
			if (value == null) {
				return null;
			}

			DateTime dt = DateTime.Parse(value.ToString());
			return dt.ToString("dd/MM/yyyy HH:mm");
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) {
			throw new NotImplementedException();
		}
	}
}
