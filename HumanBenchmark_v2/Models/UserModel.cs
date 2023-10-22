using System.ComponentModel.DataAnnotations;

namespace HumanBenchmark_v2.Models
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
