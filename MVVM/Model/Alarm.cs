using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AlarmClockApp.MVVM.Abstract;
using Microsoft.Win32.TaskScheduler;

namespace AlarmClockApp.MVVM.Model
{
    public class Alarm : ViewModelBase
    {
        #region Members
        #region Main
        private DateTime _startDate;
        private DateTime _startTime;
        private bool _syncTimeZones;
        private string _description;
        #endregion
        #region Repeat
        private bool _computerStarts;
        private bool _logOn;
        public enum ReiterationType { oneTime, daily, weekly, monthly };
        private ReiterationType _reiteration;
        private int _recurDays;
        private int _recurWeeks;
        private DaysOfTheWeek _daysOfTheWeek;
        #endregion
        #region Advanced
        private bool _randomDelay;
        private TimeSpan _randomDelayTime;
        private bool _repeatAlarm;
        private TimeSpan _repeatAlarmTime;
        private bool _repeatAlarmDuration;
        private TimeSpan _repeatAlarmDurationTime;
        private bool _stopRepeat;
        #endregion
        #region Notify
        public enum NotifyType { simpleNotify, popupWindow };
        private NotifyType _notify;
        public enum PriorityType { normal, high };
        private PriorityType _priority;
        #endregion
        #region Conditions
        private bool _runAsap;
        private bool _deleteAfter;
        private int _deleteAfterDays;
        private bool _wake;
        #endregion
        #region Finaly
        private bool _enabled;
        #endregion
        #endregion
        #region Properties
        #region Main
        public DateTime StartDate { get => _startDate; set => OnPropertyChanged(ref _startDate, value); }
        public DateTime StartTime { get => _startTime; set => OnPropertyChanged(ref _startTime, value); }
        public bool SyncTimeZones { get => _syncTimeZones; set => OnPropertyChanged(ref _syncTimeZones, value); }
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }
        #endregion
        #region Repeat
        public bool ComputerStarts { get => _computerStarts; set => OnPropertyChanged(ref _computerStarts, value); }
        public bool LogOn { get => _logOn; set => OnPropertyChanged(ref _logOn, value); }
        public ReiterationType Reiteration { get => _reiteration; set => OnPropertyChanged(ref _reiteration, value); }
        public int RecurDays { get => _recurDays; set => OnPropertyChanged(ref _recurDays, value); }
        public int RecurWeeks { get => _recurWeeks; set => OnPropertyChanged(ref _recurWeeks, value); }
        public DaysOfTheWeek DaysOfTheWeek { get => _daysOfTheWeek; set => OnPropertyChanged(ref _daysOfTheWeek, value); }
        #endregion
        #region Advanced
        public bool RandomDelay { get => _randomDelay; set => OnPropertyChanged(ref _randomDelay, value); }
        public TimeSpan RandomDelayTime { get => _randomDelayTime; set => OnPropertyChanged(ref _randomDelayTime, value); }
        public bool RepeatAlarm { get => _repeatAlarm; set => OnPropertyChanged(ref _repeatAlarm, value); }
        public TimeSpan RepeatAlarmTime { get => _repeatAlarmTime; set => OnPropertyChanged(ref _repeatAlarmTime, value); }
        public bool RepeatAlarmDuration { get => _repeatAlarmDuration; set => OnPropertyChanged(ref _repeatAlarmDuration, value); }
        public TimeSpan RepeatAlarmDurationTime { get => _repeatAlarmDurationTime; set => OnPropertyChanged(ref _repeatAlarmDurationTime, value); }
        public bool StopRepeat { get => _stopRepeat; set => OnPropertyChanged(ref _stopRepeat, value); }
        #endregion
        #region Notify
        public NotifyType Notify { get => _notify; set => OnPropertyChanged(ref _notify, value); }
        public PriorityType Priority { get => _priority; set => OnPropertyChanged(ref _priority, value); }
        #endregion
        #region Conditions
        public bool RunAsap { get => _runAsap; set => OnPropertyChanged(ref _runAsap, value); }
        public bool DeleteAfter { get => _deleteAfter; set => OnPropertyChanged(ref _deleteAfter, value); }
        public int DeleteAfterDays { get => _deleteAfterDays; set => OnPropertyChanged(ref _deleteAfterDays, value); }
        public bool Wake { get => _wake; set => OnPropertyChanged(ref _wake, value); }
        #endregion
        #region Finaly
        public bool Enabled { get => _enabled; set => OnPropertyChanged(ref _enabled, value); }
        #endregion
        #endregion
        #region INotifyPropertyChanged Members - moved to class ViewModelBase
        /*        public event PropertyChangedEventHandler PropertyChanged;
                public void OnPropertyChanged([CallerMemberName] string prop = "")
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
                }*/
        #endregion
    }
}
