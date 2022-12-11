using Newtonsoft.Json;

namespace BeeJee.Xamarin.App.Models.Auth
{
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
