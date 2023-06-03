using ModelAgency_Api.Settings;

namespace ModelAgency_Api.Models
{
    public class User : IValidator<User>
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBith { get; set; }
        public int GenderId { get; set; }
        public int RoleId { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Surname))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }

            if (this.DateOfBith >= DateTime.Today.AddYears(-18))
            {
                return false;
            }

            return true;
        }
    }
}
