using Microsoft.AspNetCore.Mvc;
using ModelAgency_Api.Models;
using ModelAgency_Api.Services;

namespace ModelAgency_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet()]
        public async Task<IEnumerable<SortedReportData>> GetReportData()
        {
            var events = await _reportService.GetReportData();

            return events;
        }
    }
}
