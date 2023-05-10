using Microsoft.AspNetCore.Mvc;

namespace ModelAgency_Api.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBith { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double Chest { get; set; }
        public double Waist { get; set; }
        public double Hips { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}
