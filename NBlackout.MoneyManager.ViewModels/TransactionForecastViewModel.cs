using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class TransactionForecastViewModel : BaseViewModel
    {
        [Display(Name = "Labels_Account", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_AccountRequired", ErrorMessageResourceType = typeof(Resource))]
        public long AccountId { get; set; }

        [Display(Name = "Labels_Organization", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_OrganizationRequired", ErrorMessageResourceType = typeof(Resource))]
        public long OrganizationId { get; set; }

        [Display(Name = "Labels_AutoReschedule", ResourceType = typeof(Resource))]
        public bool AutoReschedule { get; set; }

        public AccountViewModel Account { get; set; }

        public OrganizationViewModel Organization { get; set; }

        public IEnumerable<TransactionForecastHitViewModel> Hits { get; set; }

        #region Collections

        public IEnumerable<AccountViewModel> Accounts { get; set; }

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }

        #endregion

        #region Read-only

        public AutomationElementViewModel AutomationElement
        {
            get { return Organization.AutomationElements.SingleOrDefault(t => t.AccountId == AccountId); }
        }

        #endregion
    }
}
