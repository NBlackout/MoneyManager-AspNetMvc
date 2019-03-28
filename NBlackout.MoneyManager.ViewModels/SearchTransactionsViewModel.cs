using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class SearchTransactionsViewModel
    {
        [Display(Name = "Labels_Account", ResourceType = typeof(Resource))]
        public long? AccountId { get; set; }

        [Display(Name = "Labels_Organization", ResourceType = typeof(Resource))]
        public long? OrganizationId { get; set; }

        [Display(Name = "Labels_Label", ResourceType = typeof(Resource))]
        public string Label { get; set; }

        #region Collections

        public IEnumerable<AccountViewModel> Accounts { get; set; }

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }

        #endregion
    }
}