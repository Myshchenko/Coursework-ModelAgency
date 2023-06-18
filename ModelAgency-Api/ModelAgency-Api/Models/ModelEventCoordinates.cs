using ModelAgency_Api.Settings;

namespace ModelAgency_Api.Models
{
    public class ModelEventCoordinates : IValidator<ModelEventCoordinates>
    {
        public int EventId { get; set; }
        public int ModelId { get; set; }
        public ModelEventResponceType ModelEventResponceType { get; set; }

        public bool IsValid()
        {
            if(this.EventId <= 0)
            {
                return false;
            }

            if(this.ModelId <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
