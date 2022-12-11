using BeeJee.Xamarin.App.ViewModels;

namespace BeeJee.Xamarin.App.Views
{
    public partial class NewItemPage : BaseContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}