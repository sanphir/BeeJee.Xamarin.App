using BeeJee.Xamarin.App.ViewModels;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.Views
{
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}