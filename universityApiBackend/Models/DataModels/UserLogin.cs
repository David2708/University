using System.ComponentModel.DataAnnotations;

namespace universityApiBackend.Models.DataModels
{
    public class UserLogin
    {
        [Required]
        public string USerName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
