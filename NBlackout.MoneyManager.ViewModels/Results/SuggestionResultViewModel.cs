using System;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels.Results
{
    public class SuggestionResultViewModel
    {
        public TransactionTypeViewModel? Category { get; set; }

        public long OrganizationId { get; set; }

        [Display(Name = "Labels_Organization", ResourceType = typeof(Resource))]
        public string OrganizationLabel { get; set; }

        public long CustomerId { get; set; }

        [Display(Name = "Labels_Customer", ResourceType = typeof(Resource))]
        public string CustomerFullName { get; set; }

        public long AccountId { get; set; }

        [Display(Name = "Labels_Account", ResourceType = typeof(Resource))]
        public string AccountTitle { get; set; }

        public long TransactionId { get; set; }

        [Display(Name = "Labels_Date", ResourceType = typeof(Resource))]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Labels_Label", ResourceType = typeof(Resource))]
        public string TransactionLabel { get; set; }

        [Display(Name = "Labels_Amount", ResourceType = typeof(Resource))]
        public decimal TransactionAmount { get; set; }
    }
}
