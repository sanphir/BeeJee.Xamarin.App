using BeeJee.Xamarin.App.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool _isBusy = false;
        bool _isAuth = false;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public bool IsAuth
        {
            get { return _isAuth; }
            set { SetProperty(ref _isAuth, value); }
        }

        string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public virtual async Task OnAppearing()
        {
            var token = await SecureStorage.GetAsync(StoreKeys.TOKEN);
            IsAuth = !string.IsNullOrEmpty(token);

            SubscribeMessages();

            await Task.CompletedTask;
        }

        public virtual async Task OnDisappearing()
        {
            UnsubscribeMessages();
            await Task.CompletedTask;
        }

        public virtual async Task OnDis()
        {
            SubscribeMessages();
            await Task.CompletedTask;
        }

        protected virtual void SubscribeMessages()
        {
            MessagingCenter.Subscribe<AuthChangedEventArgs>(this, Messages.AUTH_CHANGED, (e) => { IsAuth = e.IsAuth; });
        }

        protected virtual void UnsubscribeMessages()
        {
            MessagingCenter.Unsubscribe<AuthChangedEventArgs>(this, Messages.AUTH_CHANGED);
        }
    }
}
