using System;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class TransactionForecastHitViewModel : BaseViewModel
    {
        public long ForecastId { get; set; }

        [Display(Name = "Labels_Date", ResourceType = typeof(Resource))]
        public DateTime Date { get; set; }

        public long? TransactionId { get; set; }

        public TransactionForecastViewModel Forecast { get; set; }

        public TransactionViewModel Transaction { get; set; }
    }
}
