using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using AlarmClockApp.MVVM.Model;
using AlarmClockApp.MVVM.Abstract;
using System.Windows.Input;
using AlarmClockApp.Shared;
using ControlzEx.Theming;
using AlarmClockApp;

namespace AlarmClockApp.MVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private enum EditModeType { editMode, newMode };
        private enum ListModeType { templatesMode, loadedMode };
        private readonly string[] _themes = { "Light.Blue", "Dark.Blue", "Light.Green", "Dark.Green", "Light.Steel", "Dark.Steel" };
        private int _currentTheme = 0;
        private ICommand _cmdToggleTheme;
        public ICommand CmdToggleTheme
        {
            get
            {
                return _cmdToggleTheme ?? (_cmdToggleTheme = new RelayCommand(x => ToggleTheme()));
            }
        }
        private void ToggleTheme()
        {
            /*ThemeManager.Current.ChangeTheme(MainWindow, "Light.Blue");*/
        }
    }
}
