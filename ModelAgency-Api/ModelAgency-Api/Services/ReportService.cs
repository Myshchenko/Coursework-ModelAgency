using ModelAgency_Api.Models;
using ModelAgency_Api.Repositories;

namespace ModelAgency_Api.Services
{
    public interface IReportService
    {
        Task<List<SortedReportData>> GetReportData();
    }

    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }


        public async Task<List<SortedReportData>> GetReportData()
        {
            var reports = await _reportRepository.GetReportData();

            var availableReports = new List<ReportData>();
            availableReports = reports.DistinctBy(r => r.Details).ToList();

            var sortedReports = new List<SortedReportData>();

            foreach (var item in availableReports)
            {
                var sortedReport = new SortedReportData();
                sortedReport.Details = item.Details;
                sortedReport.Address = item.Address;
                sortedReport.TargetDate = item.TargetDate;

                sortedReport.AcceptedUsers = reports.Where(r => r.Responce == "Accepted" && r.Details == item.Details).Select(u => u.UserName + " " + u.UserSurname).ToList();
                sortedReport.DeclinedUsers = reports.Where(r => r.Responce == "Declined" && r.Details == item.Details).Select(u => u.UserName + " " + u.UserSurname).ToList();
                sortedReport.NotReviewedUsers = reports.Where(r => r.Responce == "Not reviewed" && r.Details == item.Details).Select(u => u.UserName + " " + u.UserSurname).ToList();

                sortedReports.Add(sortedReport);
            }

            return sortedReports;
        }
    }
}
