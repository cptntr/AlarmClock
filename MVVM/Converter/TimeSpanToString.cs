using AlarmClockApp.MVVM.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AlarmClockApp.MVVM.Converter
{
    class TimeSpanToString : ConverterBase<TimeSpanToString>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{


			return ((TimeSpan)value).ToString();
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//throw new NotImplementedException();
			DateTime dt = (DateTime)value;
			TimeSpan ret = new TimeSpan(dt.Hour, dt.Minute, dt.Second);

			return ret;
		}
	}
}