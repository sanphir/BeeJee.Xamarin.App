using BeeJee.Xamarin.App.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private int _pagerPageSize = App.SettingsStore.PageSize.GetValue();
        public ObservableCollection<ValidationError> ValidationErrors { get; } = new ObservableCollection<ValidationError>();

        public int PagerPageSize
        {
            get => _pagerPageSize;
            set
            {
                SetProperty(ref _pagerPageSize, value);
                if (ValidationErrors.Count > 0)
                {
                    ValidationErrors.Clear();
                }
            }
        }

        public async override Task OnAppearing()
        {
            await base.OnAppearing();
            ValidationErrors.Clear();
        }

        public Command UpdateCommand { get; }

        public SettingsViewModel()
        {
            Title = "Settings";

            UpdateCommand = new Command(OnUpdate);
        }

        private void OnUpdate()
        {
            if (_pagerPageSize <= 0)
            {
                ValidationErrors.Add(new ValidationError()
                {
                    PropertyName = nameof(PagerPageSize),
                    ErrorMessage = "Неверное значение для размера страницы"
                });
                return;
            }
            App.SettingsStore.PageSize.SetValue(_pagerPageSize);
        }

    }
}
