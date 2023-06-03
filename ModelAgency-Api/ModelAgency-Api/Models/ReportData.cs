namespace ModelAgency_Api.Models
{
    public class ReportData
    {
        public string Details { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime TargetDate { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserSurname { get; set; } = string.Empty;
        public string Responce { get; set; } = string.Empty;
    }
}
