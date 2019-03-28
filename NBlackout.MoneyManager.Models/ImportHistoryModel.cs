using System;
using System.Collections.Generic;

namespace NBlackout.MoneyManager.Models
{
    public class ImportHistoryModel : BaseModel
    {
        public string FileName { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserName { get; set; }

        public virtual ICollection<AccountModel> Accounts { get; set; }

        public virtual ICollection<TransactionModel> Transactions { get; set; }
    }
}
