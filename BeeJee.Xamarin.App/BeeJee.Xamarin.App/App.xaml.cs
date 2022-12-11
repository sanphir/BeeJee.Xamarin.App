using BeeJee.Xamarin.App.Services;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<TaskItemsService>();
            DependencyService.Register<TokenService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
