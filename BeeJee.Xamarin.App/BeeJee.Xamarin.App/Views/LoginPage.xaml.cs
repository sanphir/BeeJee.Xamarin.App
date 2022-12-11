using BeeJee.Xamarin.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeeJee.Xamarin.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}