using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class UpdateEmailViewModel
    {
        [Display(Name = "Labels_Email", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_EmailRequired", ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessage = "Messages_EmailInvalid", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = "Labels_Confirmation", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_ConfirmationRequired", ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessage = "Messages_EmailInvalid", ErrorMessageResourceType = typeof(Resource))]
        public string EmailConfirmation { get; set; }
    }
}