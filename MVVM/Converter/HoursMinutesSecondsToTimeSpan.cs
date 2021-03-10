using AlarmClockApp.MVVM.Abstract;
using AlarmClockApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AlarmClockApp.MVVM.Converter
{
    class HoursMinutesSecondsToTimespan : ConverterBase<HoursMinutesSecondsToTimespan>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
/*			switch (parameter)
			{
				case "Hours":
					return ;
				case "Minutes":
					;
				case "Seconds":
					;
			}*/

			return false;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//throw new NotImplementedException();
/*			switch (parameter)
			{
				case "Hours":
					;
				case "Minutes":
					;
				case "Seconds":
					;
			}*/

			return Alarm.NotifyType.popupWindow;
		}
	}
}
