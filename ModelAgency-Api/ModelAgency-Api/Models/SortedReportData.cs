namespace ModelAgency_Api.Models
{
    public class SortedReportData
    {
        public string Details { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime TargetDate { get; set; }

        public List<string> AcceptedUsers { get; set; }
        public List<string> DeclinedUsers { get; set; }
        public List<string> NotReviewedUsers { get; set; }
        //public string UserName { get; set; } = string.Empty;
        //public string UserSurname { get; set; } = string.Empty;
        //public string Responce { get; set; } = string.Empty;
    }
}
