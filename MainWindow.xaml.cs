using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using MahApps.Metro.Controls;
using ASquare.WindowsTaskScheduler;
using ASquare.WindowsTaskScheduler.Models;

namespace WPF_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Timer timer;
        /*        int maxsec;
                DateTime curtime;
                DateTime settime;*/
        SchedulerResponse response;
        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            /*            this.Dispatcher.Invoke(() =>
                        {
                            curtime = DateTime.Now;
                            settime = DateTime.Now;


                            if (curtime.Hour == settime.Hour && curtime.Minute == settime.Minute && curtime.Second == settime.Second)
                            {
                                timer.Stop();
                                try
                                {

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        });*/
        }
        private void btnApproveAlarm_Click(object sender, RoutedEventArgs e)
        {
            response = WindowTaskScheduler
                .Configure()
                .CreateTask("TaskName", "C:\\Test.bat")
                .RunDaily()
                .RunEveryXMinutes(10)
                .RunDurationFor(new TimeSpan(18, 0, 0))
                .SetStartDate(new DateTime(2015, 8, 8))
                .SetStartTime(new TimeSpan(8, 0, 0))
                .Execute();
        }
    }
}
