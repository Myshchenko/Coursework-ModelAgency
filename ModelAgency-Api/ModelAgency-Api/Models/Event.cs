namespace ModelAgency_Api.Models
{
    public class Event
    {
        public int Id { get; set; }
        public EventType EventType { get; set; }

        public DateTime TargetDate { get; set; }
        public string Address { get; set; } = String.Empty;

        public DateTime? CreatedAt { get; set; }
    }
}
