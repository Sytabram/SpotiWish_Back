using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class CreateAccountRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}