using System;
using System.Diagnostics;
using System.Windows;
using Scrooge.Service;
using Scrooge.Service.Definitions;

namespace Scrooge
{
    public class ErrorHandler
    {
        public void Panic(Exception exception)
        {
            MessageBox.Show(exception.ToString(), "Error");
            Singleton<ServiceController>.Instance.Get<IApplicationEventService>()?.TriggerApplicationClosing();
            Application.Current.Shutdown(exception.HResult);
            Process.GetCurrentProcess().Kill();
        }
    }
}
