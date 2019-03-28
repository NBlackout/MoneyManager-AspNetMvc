using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class AutomationElementViewModel : BaseViewModel
    {
        [Display(Name = "Labels_Organization", ResourceType = typeof(Resource))]
        public long OrganizationId { get; set; }

        [Display(Name = "Labels_Account", ResourceType = typeof(Resource))]
        public long AccountId { get; set; }

        [Display(Name = "Labels_Amount", ResourceType = typeof(Resource))]
        public decimal? Amount { get; set; }

        [Display(Name = "Labels_Tolerance", ResourceType = typeof(Resource))]
        public decimal? Tolerance { get; set; }

        public OrganizationViewModel Organization { get; set; }

        public AccountViewModel Account { get; set; }

        public AutomationElementTagViewModel[] Tags { get; set; }

        #region Collections

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }

        public IEnumerable<AccountViewModel> Accounts { get; set; }

        #endregion
    }
}