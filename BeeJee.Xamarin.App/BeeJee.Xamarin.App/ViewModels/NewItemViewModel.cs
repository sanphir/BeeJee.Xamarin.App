using BeeJee.Xamarin.App.Models;
using BeeJee.Xamarin.App.Models.Tasks;
using BeeJee.Xamarin.App.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private ITaskItemsService _taskItemsService => DependencyService.Get<ITaskItemsService>();

        private string _userName;
        private string _email;
        private string _text;

        public ObservableCollection<ValidationError> ValidationErrors { get; }

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

        public AsyncCommand SaveCommand { get; }
        public Command CancelCommand { get; }


        public NewItemViewModel()
        {
            Title = "New task";
            ValidationErrors = new ObservableCollection<ValidationError>();

            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new Command(OnCancel);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {
            var result = await _taskItemsService.CreateAsync(new NewTaskItem
            {
                Email = _email ?? "",
                Text = _text ?? "",
                UserName = _userName ?? "",
            });

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
