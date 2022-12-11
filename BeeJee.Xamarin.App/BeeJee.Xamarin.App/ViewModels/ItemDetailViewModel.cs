using BeeJee.Xamarin.App.Models.Tasks;
using BeeJee.Xamarin.App.Services;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    [QueryProperty(nameof(Item), nameof(Item))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private ITaskItemsService _taskItemsService => DependencyService.Get<ITaskItemsService>();

        private string _userName;
        private string _email;
        private string _text;

        private TaskItem _item;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public TaskItem Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
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
            var result = await _taskItemsService.EditAsync(_text, TaskItemStatus.CompletedAndEditedByAdmin, _item.Id);

            /*if (result.Status == Enums.ResultStatus.Ok)
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
            }*/
        }
    }
}
