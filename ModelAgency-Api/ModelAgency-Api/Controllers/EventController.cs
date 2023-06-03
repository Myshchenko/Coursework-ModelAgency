using Microsoft.AspNetCore.Mvc;
using ModelAgency_Api.Models;
using ModelAgency_Api.Services;
using System.Collections;

namespace ModelAgency_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet()]
        public async Task<IEnumerable<Event>> GetEvents()
        {
            var events = await _eventService.GetEvents();

            return events;
        }

        [HttpGet("eventsForModel")]
        public async Task<IEnumerable<ModelEvent>> GetEventsForModel(int id)
        {
            return await _eventService.GetEventsForModel(id);
        }

        [HttpGet("availableEventsForSpecificModel")]
        public async Task<IEnumerable<Event>> GetAvailableEventsForSpecificModel(int modelId)
        {
            return await _eventService.GetAvailableEventsForSpecificModel(modelId);
        }

        [HttpPost]
        [Route("addModelToTheEvent")]
        public async Task AddModelToTheEvent(ModelEventCoordinates modelEventCoordinates)
        {
            await _eventService.AddModelToTheEvent(modelEventCoordinates);
        }

        [HttpPost]
        [Route("add")]
        public async Task AddEvents(Event specificEvent)
        {
            await _eventService.AddEvent(specificEvent);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateEvent(Event modelEvent)
        {
            await _eventService.UpdateEvent(modelEvent);
        }

        [HttpPut]
        [Route("updateModelEventResponce")]
        public async Task UpdateModelEventResponce(ModelEventCoordinates modelEventCoordinates)
        {
            await _eventService.UpdateModelEventResponce(modelEventCoordinates);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task DeleteEvent(int Id)
        {
            await _eventService.DeleteEvent(Id);
        }
    }
}
