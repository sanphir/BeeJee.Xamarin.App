using BeeJee.Xamarin.App.Common;
using BeeJee.Xamarin.App.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            MessagingCenter.Subscribe<AuthChangedEventArgs>(this, Messages.AUTH_CHANGED, OnAuthChanged);

            SecureStorage.Remove(StoreKeys.TOKEN);
        }

        private void OnAuthChanged(AuthChangedEventArgs e)
        {
            AuthMenuItem.Text = e.IsAuth ? "Logout" : "Login";
        }

        private async void OnAuthMenuItemClicked(object sender, EventArgs e)
        {
            if (AuthMenuItem.Text == "Logout")
            {
                SecureStorage.Remove(StoreKeys.TOKEN);
                MessagingCenter.Send(new AuthChangedEventArgs(isAuth: false), Messages.AUTH_CHANGED);
            }
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
