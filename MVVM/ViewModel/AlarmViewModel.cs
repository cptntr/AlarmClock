using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Win32.TaskScheduler;
using AlarmClockApp.MVVM.Model;
using AlarmClockApp.MVVM.Abstract;
using AlarmClockApp.Shared;
using System.Windows.Input;

namespace AlarmClockApp.MVVM.ViewModel
{
    public class AlarmViewModel : ViewModelBase
    {
        #region Members
        private Alarm _currentEditAlarm;
        private Alarm _defaultAlarm;

        private ObservableCollection<Alarm> _deployedAlarms;
        private ObservableCollection<Alarm> _alarmTemplates;
        #endregion
        #region Commands
        private ICommand _cmdToggleAllDays;
        public ICommand CmdToggleAllDays
        {
            get
            {
                return _cmdToggleAllDays ?? (_cmdToggleAllDays = new RelayCommand(x => ToggleAllDays()));
            }
        }
        private void ToggleAllDays()
        {
            _currentEditAlarm.DaysOfTheWeek = _currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.AllDays) ? ~DaysOfTheWeek.AllDays : DaysOfTheWeek.AllDays;
            OnPropertyChanged("Mon"); OnPropertyChanged("Tue"); OnPropertyChanged("Wed"); OnPropertyChanged("Thu"); OnPropertyChanged("Fri"); OnPropertyChanged("Sat"); OnPropertyChanged("Sun"); OnPropertyChanged("AllDaysSelected");
        }
        #endregion
        #region Properties
        public Alarm CurrentEditAlarm { get => _currentEditAlarm; set => OnPropertyChanged(ref _currentEditAlarm, value); }
        // We may want to load the default values from the file if one exists
        public Alarm DefaultAlarm { get => _defaultAlarm; set => OnPropertyChanged(ref _defaultAlarm, value); }
        #region Alarm properties
        #region Main
        public DateTime StartDate { get => _currentEditAlarm.StartDate; set { _currentEditAlarm.StartDate = value; OnPropertyChanged(); } }
        public DateTime StartTime { get => _currentEditAlarm.StartTime; set { _currentEditAlarm.StartTime = value; OnPropertyChanged(); } }
        public bool SyncTimeZones { get => _currentEditAlarm.SyncTimeZones; set { _currentEditAlarm.SyncTimeZones = value; OnPropertyChanged(); } }
        public string Description { get => _currentEditAlarm.Description; set { _currentEditAlarm.Description = value; OnPropertyChanged(); } }
        #endregion
        #region Repeat
        public bool ComputerStarts { get => _currentEditAlarm.ComputerStarts; set { _currentEditAlarm.ComputerStarts = value; OnPropertyChanged(); OnPropertyChanged("OnLogOption"); } }
        public bool LogOn { get => _currentEditAlarm.LogOn; set { _currentEditAlarm.LogOn = value; OnPropertyChanged(); OnPropertyChanged("OnLogOption"); } }
        public Alarm.ReiterationType Reiteration { get => _currentEditAlarm.Reiteration; set { _currentEditAlarm.Reiteration = value; OnPropertyChanged(); } }
        public int RecurDays { get => _currentEditAlarm.RecurDays; set { _currentEditAlarm.RecurDays = value; OnPropertyChanged(); } }
        public int RecurWeeks { get => _currentEditAlarm.RecurWeeks; set { _currentEditAlarm.RecurWeeks = value; OnPropertyChanged(); } }
        public bool Mon { get => _currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Monday); set { if (value) _currentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Monday; else _currentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Monday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Tue { get => _currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Tuesday); set { if (value) _currentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Tuesday; else _currentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Tuesday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Wed { get => _currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Wednesday); set { if (value) _currentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Wednesday; else _currentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Wednesday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Thu { get => _currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Thursday); set { if (value) _currentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Thursday; else _currentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Thursday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Fri { get => _currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Friday); set { if (value) _currentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Friday; else _currentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Friday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Sat { get => _currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Saturday); set { if (value) _currentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Saturday; else _currentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Saturday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Sun { get => _currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Sunday); set { if (value) _currentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Sunday; else _currentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Sunday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        #endregion
        #region Advanced
        public bool RandomDelay { get => _currentEditAlarm.RandomDelay; set { _currentEditAlarm.RandomDelay = value; OnPropertyChanged(); } }
        public TimeSpan RandomDelayTime { get => _currentEditAlarm.RandomDelayTime; set { _currentEditAlarm.RandomDelayTime = value; OnPropertyChanged(); } }
        public bool RepeatAlarm { get => _currentEditAlarm.RepeatAlarm; set { _currentEditAlarm.RepeatAlarm = value; OnPropertyChanged(); } }
        public TimeSpan RepeatAlarmTime { get => _currentEditAlarm.RepeatAlarmTime; set { _currentEditAlarm.RepeatAlarmTime = value; OnPropertyChanged(); } }
        public bool RepeatAlarmDuration { get => _currentEditAlarm.RepeatAlarmDuration; set { _currentEditAlarm.RepeatAlarmDuration = value; OnPropertyChanged(); } }
        public TimeSpan RepeatAlarmDurationTime { get => _currentEditAlarm.RepeatAlarmDurationTime; set { _currentEditAlarm.RepeatAlarmDurationTime = value; OnPropertyChanged(); } }
        public bool StopRepeat { get => _currentEditAlarm.StopRepeat; set { _currentEditAlarm.StopRepeat = value; OnPropertyChanged(); } }
        #endregion
        #region Notify
        public Alarm.NotifyType Notify { get => _currentEditAlarm.Notify; set { _currentEditAlarm.Notify = value; OnPropertyChanged(); } }
        public Alarm.PriorityType Priority { get => _currentEditAlarm.Priority; set { _currentEditAlarm.Priority = value; OnPropertyChanged(); } }
        #endregion
        #region Conditions
        public bool RunAsap { get => _currentEditAlarm.RunAsap; set { _currentEditAlarm.RunAsap = value; OnPropertyChanged(); } }
        public bool DeleteAfter { get => _currentEditAlarm.DeleteAfter; set { _currentEditAlarm.DeleteAfter = value; OnPropertyChanged(); } }
        public int DeleteAfterDays { get => _currentEditAlarm.DeleteAfterDays; set { _currentEditAlarm.DeleteAfterDays = value; OnPropertyChanged(); } }
        public bool Wake { get => _currentEditAlarm.Wake; set { _currentEditAlarm.Wake = value; OnPropertyChanged(); } }
        #endregion
        #region Finaly
        public bool Enabled { get => _currentEditAlarm.Enabled; set { _currentEditAlarm.Enabled = value; OnPropertyChanged(); } }
        #endregion
        #region Control IsEnabled properties
        public bool OnLogOption { get { return !(ComputerStarts | LogOn); } }
        public bool AllDaysSelected { get => !_currentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.AllDays); }
        #endregion
        #endregion
        #endregion
        public AlarmViewModel()
        {
            _defaultAlarm = new Alarm()
            {
                //  ...
                StartDate = DateTime.Now,
                StartTime = DateTime.Now,
                SyncTimeZones = false,
                Description = "Default alarm",
                ComputerStarts = false,
                LogOn = false,
                Reiteration = Alarm.ReiterationType.daily,//
                RecurDays = 99,
                RecurWeeks = 99,
                DaysOfTheWeek = DaysOfTheWeek.Monday | DaysOfTheWeek.Wednesday | DaysOfTheWeek.Friday | DaysOfTheWeek.Sunday,
                RandomDelay = false,
                RandomDelayTime = TimeSpan.FromHours(1),//1H
                RepeatAlarm = false,
                RepeatAlarmTime = TimeSpan.FromHours(1),//1H
                RepeatAlarmDuration = false,
                RepeatAlarmDurationTime = TimeSpan.FromMinutes(15),//15M
                StopRepeat = false,
                Notify = Alarm.NotifyType.simpleNotify,
                Priority = Alarm.PriorityType.high,//
                RunAsap = false,
                DeleteAfter = false,
                DeleteAfterDays = 1,
                Wake = true,
                Enabled = true
            };
             _currentEditAlarm = _defaultAlarm;//copy?
        }
    }
}
