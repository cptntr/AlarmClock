using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Text;
using AlarmClockApp.MVVM.Model;

namespace AlarmClockApp.Shared
{
    public class Interpreter
    {
        // method only for testing purpose
        public void PushTestAlarm()
        {
            // Create a new task definition for the local machine and assign properties
            TaskDefinition td = TaskService.Instance.NewTask();
            td.RegistrationInfo.Description = "Does something";

            // Add a trigger that, starting tomorrow, will fire every other week on Monday
            // and Saturday and repeat every 10 minutes for the following 11 hours
            WeeklyTrigger wt = new WeeklyTrigger();
            wt.StartBoundary = DateTime.Today.AddDays(1);
            wt.DaysOfWeek = DaysOfTheWeek.Monday | DaysOfTheWeek.Saturday;
            wt.WeeksInterval = 2;
            wt.Repetition.Duration = TimeSpan.FromHours(11);
            wt.Repetition.Interval = TimeSpan.FromMinutes(10);
            td.Triggers.Add(wt);

            // Create an action that will launch Notepad whenever the trigger fires
            td.Actions.Add("notepad.exe", "c:\\test.log");

            // Register the task in the root folder of the local machine
            TaskService.Instance.RootFolder.RegisterTaskDefinition("Test", td);
        }

        public int AlarmToWinSheduler(Alarm _alarm)
        {
            return 0;
        }
    }
}
