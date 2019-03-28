using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public enum TransactionTypeViewModel
    {
        [Display(Name = "Labels_Expenditure", ResourceType = typeof(Resource))]
        Expenditure = 0,

        [Display(Name = "Labels_Income", ResourceType = typeof(Resource))]
        Income = 1
    }
}
