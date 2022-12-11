using BeeJee.Xamarin.App.Enums;

namespace BeeJee.Xamarin.App.ViewModels
{
    public class HeaderViewModel : BaseViewModel
    {
        private string _fieldName;
        private string _displayName;
        private bool _isSortField;
        private double _width;
        private SortDirection _sortDirection;
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }
        public string SortLable
        {
            get => _sortDirection == SortDirection.ASC ? "asc" : "desc";
            set => OnPropertyChanged(nameof(SortLable));
        }
        public string FieldName
        {
            get => _fieldName;
            set => SetProperty(ref _fieldName, value);
        }
        public bool IsSortField
        {
            get => _isSortField;
            set => SetProperty(ref _isSortField, value);
        }

        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public SortDirection SortDirection
        {
            get => _sortDirection;
            set => SetProperty(ref _sortDirection, value);
        }

        public HeaderViewModel(string fieldName, string displayName = null)
        {
            _fieldName = fieldName;
            _displayName = !string.IsNullOrEmpty(displayName) ? displayName : fieldName;
            _isSortField = false;
        }
    }
}
