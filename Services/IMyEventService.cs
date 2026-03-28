using Campus_Activity_Manager.Models;

namespace Campus_Activity_Manager.Services
{
	public interface IMyEventService
	{
		Task<List<Event>> GetMyEventsAsync();
		Task<bool> UnregisterEventAsync(Guid eventId);
	}
}
