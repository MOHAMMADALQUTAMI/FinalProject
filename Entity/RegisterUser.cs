using System.ComponentModel.DataAnnotations;

namespace FinalProject.Entity
{
    public class RegisterUser
    {
       
            [Required]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match")]
            public string ConfirmPassword { get; set; }
            public bool IsAdmin { get; set; }
        
    }
}