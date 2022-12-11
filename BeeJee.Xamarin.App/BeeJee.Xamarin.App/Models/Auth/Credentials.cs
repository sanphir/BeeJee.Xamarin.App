using Newtonsoft.Json;

namespace BeeJee.Xamarin.App.Models.Auth
{
    public class Credentials
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }
}
