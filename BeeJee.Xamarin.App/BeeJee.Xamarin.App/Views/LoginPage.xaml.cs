using BeeJee.Xamarin.App.ViewModels;
using Xamarin.Forms.Xaml;

namespace BeeJee.Xamarin.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BaseContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}