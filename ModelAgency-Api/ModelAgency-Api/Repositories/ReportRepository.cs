using Microsoft.Data.Sqlite;
using ModelAgency_Api.Models;

namespace ModelAgency_Api.Repositories
{
    public interface IReportRepository
    {
        Task<List<ReportData>> GetReportData();
    }

    public class ReportRepository : IReportRepository
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public ReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            string? conStr = _configuration["ConnectionString"];
            if (!string.IsNullOrEmpty(conStr))
            {
                connectionString = conStr;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<List<ReportData>> GetReportData()
        {
            const string getDataForReport = @"SELECT Event.Details, Event.Address, Event.TargetDate, User.Name, User.Surname, AcceptingType.Name
                                              FROM Event
                                              join ModelEvent on Event.Id = ModelEvent.EventId
                                              join User on ModelEvent.ModelId = User.Id
                                              join AcceptingType on ModelEvent.IsAccepted = AcceptingType.Id";

            List<ReportData> listReportData = new List<ReportData>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = getDataForReport;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var reportData = new ReportData();

                        reportData.Details = reader.GetString(0);
                        reportData.Address = reader.GetString(1);
                        reportData.TargetDate = DateTime.Parse(reader.GetString(2));
                        reportData.UserName = reader.GetString(3);
                        reportData.UserSurname = reader.GetString(4);
                        reportData.Responce = reader.GetString(5);

                        listReportData.Add(reportData);
                    }
                }
            }

            return listReportData;
        }
    }
}
