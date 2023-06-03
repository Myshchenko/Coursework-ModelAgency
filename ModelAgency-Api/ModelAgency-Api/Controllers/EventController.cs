using Microsoft.AspNetCore.Mvc;
using ModelAgency_Api.Models;
using ModelAgency_Api.Services;

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
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            try
            {
                var events = await _eventService.GetEvents();

                if(events != null)
                {
                    return events;
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("eventsForModel")]
        public async Task<ActionResult<IEnumerable<ModelEvent>>> GetEventsForModel(int id)
        {
            try
            {
                var events = await _eventService.GetEventsForModel(id);

                if (events != null)
                {
                    return events;
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("availableEventsForSpecificModel")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAvailableEventsForSpecificModel(int modelId)
        {
            try
            {
                var events = await _eventService.GetAvailableEventsForSpecificModel(modelId);

                if (events != null)
                {
                    return events;
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addModelToTheEvent")]
        public async Task<IActionResult> AddModelToTheEvent(ModelEventCoordinates modelEventCoordinates)
        {
            try
            {
                await _eventService.AddModelToTheEvent(modelEventCoordinates);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEvent(Event specificEvent)
        {
            try
            {
                await _eventService.AddEvent(specificEvent);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateEvent(Event modelEvent)
        {
            try
            {
                await _eventService.UpdateEvent(modelEvent);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("updateModelEventResponce")]
        public async Task<IActionResult> UpdateModelEventResponce(ModelEventCoordinates modelEventCoordinates)
        {
            try
            {
                await _eventService.UpdateModelEventResponce(modelEventCoordinates);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteEvent(int Id)
        {
            try
            {
                await _eventService.DeleteEvent(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
