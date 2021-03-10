using System.Windows;
using MahApps.Metro.Controls;
using AlarmClockApp.MVVM.ViewModel;
using System;
using System.Linq;

namespace AlarmClockApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ToastViewModel _notify = new ToastViewModel();
        private AlarmViewModel _alarmVM = new AlarmViewModel();
        public MainWindow()
        {
            this.DataContext = _alarmVM;
            //_alarmVM.UpdTasks();

            string[] args = Environment.GetCommandLineArgs().Skip(1).ToArray();
            Func<string, string> lookupArg =
                option => args.Where(s => s.StartsWith(option)).Select(s => s.Substring(option.Length)).FirstOrDefault();

            string unhideArg = lookupArg("/hidden=");
            if (unhideArg == "yes")
                this.Visibility = Visibility.Hidden;

            string priorityArg = lookupArg("/priority=");
            string descriptionArg = lookupArg("/description=");
            if(priorityArg != "")
                if(descriptionArg!="")
                    switch (priorityArg)
                    {
                        case "high":
                            _notify.ShowWarning(descriptionArg); break;
                        case "normal":
                            _notify.ShowInformation(descriptionArg); break;
                    }

            InitializeComponent();
            Unloaded += OnUnload;
        }
        private void OnUnload(object sender, RoutedEventArgs e)
        {
            _notify.OnUnloaded();
        }

        private void tabList_Loaded(object sender, RoutedEventArgs e)
        {
            _alarmVM.UpdTasks();
        }

        private void tabList_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _alarmVM.UpdTasks();
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _alarmVM.UpdEdit();
        }
    }
}
