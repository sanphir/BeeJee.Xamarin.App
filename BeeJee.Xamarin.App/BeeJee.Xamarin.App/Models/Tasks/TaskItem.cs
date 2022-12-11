using Newtonsoft.Json;

namespace BeeJee.Xamarin.App.Models.Tasks
{
    public enum TaskItemStatus
    {
        NotCompleted = 0,
        NotCompletedAndEditedByAdmin = 1,
        Completed = 10,
        CompletedAndEditedByAdmin = 11
    }
    public class TaskItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("status")]
        public TaskItemStatus Status { get; set; }
    }
}
