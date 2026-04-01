using System.Collections.ObjectModel;
using Campus.Models;

namespace Campus.ViewModels;

public class MyEventsViewModel
{
	public ObservableCollection<Event> Events { get; set; }

	public MyEventsViewModel()
	{
		Events = new ObservableCollection<Event>
		{
			new Event { Title = "Event 1", Description = "Description 1" },
			new Event { Title = "Event 2", Description = "Description 2" }
		};
	}
}