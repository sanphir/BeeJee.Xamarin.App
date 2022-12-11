using BeeJee.Xamarin.App.Providers;

namespace BeeJee.Xamarin.App.Services
{
    public class BaseApiService
    {
        protected const string API_URL = "https://sanphir.com/beejeeback/";

        protected readonly JSchemaProvider _jSchema = JSchemaProvider.Instance;
    }
}
