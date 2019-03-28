using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        [Display(Name = "Labels_Customer", ResourceType = typeof(Resource))]
        public long? CustomerId { get; set; }

        [Display(Name = "Labels_Category", ResourceType = typeof(Resource))]
        public long? CategoryId { get; set; }

        [Display(Name = "Labels_Title", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_TitleRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Title { get; set; }

        [Display(Name = "Labels_Number", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_NumberRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Number { get; set; }

        [Display(Name = "Labels_Balance", ResourceType = typeof(Resource))]
        public decimal Balance { get; set; }

        public string TechnicalId { get; set; }

        [Display(Name = "Labels_LastSynchronization", ResourceType = typeof(Resource))]
        public DateTime? LastSync { get; set; }

        [Display(Name = "Labels_Enabled", ResourceType = typeof(Resource))]
        public bool Enabled { get; set; }

        [Display(Name = "Labels_EstimatedBalanceByEndOfMonth", ResourceType = typeof(Resource))]
        public decimal EstimatedBalanceByEndOfMonth { get; set; }

        public CustomerViewModel Customer { get; set; }

        public AccountCategoryViewModel Category { get; set; }

        public IEnumerable<TransactionViewModel> Transactions { get; set; }

        #region Collections

        public IEnumerable<CustomerViewModel> Customers { get; set; }

        public IEnumerable<AccountCategoryViewModel> Categories { get; set; }

        #endregion
    }
}