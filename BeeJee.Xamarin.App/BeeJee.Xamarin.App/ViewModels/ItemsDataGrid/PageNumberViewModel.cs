namespace BeeJee.Xamarin.App.ViewModels
{
    public class PageNumberViewModel : BaseViewModel
    {
        public int _number = 0;
        public bool _isSelected;
        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
