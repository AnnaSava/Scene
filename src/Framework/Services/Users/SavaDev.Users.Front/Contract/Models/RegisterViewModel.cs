using SavaDev.Base.Users.Front.Models;
using SavaDev.Infrastructure.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SavaDev.Users.Front.Contract.Models
{
    public class RegisterViewModel : IRegisterViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [IsTrue]
        [Display(Name = "Принимаю условия пользовательского соглашения")]
        public bool AcceptUserConsent { get; set; }
    }
}
