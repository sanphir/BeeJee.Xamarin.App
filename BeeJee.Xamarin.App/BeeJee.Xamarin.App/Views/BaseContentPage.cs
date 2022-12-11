using BeeJee.Xamarin.App.ViewModels;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.Views
{
    public class BaseContentPage : ContentPage
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as BaseViewModel)?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as BaseViewModel)?.OnDisappearing();
        }
    }
}
