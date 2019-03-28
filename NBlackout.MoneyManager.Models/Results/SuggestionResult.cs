using System;

namespace NBlackout.MoneyManager.Models.Results
{
    public class SuggestionResult
    {
        public TransactionTypeModel? Category { get; set; }

        public long OrganizationId { get; set; }

        public string OrganizationLabel { get; set; }

        public long CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public long AccountId { get; set; }

        public string AccountTitle { get; set; }

        public long TransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionLabel { get; set; }

        public decimal TransactionAmount { get; set; }
    }
}