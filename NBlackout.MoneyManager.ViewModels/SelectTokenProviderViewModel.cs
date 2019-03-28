using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class SelectTokenProviderViewModel
    {
        [Display(Name = "Labels_Provider", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_ProviderRequired", ErrorMessageResourceType = typeof(Resource))]
        public string TokenProvider { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<string> TokenProviders { get; set; }
    }
}
