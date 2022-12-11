using BeeJee.Xamarin.App.Services;
using BeeJee.Xamarin.App.Store;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App
{
    public partial class App : Application
    {
        public static SettingsStore SettingsStore { get; private set; }
        private const int PAGE_SIZE = 3;
        public App()
        {
            InitializeComponent();

            DependencyService.Register<TaskItemsService>();
            DependencyService.Register<TokenService>();

            SettingsStore = new SettingsStore()
            {
                PageSize = new StoreProperty<int>(PAGE_SIZE)

            };
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
