using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using AlarmClockApp.MVVM.Abstract;
using AlarmClockApp.MVVM.Model;

namespace AlarmClockApp.MVVM.Converter
{
    class NotifyToBoolConverter : ConverterBase<NotifyToBoolConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch ((Alarm.NotifyType)value)
			{
				case Alarm.NotifyType.simpleNotify:
					return true;
				case Alarm.NotifyType.popupWindow:
					return false;
			}

			return false;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//throw new NotImplementedException();
			if (value is bool)
			{
				if ((bool)value)
					return Alarm.NotifyType.simpleNotify;
				else
					return Alarm.NotifyType.popupWindow;
			}

			return Alarm.NotifyType.popupWindow;
		}
	}
}
