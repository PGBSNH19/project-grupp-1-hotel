using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Hotel.Client.Toast
{
    public enum ToastLevel
    {
        Info,
        Success, 
        Warning,
        Error
    }
    public class ToastService : IDisposable
    {
        public event Action<string, ToastLevel> OnShow;
        public event Action OnHide;
        private Timer CountDown;

        public void ShowToast(string message, ToastLevel level)
        {
            OnShow?.Invoke(message, level);
            StartCountDown();
        }
        private void StartCountDown()
        {
            SetCountDown();
            if (CountDown.Enabled)
            {
                CountDown.Stop();
                CountDown.Start();
            }
            else
            {
                CountDown.Start();
            }
        }

        private void SetCountDown()
        {
            if (CountDown == null)
            {
                CountDown = new Timer(6000);
                CountDown.Elapsed += HideToast;
                CountDown.AutoReset = false;
            }
        }

        private void HideToast(object sender, ElapsedEventArgs e)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            CountDown?.Dispose();
        }
    }
}
