using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class UpdatePasswordViewModel
    {
        [Display(Name = "Labels_OldPassword", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_OldPasswordRequired", ErrorMessageResourceType = typeof(Resource))]
        public string OldPassword { get; set; }

        [Display(Name = "Labels_Password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_PasswordRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Display(Name = "Labels_Confirmation", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_ConfirmationRequired", ErrorMessageResourceType = typeof(Resource))]
        public string PasswordConfirmation { get; set; }
    }
}