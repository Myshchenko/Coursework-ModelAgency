namespace ModelAgency_Api.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Details { get; set; } = string.Empty;

        public int CreatedBy { get; set; }
        public string EventType { get; set; } = string.Empty;

        public DateTime TargetDate { get; set; }
        public string Address { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }
    }
}
