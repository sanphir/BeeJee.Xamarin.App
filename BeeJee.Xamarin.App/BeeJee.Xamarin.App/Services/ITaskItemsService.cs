using BeeJee.Xamarin.App.Enums;
using BeeJee.Xamarin.App.Models;
using BeeJee.Xamarin.App.Models.Tasks;
using System.Threading.Tasks;

namespace BeeJee.Xamarin.App.Services
{
    public interface ITaskItemsService
    {
        Task<ServiceResult<TaskItemsResponse>> GetAsync(string sort_field, SortDirection sort_direction, int page, int pageSize);

        Task<ServiceResult<TaskItem>> CreateAsync(NewTaskItem newItem);

        Task<ServiceResult<TaskItem>> EditAsync(string text, TaskItemStatus status, int id);
    }
}
