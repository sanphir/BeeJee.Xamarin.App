using BeeJee.Xamarin.App.ViewModels;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}