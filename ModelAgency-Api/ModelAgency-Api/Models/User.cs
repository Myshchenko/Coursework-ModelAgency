namespace ModelAgency_Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBith { get; set; }
        public int GenderId { get; set; }
        public int RoleId { get; set; }
    }
}
