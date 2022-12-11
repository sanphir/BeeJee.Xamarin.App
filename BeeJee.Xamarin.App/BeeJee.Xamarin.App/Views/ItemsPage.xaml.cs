using BeeJee.Xamarin.App.ViewModels;

namespace BeeJee.Xamarin.App.Views
{
    public partial class ItemsPage : BaseContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = new ItemsViewModel();
        }
    }
}