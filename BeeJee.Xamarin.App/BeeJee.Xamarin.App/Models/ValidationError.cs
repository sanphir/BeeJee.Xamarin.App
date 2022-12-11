using Newtonsoft.Json;

namespace BeeJee.Xamarin.App.Models
{
    public class ValidationError
    {
        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
