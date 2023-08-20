using System.ComponentModel.DataAnnotations;

namespace FinalProject.Entity
{
    public class LoginUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}