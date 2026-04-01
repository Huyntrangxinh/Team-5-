using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Campus.Models;
using Campus.Services;

namespace Campus.ViewModels;

public partial class EventViewModels : ObservableObject
{
	private readonly ICategoryService _categoryService;
	private readonly IEventService _eventService;

	public EventViewModels(ICategoryService categoryService, IEventService eventService)
	{
		_categoryService = categoryService;
		_eventService = eventService;
		_ = LoadDataAsync();
		MyEventsList.Add(new Event
		{
			Title = "TEST EVENT",
			Description = "NOW IT WILL SHOW"
		});
	}

	[ObservableProperty]
	private ObservableCollection<Category> _categories = new();

	[ObservableProperty]
	private ObservableCollection<Event> _events = new();

	[ObservableProperty]
	private ObservableCollection<Event> _filteredEvents = new();

	[ObservableProperty]
	private Category? _selectedCategory;

	[ObservableProperty]
	private bool _isLoading;

	[ObservableProperty]
	private bool _hasNoEvents;

	[ObservableProperty]
	private bool _isBusy;

	[ObservableProperty]
	private bool _isEmpty;

	
	public ObservableCollection<Event> MyEventsList { get; set; } = new();

	private async Task LoadDataAsync()
	{
		IsLoading = true;
		try
		{
			var categories = await _categoryService.GetAllCategoriesAsync();
			Categories = new ObservableCollection<Category>(categories);

			var events = await _eventService.GetAllEventsAsync();
			Events = new ObservableCollection<Event>(events);

			FilteredEvents = new ObservableCollection<Event>(events);
			HasNoEvents = !FilteredEvents.Any();
		}
		finally
		{
			IsLoading = false;
		}
	}

	[RelayCommand]
	private async Task SelectCategory(Category? category)
	{
		SelectedCategory = category;
		await FilterEventsAsync();
	}

	[RelayCommand]
	private async Task ShowAllCategories()
	{
		SelectedCategory = null;
		await FilterEventsAsync();
	}

	[RelayCommand]
	private async Task RefreshAsync()
	{
		await LoadDataAsync();
		if (SelectedCategory != null)
		{
			await FilterEventsAsync();
		}
	}
	[RelayCommand]
	private async Task ApplyFilter()
	{
		await FilterEventsAsync();
	}

	[RelayCommand]
	private async Task ClearFilter()
	{
		SelectedCategory = null;
		await FilterEventsAsync();
	}
	private async Task FilterEventsAsync()
	{
		if (SelectedCategory == null)
		{
			FilteredEvents = new ObservableCollection<Event>(Events);
		}
		else
		{
			var filtered = await _eventService.GetEventsByCategoryAsync(SelectedCategory.CategoryId);
			FilteredEvents = new ObservableCollection<Event>(filtered);
		}
		HasNoEvents = !FilteredEvents.Any();
	}

	[RelayCommand]
	private async Task LoadMyEvents()
	{
		if (IsBusy) return;

		try
		{
			IsBusy = true;

			MyEventsList.Clear(); // ✅ ĐỔI Ở ĐÂY

			var events = await _eventService.GetMyEventsAsync();

			foreach (var e in events)
			{
				MyEventsList.Add(e); // ✅ ĐỔI Ở ĐÂY
			}
		}
		finally
		{
			IsBusy = false;
			IsEmpty = MyEventsList.Count == 0; // ✅ ĐỔI Ở ĐÂY
		}
	}

	[RelayCommand]
	private async Task Unregister(Event eventItem)
	{
		if (eventItem == null) return;

		var success = await _eventService.UnregisterEventAsync(eventItem.Id);

		if (success)
		{
			MyEventsList.Remove(eventItem); // ✅ ĐỔI Ở ĐÂY
			IsEmpty = MyEventsList.Count == 0; // ✅ ĐỔI Ở ĐÂY
		}
	}

}