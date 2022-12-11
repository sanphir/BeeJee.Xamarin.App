using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://sanphir.com/beejeeback/swagger/index.html"));
        }

        public ICommand OpenWebCommand { get; }
    }
}