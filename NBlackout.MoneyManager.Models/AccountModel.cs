using System;
using System.Collections.Generic;

namespace NBlackout.MoneyManager.Models
{
    public class AccountModel : BaseModel
    {
        public long? CustomerId { get; set; }

        public long? CategoryId { get; set; }

        public string Title { get; set; }

        public string Number { get; set; }

        public decimal Balance { get; set; }

        public string TechnicalId { get; set; }

        public DateTime? LastSync { get; set; }

        public bool Enabled { get; set; }

        public long? ImportHistoryId { get; set; }

        public virtual CustomerModel Customer { get; set; }

        public virtual AccountCategoryModel Category { get; set; }

        public virtual ICollection<TransactionModel> Transactions { get; set; }

        public virtual ImportHistoryModel ImportHistory { get; set; }
    }
}