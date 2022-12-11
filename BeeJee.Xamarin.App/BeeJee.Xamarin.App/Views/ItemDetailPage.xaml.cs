using BeeJee.Xamarin.App.ViewModels;

namespace BeeJee.Xamarin.App.Views
{
    public partial class ItemDetailPage : BaseContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}