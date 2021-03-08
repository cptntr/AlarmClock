using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using AlarmClockApp.MVVM.Model;

namespace AlarmClockApp.MVVM.Converter
{
    class AlarmPriorityToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter,
			System.Globalization.CultureInfo culture)
		{
            switch ((Alarm.PriorityType)value)
            {
				case Alarm.PriorityType.high:
					return true;
				case Alarm.PriorityType.normal:
					return false;
            }

			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
			System.Globalization.CultureInfo culture)
		{
			//throw new NotImplementedException();
			if(value is bool)
            {
				if ((bool)value)
					return Alarm.PriorityType.high;
				else
					return Alarm.PriorityType.normal;
            }

			return Alarm.NotifyType.popupWindow;
		}
	}
}
