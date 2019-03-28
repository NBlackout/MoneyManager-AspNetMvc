using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class AuthenticationViewModel
    {
        [Display(Name = "Labels_UserName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_UserNameRequired", ErrorMessageResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = "Labels_Password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_PasswordRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}