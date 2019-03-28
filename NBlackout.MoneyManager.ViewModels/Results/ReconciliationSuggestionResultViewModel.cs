using System;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels.Results
{
    public class ReconciliationSuggestionResultViewModel
    {
        public long TransactionForecastHitId { get; set; }

        [Display(Name = "Labels_Account", ResourceType = typeof(Resource))]
        public string TransactionForecastAccountTitle { get; set; }

        [Display(Name = "Labels_Organization", ResourceType = typeof(Resource))]
        public string TransactionForecastOrganizationLabel { get; set; }

        [Display(Name = "Labels_ExpectedAmount", ResourceType = typeof(Resource))]
        public decimal? TransactionForecastOrganizationAmount { get; set; }

        [Display(Name = "Labels_Tolerance", ResourceType = typeof(Resource))]
        public decimal? TransactionForecastOrganizationTolerance { get; set; }

        [Display(Name = "Labels_ExpectedDate", ResourceType = typeof(Resource))]
        public DateTime TransactionForecastHitDate { get; set; }

        public long TransactionId { get; set; }

        [Display(Name = "Labels_Label", ResourceType = typeof(Resource))]
        public string TransactionLabel { get; set; }

        [Display(Name = "Labels_RealDate", ResourceType = typeof(Resource))]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Labels_RealAmount", ResourceType = typeof(Resource))]
        public decimal TransactionAmount { get; set; }
    }
}
