using System;

namespace BeeJee.Xamarin.App.Common
{
    public class AuthChangedEventArgs : EventArgs
    {
        public bool IsAuth { get; private set; }
        public AuthChangedEventArgs(bool isAuth)
        {
            IsAuth = isAuth;
        }
    }
}
