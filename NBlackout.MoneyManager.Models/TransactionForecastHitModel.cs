using System;

namespace NBlackout.MoneyManager.Models
{
    public class TransactionForecastHitModel : BaseModel
    {
        public long ForecastId { get; set; }

        public DateTime Date { get; set; }

        public long? TransactionId { get; set; }

        public virtual TransactionForecastModel Forecast { get; set; }

        public virtual TransactionModel Transaction { get; set; }
    }
}
