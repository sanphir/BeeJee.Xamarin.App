using Newtonsoft.Json;

namespace BeeJee.Xamarin.App.Models.Tasks
{
    public class TaskItemsResponse
    {
        [JsonProperty("tasks")]
        public TaskItem[] Tasks { get; set; }
        public int total_task_count { get; set; }
    }
}
