using BeeJee.Xamarin.App.Enums;
using BeeJee.Xamarin.App.Models.Tasks;
using BeeJee.Xamarin.App.Services;
using BeeJee.Xamarin.App.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public const int PAGE_SIZE = 3;
        private ITaskItemsService _taskItemsService => DependencyService.Get<ITaskItemsService>();
        private TaskItem _selectedItem;
        private PageNumberViewModel _currentPageNumber;
        private HeaderViewModel _currentHeader;

        public ObservableCollection<TaskItem> Items { get; }
        public ObservableCollection<PageNumberViewModel> PageNumbers { get; }
        public ObservableCollection<HeaderViewModel> Headers { get; }

        public Command AddItemCommand { get; }
        public Command<TaskItem> ItemTapped { get; }
        public AsyncCommand<HeaderViewModel> HeaderTapped { get; }
        public AsyncCommand<PageNumberViewModel> PagerTapped { get; }

        public TaskItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public HeaderViewModel CurrentHeader
        {
            get => _currentHeader;
            set => SetProperty(ref _currentHeader, value);
        }
        public PageNumberViewModel CurrentPageNumber
        {
            get => _currentPageNumber;
            set => SetProperty(ref _currentPageNumber, value);
        }

        public ItemsViewModel()
        {
            Title = "Tasks list.";

            Items = new ObservableCollection<TaskItem>();
            PageNumbers = new ObservableCollection<PageNumberViewModel>();
            Headers = new ObservableCollection<HeaderViewModel>()
            {
                new HeaderViewModel(nameof(TaskItem.UserName), "User name") { Width = 140, IsSortField = true},
                new HeaderViewModel(nameof(TaskItem.Email)) { Width = 140 },
                new HeaderViewModel(nameof(TaskItem.Text)) { Width = 240 },
                new HeaderViewModel(nameof(TaskItem.Status)) { Width = 240 },
            };

            ItemTapped = new Command<TaskItem>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);

            HeaderTapped = new AsyncCommand<HeaderViewModel>(OnHeaderTapped);
            PagerTapped = new AsyncCommand<PageNumberViewModel>(OnPagerTapped);
        }

        private async Task LoadTaskItems(string sortField, SortDirection sortDirection, int pageNumber)
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var result = await _taskItemsService.GetAsync(sortField, sortDirection, pageNumber);
                if (result.Status == ResultStatus.Ok)
                {
                    foreach (var item in result.Data.Tasks)
                    {
                        Items.Add(item);
                    }

                    Title = $"Tasks list. ({result.Data.total_task_count} items)";

                    FillPageNumbers(result.Data.total_task_count, pageNumber);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FillPageNumbers(int totalCount, int currentNumber)
        {
            var totalPages = (totalCount % PAGE_SIZE) > 0 ? totalCount / PAGE_SIZE + 1 : totalCount / PAGE_SIZE;
            if (PageNumbers.Count == totalPages)
            {
                foreach (var item in PageNumbers)
                {
                    item.IsSelected = item.Number == currentNumber;
                    if (item.IsSelected)
                    {
                        CurrentPageNumber = item;
                    }
                }
            }
            else
            {
                PageNumbers.Clear();

                for (int i = 0; i < totalCount; i += PAGE_SIZE)
                {
                    var page = new PageNumberViewModel
                    {
                        Number = (i + PAGE_SIZE) / PAGE_SIZE,
                    };

                    page.IsSelected = page.Number == currentNumber;
                    PageNumbers.Add(page);

                    if (page.IsSelected)
                    {
                        CurrentPageNumber = page;
                    }
                }
            }
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            IsBusy = true;

            SelectedItem = null;
            CurrentPageNumber = null;
            CurrentHeader = Headers.FirstOrDefault(r => r.IsSortField);

            await LoadTaskItems(nameof(TaskItem.UserName), SortDirection.ASC, 1);
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async Task OnHeaderTapped(HeaderViewModel header)
        {
            if (header == CurrentHeader)
            {
                header.SortDirection = header.SortDirection == SortDirection.ASC ? SortDirection.DESC : SortDirection.ASC;
            }
            else
            {
                foreach (var item in Headers)
                {
                    item.IsSortField = item == header;
                }

                header.SortDirection = SortDirection.ASC;
                CurrentHeader = header;
            }

            await LoadTaskItems(header.FieldName, header.SortDirection, CurrentPageNumber?.Number ?? 1);
        }

        private async Task OnPagerTapped(PageNumberViewModel pageNumber)
        {
            foreach (var item in PageNumbers)
            {
                item.IsSelected = item == pageNumber;
            }
            CurrentPageNumber = pageNumber;

            await LoadTaskItems(CurrentHeader?.FieldName ?? nameof(TaskItem.UserName), CurrentHeader?.SortDirection ?? SortDirection.ASC, pageNumber.Number);
        }

        async void OnItemSelected(TaskItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Item)}={item}");
        }
    }
}