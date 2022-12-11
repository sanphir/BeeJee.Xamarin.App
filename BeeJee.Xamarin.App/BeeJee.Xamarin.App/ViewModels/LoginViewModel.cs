using BeeJee.Xamarin.App.Common;
using BeeJee.Xamarin.App.Models;
using BeeJee.Xamarin.App.Models.Auth;
using BeeJee.Xamarin.App.Services;
using BeeJee.Xamarin.App.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ITokenService _tokenService => DependencyService.Get<ITokenService>();

        private string _userName;
        private string _password;

        public ObservableCollection<ValidationError> ValidationErrors { get; } = new ObservableCollection<ValidationError>();

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public Command LoginCommand { get; }
        public Command CancelCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            CancelCommand = new Command(OnCancel);
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        public async override Task OnAppearing()
        {
            await base.OnAppearing();

            ClearValues();

            await Task.CompletedTask;
        }

        private void ClearValues()
        {
            ValidationErrors.Clear();
            UserName = "";
            Password = "";
        }

        private async void OnLoginClicked(object obj)
        {
            var result = await _tokenService.GetToken(new Credentials
            {
                UserName = _userName,
                Password = _password
            });

            if (result.Status == Enums.ResultStatus.Ok)
            {
                await SecureStorage.SetAsync(StoreKeys.TOKEN, result?.Data?.Token ?? throw new UnauthorizedAccessException("Пустой токен"));
                MessagingCenter.Send(new AuthChangedEventArgs(isAuth: true), Messages.AUTH_CHANGED);
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                ValidationErrors.Clear();
                if (result.ValidationErrors?.Any() ?? false)
                {
                    foreach (var item in result.ValidationErrors)
                    {
                        ValidationErrors.Add(item);
                    }
                }
                else
                {
                    //Somthing whent wrong
                    ValidationErrors.Add(new ValidationError
                    {
                        PropertyName = "Ошибка",
                        ErrorMessage = result.ErrorMessage
                    });
                }
            }
        }
    }
}
