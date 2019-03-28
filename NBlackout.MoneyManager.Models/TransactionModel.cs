using System;

namespace NBlackout.MoneyManager.Models
{
    public class TransactionModel : BaseModel
    {
        public long AccountId { get; set; }

        public long? OrganizationId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public string Label { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public long? ImportHistoryId { get; set; }

        public virtual AccountModel Account { get; set; }

        public virtual OrganizationModel Organization { get; set; }

        public virtual ImportHistoryModel ImportHistory { get; set; }
    }
}