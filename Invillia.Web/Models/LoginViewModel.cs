using System.ComponentModel.DataAnnotations;

namespace Invillia.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
