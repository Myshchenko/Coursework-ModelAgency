using Microsoft.AspNetCore.Mvc;

namespace ModelAgency_Api.Models
{
    public class Model:User
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public double Chest { get; set; }
        public double Waist { get; set; }
        public double Hips { get; set; }
        public double Shoes { get; set; }
        public string Hair { get; set; } = string.Empty;
    }
}
