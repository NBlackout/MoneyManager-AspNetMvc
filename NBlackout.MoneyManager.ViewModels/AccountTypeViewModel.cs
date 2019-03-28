using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public enum AccountTypeViewModel
    {
        [Display(Name = "Labels_Checking", ResourceType = typeof(Resource))]
        Checking = 0,

        [Display(Name = "Labels_Saving", ResourceType = typeof(Resource))]
        Saving = 1,

        [Display(Name = "Labels_CertificateOfDeposit", ResourceType = typeof(Resource))]
        CertificateOfDeposit = 2
    }
}