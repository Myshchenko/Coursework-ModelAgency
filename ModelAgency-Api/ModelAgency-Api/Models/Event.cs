using ModelAgency_Api.Settings;
using Newtonsoft.Json;

namespace ModelAgency_Api.Models
{
    public class Event : IValidator<Event>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; } = 0;
        public string Details { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string EventType { get; set; } = string.Empty;

        public DateTime TargetDate { get; set; }
        public string Address { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.Details))
            {
                return false;
            }

            if(this.CreatedBy <= 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.EventType))
            {
                return false;
            }

            if(this.TargetDate < DateTime.Today || this.TargetDate == DateTime.Now)
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Address))
            {
                return false;
            }

            if(this.CreatedAt > DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}