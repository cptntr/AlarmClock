using AlarmClockApp.MVVM.Abstract;
using AlarmClockApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AlarmClockApp.MVVM.Converter
{
    class SelectedIndexToReiteration : ConverterBase<SelectedIndexToReiteration>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            switch ((Alarm.ReiterationType)value)
            {
				case Alarm.ReiterationType.oneTime:
					return 0;
				case Alarm.ReiterationType.daily:
					return 1;
				case Alarm.ReiterationType.weekly:
					return 2;
            }

			return -1;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
            //throw new NotImplementedException();
            switch (value)
            {
				case 0:
					return Alarm.ReiterationType.oneTime;
				case 1:
					return Alarm.ReiterationType.daily;
				case 2:
					return Alarm.ReiterationType.weekly;
			}

			return Alarm.ReiterationType.oneTime;
		}
	}
}
