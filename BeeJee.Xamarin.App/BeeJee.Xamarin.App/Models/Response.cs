using Newtonsoft.Json;

namespace BeeJee.Xamarin.App.Models
{
    public class ResponseMessage<TStatus, TMessage>
        where TMessage : class

    {
        [JsonProperty("message")]
        public TMessage Message { get; set; }

        [JsonProperty("status")]
        public TStatus Status { get; set; }
    }
}
