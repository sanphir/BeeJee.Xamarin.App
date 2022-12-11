using BeeJee.Xamarin.App.Models;
using BeeJee.Xamarin.App.Models.Tasks;
using BeeJee.Xamarin.App.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    [QueryProperty(nameof(ItemJson), nameof(ItemJson))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private ITaskItemsService _taskItemsService => DependencyService.Get<ITaskItemsService>();
        public ObservableCollection<ValidationError> ValidationErrors { get; } = new ObservableCollection<ValidationError>();

        private TaskItem _item;

        public string ItemJson
        {
            set
            {
                Item = JsonConvert.DeserializeObject<TaskItem>(value);
            }
        }

        public TaskItem Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        public AsyncCommand SaveCommand { get; }
        public Command CancelCommand { get; }

        public ItemDetailViewModel()
        {
            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new Command(OnCancel);
        }


        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {
            var result = await _taskItemsService.EditAsync(Item.Text ?? "", TaskItemStatus.CompletedAndEditedByAdmin, _item.Id);

            if (result.Status == Enums.ResultStatus.Ok)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                ValidationErrors.Clear();
                if (result.ValidationErrors?.Any() ?? false)
                {
                    foreach (var item in result.ValidationErrors)
                    {
                        ValidationErrors.Add(item);
                    }
                }
                else
                {
                    //Somthing whent wrong
                    ValidationErrors.Add(new ValidationError
                    {
                        PropertyName = "Ошибка",
                        ErrorMessage = result.ErrorMessage
                    });
                }
            }
        }
    }
}
