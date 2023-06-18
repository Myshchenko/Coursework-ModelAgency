using ModelAgency_Api.Models;
using ModelAgency_Api.Repositories;

namespace ModelAgency_Api.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetEvents();

        Task<List<ModelEvent>> GetEventsForModel(int modelId);

        Task<List<Event>> GetAvailableEventsForSpecificModel(int modelId);

        Task AddEvent(Event modelEvent);
        Task AddModelToTheEvent(ModelEventCoordinates modelEventCoordinates);

        Task UpdateEvent(Event modelEvent);

        Task UpdateModelEventResponce(ModelEventCoordinates modelEventCoordinates);

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
            modelEvent.CreatedAt = DateTime.Now;

            if (modelEvent.IsValid())
            {
                await _eventRepository.AddEvent(modelEvent);
                await _eventRepository.AddEventManaging(modelEvent);
            }
            else
            {
                throw new Exception("Invalid Event");
            }
        }

        public async Task AddModelToTheEvent(ModelEventCoordinates modelEventCoordinates)
        {
            if (modelEventCoordinates.IsValid())
            {
                await _eventRepository.AddModelToTheEvent(modelEventCoordinates);
            }
            else
            {
                throw new Exception("Invalid object");
            }
        }

        public async Task DeleteEvent(int Id)
        {
            await _eventRepository.DeleteEvent(Id);
        }

        public async Task<List<Event>> GetEvents()
        {
            var events = await _eventRepository.GetEvents();

            return events;
        }

        public async Task<List<ModelEvent>> GetEventsForModel(int modelId)
        {
            return await _eventRepository.GetEventsForModel(modelId);
        }

        public async Task<List<Event>> GetAvailableEventsForSpecificModel(int modelId)
        {
            return await _eventRepository.GetAvailableEventsForSpecificModel(modelId);
        }

        public async Task UpdateEvent(Event modelEvent)
        {
            await _eventRepository.UpdateEvent(modelEvent);
        }

        public async Task UpdateModelEventResponce(ModelEventCoordinates modelEventCoordinates)
        {
            if (modelEventCoordinates.IsValid())
            {
                await _eventRepository.UpdateModelEventResponce(modelEventCoordinates);
            }
            else
            {
                throw new Exception("Invalid object");
            }   
        }
    }
}
