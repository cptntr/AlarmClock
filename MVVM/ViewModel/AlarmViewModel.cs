using System;
using System.Collections.Generic;
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
        private Task _selectedTask;

        private ObservableCollection<Alarm> _deployedAlarms;
        private ObservableCollection<Alarm> _alarmTemplates;
        private IEnumerable<Task> _tasks;
        #endregion
        #region Commands
        private ICommand _cmdUpdTasks;
        public ICommand CmdUpdTasks
        {
            get
            {
                return _cmdUpdTasks ?? (_cmdUpdTasks = new RelayCommand(x => UpdTasks()));
            }
        }

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
        private ICommand _cmdAddAlarm;
        public ICommand CmdTAddAlarm
        {
            get
            {
                return _cmdAddAlarm ?? (_cmdAddAlarm = new RelayCommand(x => AddAlarm()));
            }
        }
        public void UpdTasks()
        {
            Tasks = new TaskService().FindAllTasks(new System.Text.RegularExpressions.Regex(@"ALARM_\d*"));
        }
        public void UpdEdit()
        {
            CurrentEditAlarm = new Interpreter().AlarmScheduler(SelectedTask);
            OnPropertyChanged("Mon"); OnPropertyChanged("Tue"); OnPropertyChanged("Wed"); OnPropertyChanged("Thu"); OnPropertyChanged("Fri"); OnPropertyChanged("Sat"); OnPropertyChanged("Sun"); OnPropertyChanged("AllDaysSelected");
            OnPropertyChanged("StartDate");
            OnPropertyChanged("StartTime");
            OnPropertyChanged("Description");
            OnPropertyChanged("ComputerStarts");
            OnPropertyChanged("LogOn");
            OnPropertyChanged("Reiteration");
            OnPropertyChanged("RecurDays");
            OnPropertyChanged("RecurWeeks");
            OnPropertyChanged("");
        }
        private void AddAlarm()
        {
            new Interpreter().PushAlarmToWinScheduler(CurrentEditAlarm);
        }
        /*        private ICommand _cmdOneTime;
                public ICommand CmdOneTime
                {
                    get
                    {
                        return _cmdToggleAllDays ?? (_cmdToggleAllDays = new RelayCommand(x => OneTime()));
                    }
                }
                private void OneTime()
                {
                    Reiteration = Alarm.ReiterationType.oneTime;
                }
                private ICommand _cmdDaily;
                public ICommand CmdDaily
                {
                    get
                    {
                        return _cmdToggleAllDays ?? (_cmdToggleAllDays = new RelayCommand(x => Daily()));
                    }
                }
                private void Daily()
                {
                    Reiteration = Alarm.ReiterationType.daily;
                }
                private ICommand _cmdWeekly;
                public ICommand CmdWeekly
                {
                    get
                    {
                        return _cmdToggleAllDays ?? (_cmdToggleAllDays = new RelayCommand(x => Weekly()));
                    }
                }
                private void Weekly()
                {
                    Reiteration = Alarm.ReiterationType.oneTime;
                }*/
        #endregion
        #region Properties
        #region Lists
        public IEnumerable<Task> Tasks
        {
            get => _tasks; set { _tasks = value; OnPropertyChanged(); }
        }
        public Task SelectedTask
        {
            get => _selectedTask; set { _selectedTask = value; OnPropertyChanged(); }
        }
        #endregion
        #region Basic
        public Alarm CurrentEditAlarm { get => _currentEditAlarm; set => OnPropertyChanged(ref _currentEditAlarm, value); }
        // We may want to load the default values from the file if one exists
        public Alarm DefaultAlarm { get => _defaultAlarm; set => OnPropertyChanged(ref _defaultAlarm, value); }
        #endregion
        #region Alarm properties
        #region Main
        public string Name { get => CurrentEditAlarm.Name; set { CurrentEditAlarm.Name = value; OnPropertyChanged(); } }
        public DateTime StartDate { get => CurrentEditAlarm.StartDate; set { CurrentEditAlarm.StartDate = value; OnPropertyChanged(); } }
        public DateTime StartTime { get => CurrentEditAlarm.StartTime; set { CurrentEditAlarm.StartTime = value; OnPropertyChanged(); } }
        public bool SyncTimeZones { get => CurrentEditAlarm.SyncTimeZones; set { CurrentEditAlarm.SyncTimeZones = value; OnPropertyChanged(); } }
        public string Description { get => CurrentEditAlarm.Description; set { CurrentEditAlarm.Description = value; OnPropertyChanged(); } }
        #endregion
        #region Repeat
        public bool ComputerStarts { get => CurrentEditAlarm.ComputerStarts; set { CurrentEditAlarm.ComputerStarts = value; OnPropertyChanged(); OnPropertyChanged("OnLogOption"); OnPropertyChanged("LogOnEnabled"); OnPropertyChanged("DelayText"); } }
        public bool LogOn { get => CurrentEditAlarm.LogOn; set { CurrentEditAlarm.LogOn = value; OnPropertyChanged(); OnPropertyChanged("OnLogOption"); OnPropertyChanged("ComputerStartsEnabled"); OnPropertyChanged("DelayText"); } }
        public Alarm.ReiterationType Reiteration { get => CurrentEditAlarm.Reiteration; set { CurrentEditAlarm.Reiteration = value; OnPropertyChanged(); } }
        public short RecurDays { get => CurrentEditAlarm.RecurDays; set { CurrentEditAlarm.RecurDays = value; OnPropertyChanged(); } }
        public short RecurWeeks { get => CurrentEditAlarm.RecurWeeks; set { CurrentEditAlarm.RecurWeeks = value; OnPropertyChanged(); } }
        public bool Mon { get => CurrentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Monday); set { if (value) CurrentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Monday; else CurrentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Monday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Tue { get => CurrentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Tuesday); set { if (value) CurrentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Tuesday; else CurrentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Tuesday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Wed { get => CurrentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Wednesday); set { if (value) CurrentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Wednesday; else CurrentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Wednesday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Thu { get => CurrentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Thursday); set { if (value) CurrentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Thursday; else CurrentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Thursday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Fri { get => CurrentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Friday); set { if (value) CurrentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Friday; else CurrentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Friday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Sat { get => CurrentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Saturday); set { if (value) CurrentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Saturday; else CurrentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Saturday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        public bool Sun { get => CurrentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.Sunday); set { if (value) CurrentEditAlarm.DaysOfTheWeek |= DaysOfTheWeek.Sunday; else CurrentEditAlarm.DaysOfTheWeek &= ~DaysOfTheWeek.Sunday; OnPropertyChanged(); OnPropertyChanged("AllDaysSelected"); } }
        #endregion
        #region Advanced
        public bool RandomDelay { get => CurrentEditAlarm.RandomDelay; set { CurrentEditAlarm.RandomDelay = value; OnPropertyChanged(); } }
        public TimeSpan RandomDelayTime { get => CurrentEditAlarm.RandomDelayTime; set { CurrentEditAlarm.RandomDelayTime = value; OnPropertyChanged(); } }
        public bool RepeatAlarm { get => CurrentEditAlarm.RepeatAlarm; set { CurrentEditAlarm.RepeatAlarm = value; OnPropertyChanged(); } }
        public TimeSpan RepeatAlarmTime { get => CurrentEditAlarm.RepeatAlarmTime; set { CurrentEditAlarm.RepeatAlarmTime = value; OnPropertyChanged(); } }
        public bool RepeatAlarmDuration { get => CurrentEditAlarm.RepeatAlarmDuration; set { CurrentEditAlarm.RepeatAlarmDuration = value; OnPropertyChanged(); } }
        public TimeSpan RepeatAlarmDurationTime { get => CurrentEditAlarm.RepeatAlarmDurationTime; set { CurrentEditAlarm.RepeatAlarmDurationTime = value; OnPropertyChanged(); } }
        public bool StopRepeat { get => CurrentEditAlarm.StopRepeat; set { CurrentEditAlarm.StopRepeat = value; OnPropertyChanged(); } }
        #endregion
        #region Notify
        public Alarm.NotifyType Notify { get => CurrentEditAlarm.Notify; set { CurrentEditAlarm.Notify = value; OnPropertyChanged(); } }
        public Alarm.PriorityType Priority { get => CurrentEditAlarm.Priority; set { CurrentEditAlarm.Priority = value; OnPropertyChanged(); } }
        #endregion
        #region Conditions
        public bool RunAsap { get => CurrentEditAlarm.RunAsap; set { CurrentEditAlarm.RunAsap = value; OnPropertyChanged(); } }
        public bool DeleteAfter { get => CurrentEditAlarm.DeleteAfter; set { CurrentEditAlarm.DeleteAfter = value; OnPropertyChanged(); } }
        public int DeleteAfterDays { get => CurrentEditAlarm.DeleteAfterDays; set { CurrentEditAlarm.DeleteAfterDays = value; OnPropertyChanged(); } }
        public bool Wake { get => CurrentEditAlarm.Wake; set { CurrentEditAlarm.Wake = value; OnPropertyChanged(); } }
        #endregion
        #region Finaly
        public bool Enabled { get => CurrentEditAlarm.Enabled; set { CurrentEditAlarm.Enabled = value; OnPropertyChanged(); } }
        #endregion
        #endregion
        #region Control IsEnabled properties
        public bool OnLogOption { get { return !(ComputerStarts | LogOn); } }
        public bool AllDaysSelected { get => !CurrentEditAlarm.DaysOfTheWeek.HasFlag(DaysOfTheWeek.AllDays); }
        public bool ComputerStartsEnabled { get => !LogOn; }
        public bool LogOnEnabled { get => !ComputerStarts; }
        #endregion
        #region Control text properties
        public string DelayText { get => (ComputerStarts | LogOn) ? " Delay alarm" : " Delay alarm for up to (random delay)"; }
        #endregion
        #endregion
        public AlarmViewModel()
        {
            _defaultAlarm = new Alarm()
            {
                //  ...
                StartDate = DateTime.Now,
                StartTime = DateTime.Now + TimeSpan.FromMinutes(1),
                SyncTimeZones = false,
                Description = string.Empty,
                ComputerStarts = false,
                LogOn = false,
                Reiteration = Alarm.ReiterationType.daily,//
                RecurDays = 1,
                RecurWeeks = 1,
                DaysOfTheWeek = DaysOfTheWeek.Monday | DaysOfTheWeek.Tuesday | DaysOfTheWeek.Wednesday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday,
                RandomDelay = false,
                RandomDelayTime = TimeSpan.FromHours(1),
                RepeatAlarm = false,
                RepeatAlarmTime = TimeSpan.FromMinutes(30),
                RepeatAlarmDuration = false,
                RepeatAlarmDurationTime = TimeSpan.FromMinutes(15),
                StopRepeat = false,
                Notify = Alarm.NotifyType.simpleNotify,
                Priority = Alarm.PriorityType.normal,//
                RunAsap = false,
                DeleteAfter = false,
                DeleteAfterDays = 1,
                Wake = true,
                Enabled = true
            };
             _currentEditAlarm = _defaultAlarm;
        }
    }
}
