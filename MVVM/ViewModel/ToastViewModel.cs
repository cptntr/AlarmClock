using System;
using System.ComponentModel;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AlarmClockApp.MVVM.ViewModel
{
    public class ToastViewModel : INotifyPropertyChanged
    {
        private Notifier _notifier;

        public void OnUnloaded()
        {
            ClearAll();
            _notifier.Dispose();
        }

        private Notifier NotifierOutWindow()
        {
            return _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new PrimaryScreenPositionProvider(
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromDays(99),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(6));

                cfg.Dispatcher = Application.Current.Dispatcher;

                cfg.DisplayOptions.TopMost = true;
                cfg.DisplayOptions.Width = 250;
            });
            _notifier.ClearMessages(new ClearAll());
        }
        private Notifier NotifierInWindow()
        {
            return _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomCenter,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(6));

                cfg.Dispatcher = Application.Current.Dispatcher;

                cfg.DisplayOptions.TopMost = true;
                cfg.DisplayOptions.Width = 250;
            });
            _notifier.ClearMessages(new ClearAll());
        }

        private MessageOptions OptInWindow()
        {
            return new MessageOptions
            {
                FontSize = 16, // set notification font size
                ShowCloseButton = false, // set the option to show or hide notification close button
                //Tag = "",
                FreezeOnMouseEnter = true, // set the option to prevent notification dissapear automatically if user move cursor on it
                NotificationClickAction = n => // set the callback for notification click event
                {
                    n.Close();
                },
            };
        }
        private MessageOptions OptOutWindow()
        {
            return new MessageOptions
            {
                FontSize = 24, // set notification font size
                ShowCloseButton = true, // set the option to show or hide notification close button
                //Tag = "",
                FreezeOnMouseEnter = true, // set the option to prevent notification dissapear automatically if user move cursor on it
                NotificationClickAction = n => // set the callback for notification click event
                {
                    //n.Close(); // call Close method to remove notification
                    Environment.Exit(-1);
                },
                CloseClickAction = n =>
                {
                    //n.Close();
                    Environment.Exit(-1);
                },
            };
        }

        public void ShowInformation(string message)
        {
            NotifierOutWindow().ShowInformation(message, OptOutWindow());
            System.Media.SystemSounds.Beep.Play();
        }
        public void ShowWarning(string message)
        {
            NotifierOutWindow().ShowWarning(message, OptOutWindow());
            System.Media.SystemSounds.Beep.Play();
        }
        public void ShowSuccess(string message)
        {
            NotifierInWindow().ShowSuccess(message, OptInWindow());
        }
        public void ShowError(string message)
        {
            NotifierInWindow().ShowError(message, OptInWindow());
        }
        public void ClearAll()
        {
            _notifier.ClearMessages(new ClearAll());
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}