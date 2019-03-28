using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Display(Name = "Labels_Numéro de téléphone", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Numéro de téléphone requis", ErrorMessageResourceType = typeof(Resource))]
        [Phone(ErrorMessageResourceName = "Numéro de téléphone au format incorrect", ErrorMessageResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        [Display(Name = "Labels_Code", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Code requis", ErrorMessageResourceType = typeof(Resource))]
        public string Code { get; set; }
    }
}