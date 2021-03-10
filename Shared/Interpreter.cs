using Microsoft.Win32.TaskScheduler;
using System;
using AlarmClockApp.MVVM.Model;
using AlarmClockApp.MVVM.ViewModel;
using System.Diagnostics;

namespace AlarmClockApp.Shared
{
    public class Interpreter
    {
        // method only for testing purpose
        public TaskDefinition AlarmScheduler(Alarm _alarm)
        {
            // Create a new task definition for the local machine and assign properties
            TaskDefinition td = TaskService.Instance.NewTask();
            td.RegistrationInfo.Description = _alarm.Description;

            switch (_alarm.Reiteration)
            {
                case Alarm.ReiterationType.oneTime:
                    {
                        if (_alarm.ComputerStarts)
                        {
                            BootTrigger tg = new BootTrigger();
                            if (_alarm.DeleteAfter)
                                tg.EndBoundary = DateTime.Now + TimeSpan.FromDays(_alarm.DeleteAfterDays);
                            if (_alarm.RandomDelay)
                                tg.Delay = _alarm.RandomDelayTime;
                            if (_alarm.RepeatAlarm)
                                tg.Repetition.Interval = _alarm.RepeatAlarmTime;
                            if (_alarm.RepeatAlarmDuration)
                                tg.Repetition.Duration = _alarm.RepeatAlarmDurationTime;
                            if (_alarm.StopRepeat)
                                tg.Repetition.StopAtDurationEnd = _alarm.StopRepeat;
                            td.Triggers.Add(tg);
                        }
                        else
                        if (_alarm.Wake)
                        {
                            LogonTrigger tg = new LogonTrigger();
                            if (_alarm.DeleteAfter)
                                tg.EndBoundary = DateTime.Now + TimeSpan.FromDays(_alarm.DeleteAfterDays);
                            if (_alarm.RandomDelay)
                                tg.Delay = _alarm.RandomDelayTime;
                            if (_alarm.RepeatAlarm)
                                tg.Repetition.Interval = _alarm.RepeatAlarmTime;
                            if (_alarm.RepeatAlarmDuration)
                                tg.Repetition.Duration = _alarm.RepeatAlarmDurationTime;
                            if (_alarm.StopRepeat)
                                tg.Repetition.StopAtDurationEnd = _alarm.StopRepeat;
                            td.Triggers.Add(tg);
                        }
                        else
                        {
                            TimeTrigger tg = new TimeTrigger();
                            tg.StartBoundary = new DateTime(_alarm.StartDate.Year, _alarm.StartDate.Month, _alarm.StartDate.Day, _alarm.StartTime.Hour, _alarm.StartTime.Minute, _alarm.StartTime.Second);
                            if (_alarm.DeleteAfter)
                                tg.EndBoundary = DateTime.Now + TimeSpan.FromDays(_alarm.DeleteAfterDays);
                            if (_alarm.RandomDelay)
                                tg.RandomDelay = _alarm.RandomDelayTime;
                            if (_alarm.RepeatAlarm)
                                tg.Repetition.Interval = _alarm.RepeatAlarmTime;
                            if (_alarm.RepeatAlarmDuration)
                                tg.Repetition.Duration = _alarm.RepeatAlarmDurationTime;
                            if (_alarm.StopRepeat)
                                tg.Repetition.StopAtDurationEnd = _alarm.StopRepeat;
                            td.Triggers.Add(tg);
                        }
                        break;
                    }
                case Alarm.ReiterationType.daily:
                    {
                        DailyTrigger tg = new DailyTrigger(_alarm.RecurDays);
                        tg.StartBoundary = new DateTime(_alarm.StartDate.Year, _alarm.StartDate.Month, _alarm.StartDate.Day, _alarm.StartTime.Hour, _alarm.StartTime.Minute, _alarm.StartTime.Second);
                        if (_alarm.DeleteAfter)
                            tg.EndBoundary = DateTime.Now + TimeSpan.FromDays(_alarm.DeleteAfterDays);
                        if (_alarm.RandomDelay)
                            tg.RandomDelay = _alarm.RandomDelayTime;
                        if (_alarm.RepeatAlarm)
                            tg.Repetition.Interval = _alarm.RepeatAlarmTime;
                        if (_alarm.RepeatAlarmDuration)
                            tg.Repetition.Duration = _alarm.RepeatAlarmDurationTime;
                        if (_alarm.StopRepeat)
                            tg.Repetition.StopAtDurationEnd = _alarm.StopRepeat;
                        td.Triggers.Add(tg);
                        break;
                    }
                case Alarm.ReiterationType.weekly:
                    {
                        WeeklyTrigger tg = new WeeklyTrigger();
                        tg.WeeksInterval = _alarm.RecurWeeks;
                        tg.DaysOfWeek = _alarm.DaysOfTheWeek;
                        tg.StartBoundary = new DateTime(_alarm.StartDate.Year, _alarm.StartDate.Month, _alarm.StartDate.Day, _alarm.StartTime.Hour, _alarm.StartTime.Minute, _alarm.StartTime.Second);
                        if (_alarm.DeleteAfter)
                            tg.EndBoundary = DateTime.Now + TimeSpan.FromDays(_alarm.DeleteAfterDays);
                        if (_alarm.RandomDelay)
                            tg.RandomDelay = _alarm.RandomDelayTime;
                        if (_alarm.RepeatAlarm)
                            tg.Repetition.Interval = _alarm.RepeatAlarmTime;
                        if (_alarm.RepeatAlarmDuration)
                            tg.Repetition.Duration = _alarm.RepeatAlarmDurationTime;
                        if (_alarm.StopRepeat)
                            tg.Repetition.StopAtDurationEnd = _alarm.StopRepeat;
                        td.Triggers.Add(tg);
                        break;
                    }
            }

            td.Settings.StartWhenAvailable = _alarm.RunAsap;
            if (_alarm.DeleteAfter)
                td.Settings.DeleteExpiredTaskAfter = TimeSpan.FromDays(_alarm.DeleteAfterDays);
            //td.Settings.ExecutionTimeLimit = TimeSpan.FromMinutes(10);
            td.Settings.WakeToRun = _alarm.Wake;
            td.Settings.Enabled = _alarm.Enabled;

            string pathOnAppExe = Process.GetCurrentProcess().MainModule.FileName;
            if (_alarm.Description == "")
                _alarm.Description = "!ALARM!";

            try
            {
                TaskService.Instance.RootFolder.CreateFolder("Alarm Clock");
            }
            catch { }

            td.Actions.Add(pathOnAppExe, $"/hidden=yes /priority={_alarm.Priority.ToString()} /description=\"{_alarm.Description}\"");

            return td;
        }

        public Alarm AlarmScheduler(Task _task)
        {
            Alarm _alarm = new Alarm()
            {
                Name = _task.Name,
                StartDate = _task.Definition.Triggers[0].StartBoundary,
                StartTime = _task.Definition.Triggers[0].StartBoundary,
                SyncTimeZones = false,//???
                Description = _task.Definition.RegistrationInfo.Description,
                ComputerStarts = _task.Definition.Triggers[0].TriggerType == TaskTriggerType.Boot,
                LogOn = _task.Definition.Triggers[0].TriggerType == TaskTriggerType.Logon,
                //Reiteration = Alarm.ReiterationType.daily,//
                //RecurDays = 1,
                //RecurWeeks = 1,
                //DaysOfTheWeek = 1,
                //RandomDelay = 1,
                //RandomDelayTime = TimeSpan.FromHours(1),
                RepeatAlarm = _task.Definition.Triggers[0].Repetition.Interval.TotalSeconds != 0,
                RepeatAlarmTime = _task.Definition.Triggers[0].Repetition.Interval,
                RepeatAlarmDuration = _task.Definition.Triggers[0].Repetition.Duration.TotalSeconds != 0,
                RepeatAlarmDurationTime = _task.Definition.Triggers[0].Repetition.Duration,
                StopRepeat = _task.Definition.Triggers[0].Repetition.StopAtDurationEnd,
                //Notify = Alarm.NotifyType.simpleNotify,
                //Priority = Alarm.PriorityType.normal,//
                RunAsap = _task.Definition.Settings.StartWhenAvailable,
                DeleteAfter = _task.Definition.Settings.DeleteExpiredTaskAfter.TotalSeconds != 0,
                DeleteAfterDays = (int)_task.Definition.Settings.DeleteExpiredTaskAfter.TotalDays,
                Wake = _task.Definition.Settings.WakeToRun,
                Enabled = _task.Enabled
            };

            switch (_task.Definition.Triggers[0].TriggerType)
            {
                case TaskTriggerType.Daily:
                    DailyTrigger tg = (DailyTrigger)(_task.Definition.Triggers[0]);
                    _alarm.RecurDays = tg.DaysInterval;
                    _alarm.RandomDelay = tg.RandomDelay.TotalSeconds == 0;
                    _alarm.RandomDelayTime = tg.RandomDelay;
                    _alarm.Reiteration = Alarm.ReiterationType.daily; break;
                case TaskTriggerType.Weekly:
                    WeeklyTrigger wt = (WeeklyTrigger)(_task.Definition.Triggers[0]);
                    _alarm.RandomDelay = wt.RandomDelay.TotalSeconds == 0;
                    _alarm.RandomDelayTime = wt.RandomDelay;
                    _alarm.RecurWeeks = wt.WeeksInterval;
                    _alarm.DaysOfTheWeek = wt.DaysOfWeek;
                    _alarm.Reiteration = Alarm.ReiterationType.weekly; break;
                case TaskTriggerType.Time:
                    _alarm.Reiteration = Alarm.ReiterationType.oneTime; break;
            }

            return _alarm;
        }

        public bool PushAlarmToWinScheduler(Alarm _alarm)
        {
            /*            try
                        {
                            Environment.SetEnvironmentVariable("ALARMCLOCKPATH", Process.GetCurrentProcess().MainModule.FileName, EnvironmentVariableTarget.Machine);
                        }
                        catch (Exception ex)
                        {
                            new ToastViewModel().ShowError(string.Format("Failed to set Environment Variable ({0})", ex.Message));
                        }*/

            try
            {
                TaskDefinition td = AlarmScheduler(_alarm);
                if (!td.Validate())
                    throw new Exception("Alarm validation failed");
                // Register the task in the root folder of the local machine
                string _alarmName = "ALARM_" + new Random().Next();
                TaskService.Instance.RootFolder.RegisterTaskDefinition(_alarmName, td);
            }
            catch (Exception ex)
            {
                new ToastViewModel().ShowError(ex.Message);
                System.Media.SystemSounds.Beep.Play();
                return false;
            }

            new ToastViewModel().ShowSuccess("Alarm set successfully");
            System.Media.SystemSounds.Asterisk.Play();
            return true;
        }
    }
}
