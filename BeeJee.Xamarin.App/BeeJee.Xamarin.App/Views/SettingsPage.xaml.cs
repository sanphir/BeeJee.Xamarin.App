using BeeJee.Xamarin.App.ViewModels;
using Xamarin.Forms.Xaml;

namespace BeeJee.Xamarin.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : BaseContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel();
        }
    }
}