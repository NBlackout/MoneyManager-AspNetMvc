using System.Collections.Generic;
using System.Linq;

namespace NBlackout.MoneyManager.Models
{
    public class TransactionForecastModel : BaseModel
    {
        public long AccountId { get; set; }

        public long OrganizationId { get; set; }

        public bool AutoReschedule { get; set; }

        public virtual AccountModel Account { get; set; }

        public virtual OrganizationModel Organization { get; set; }

        public virtual ICollection<TransactionForecastHitModel> Hits { get; set; }

        #region Read-only

        public AutomationElementModel AutomationElement
        {
            get { return Organization?.AutomationElements.SingleOrDefault(t => t.AccountId == AccountId); }
        }

        #endregion
    }
}
