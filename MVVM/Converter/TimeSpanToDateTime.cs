using AlarmClockApp.MVVM.Abstract;
using AlarmClockApp.MVVM.Model;
using System;
using System.Globalization;

namespace AlarmClockApp.MVVM.Converter
{
    class TimeSpanToDateTime : ConverterBase<TimeSpanToDateTime>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			

			return new DateTime() + (TimeSpan)value;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//throw new NotImplementedException();
			DateTime dt = (DateTime)value;
			TimeSpan ret = new TimeSpan(dt.Hour,dt.Minute,dt.Second);

			return ret;
		}
	}
}
