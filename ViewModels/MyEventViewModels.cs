using Campus_Activity_Manager.Services;
using Campus_Activity_Manager.Models;
using Campus_Activity_Manager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Campus_Activity_Manager.ViewModels
{
	public partial class MyEventsViewModel : ObservableObject
	{
		private readonly IMyEventService _myEventService;

		[ObservableProperty]
		private bool _isBusy;

		[ObservableProperty]
		private bool _isEmpty;

		public ObservableCollection<Event> MyEventsList { get; } = new();

		// Task 1.5: Constructor with IMyEventService injection
		public MyEventsViewModel(IMyEventService myEventService)
		{
			_myEventService = myEventService;
		}

		// Task 1.6: Load data from Service into ObservableCollection
		[RelayCommand]
		private async Task LoadMyEvents()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				MyEventsList.Clear();

				var events = await _myEventService.GetMyEventsAsync();

				foreach (var e in events)
				{
					MyEventsList.Add(e);
				}
			}
			finally
			{
				IsBusy = false;
				IsEmpty = MyEventsList.Count == 0;
			}
		}

		// Task 1.7: Unregister command
		[RelayCommand]
		private async Task Unregister(Event eventItem)
		{
			if (eventItem == null) return;

			var success = await _myEventService.UnregisterEventAsync(eventItem.Id);

			if (success)
			{
				MyEventsList.Remove(eventItem);
				IsEmpty = MyEventsList.Count == 0;
			}
		}
	}
}
