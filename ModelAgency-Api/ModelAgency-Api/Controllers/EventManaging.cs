using Microsoft.AspNetCore.Mvc;

namespace ModelAgency_Api.Controllers
{
    public class EventManaging : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
