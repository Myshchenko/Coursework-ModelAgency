using ModelAgency_Api.Models;
using ModelAgency_Api.Repositories;

namespace ModelAgency_Api.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEvents();

        Task AddEvent(Event modelEvent);

        Task UpdateEvent(Event modelEvent);

        Task DeleteEvent(int Id);
    }

    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddEvent(Event modelEvent)
        {
            await _eventRepository.AddEvent(modelEvent);
            await _eventRepository.AddEventManaging(modelEvent);
        }

        public async Task DeleteEvent(int Id)
        {
            await _eventRepository.DeleteEvent(Id);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var events = await _eventRepository.GetEvents();

            return events;
        }

        public async Task UpdateEvent(Event modelEvent)
        {
            await _eventRepository.UpdateEvent(modelEvent);
        }
    }
}
