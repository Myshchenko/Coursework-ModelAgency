using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ModelAgency_Api.Models
{
    public class Event
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; } = 0;
        public string Details { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string EventType { get; set; } = string.Empty;

        public DateTime TargetDate { get; set; }
        public string Address { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }
    }
}
