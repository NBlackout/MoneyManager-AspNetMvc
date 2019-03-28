using System.Collections.Generic;

namespace NBlackout.MoneyManager.Models.Results
{
    public class ImportResult
    {
        public ICollection<AccountModel> Accounts { get; set; }
        public ICollection<TransactionModel> Transactions { get; set; }

        public ImportResult()
        {
            Accounts = new List<AccountModel>();
            Transactions = new List<TransactionModel>();
        }
    }
}
