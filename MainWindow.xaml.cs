using System.Windows;
using MahApps.Metro.Controls;
using ASquare.WindowsTaskScheduler.Models;
using Microsoft.Win32.TaskScheduler;
using AlarmClockApp.MVVM.ViewModel;
using AlarmClockApp.MVVM.Model;
using AlarmClockApp.Shared;
using ControlzEx.Theming;

namespace AlarmClockApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            this.DataContext = new AlarmViewModel();
            InitializeComponent();

            ThemeManager.Current.ChangeTheme(this, "Light.Blue");
        }
        private void btnApproveAlarm_Click(object sender, RoutedEventArgs e)
        {
            /*            SchedulerResponse response;
                        response = WindowTaskScheduler
                            .Configure()
                            .CreateTask("alarmclock_low", "C:\\Test.bat")
                            .RunDaily()
                            .RunEveryXMinutes(10)
                            .RunDurationFor(new TimeSpan(18, 0, 0))
                            .SetStartDate(new DateTime(2015, 8, 8))
                            .SetStartTime(new TimeSpan(8, 0, 0))
                            .Execute()
                            ;*/

            /*            TaskService ts = new TaskService();

                        //ts.FindAllTasks();
                        dataGridAlarms.ItemsSource = ts.AllTasks;*/
            new Interpreter().PushTestAlarm();

        }

        private void tabList_Loaded(object sender, RoutedEventArgs e)
        {
/*            TaskService ts = new TaskService();
            //ts.FindAllTasks();
            dataGridAlarms.ItemsSource = ts.AllTasks;*/
        }
    }
}
