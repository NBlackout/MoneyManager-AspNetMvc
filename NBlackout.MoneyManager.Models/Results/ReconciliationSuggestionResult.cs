using System;

namespace NBlackout.MoneyManager.Models.Results
{
    public class ReconciliationSuggestionResult
    {
        public long TransactionForecastHitId { get; set; }

        public string TransactionForecastAccountTitle { get; set; }

        public string TransactionForecastOrganizationLabel { get; set; }

        public decimal? TransactionForecastOrganizationAmount { get; set; }

        public decimal? TransactionForecastOrganizationTolerance { get; set; }

        public DateTime TransactionForecastHitDate { get; set; }

        public long TransactionId { get; set; }

        public string TransactionLabel { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal TransactionAmount { get; set; }
    }
}
