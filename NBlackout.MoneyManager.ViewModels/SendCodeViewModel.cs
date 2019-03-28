using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class SendCodeViewModel
    {
        [Required(ErrorMessageResourceName = "Messages_ProviderRequired", ErrorMessageResourceType = typeof(Resource))]
        public string TokenProvider { get; set; }

        [Display(Name = "Labels_Code", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_CodeRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }
    }
}
