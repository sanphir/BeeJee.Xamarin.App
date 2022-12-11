using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private int _pagerPageSize = App.SettingsStore.PageSize.GetValue();

        public int PagerPageSize
        {
            get => _pagerPageSize;
            set => SetProperty(ref _pagerPageSize, value);
        }

        public Command UpdateCommand { get; }

        public SettingsViewModel()
        {
            Title = "Settings";

            UpdateCommand = new Command(OnUpdate);
        }

        private void OnUpdate()
        {
            App.SettingsStore.PageSize.SetValue(_pagerPageSize);
        }

    }
}
