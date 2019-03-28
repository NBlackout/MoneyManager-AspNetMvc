using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class UpdatePhoneNumberViewModel
    {
        [Display(Name = "Labels_PhoneNumber", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_PhoneNumberRequired", ErrorMessageResourceType = typeof(Resource))]
        [Phone(ErrorMessageResourceName = "Messages_PhoneNumberInvalid", ErrorMessageResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        [Display(Name = "Labels_Confirmation", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_ConfirmationRequired", ErrorMessageResourceType = typeof(Resource))]
        [Phone(ErrorMessageResourceName = "Messages_PhoneNumberInvalid", ErrorMessageResourceType = typeof(Resource))]
        public string PhoneNumberConfirmation { get; set; }
    }
}