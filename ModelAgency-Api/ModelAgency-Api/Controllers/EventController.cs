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

        [HttpPost]
        [Route("add")]
        public async Task AddEvents(Event modelEvent)
        {
            await _eventService.AddEvent(modelEvent);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateEvent(Event modelEvent)
        {
            await _eventService.UpdateEvent(modelEvent);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task DeleteEvent(int Id)
        {
            await _eventService.DeleteEvent(Id);
        }
    }
}
