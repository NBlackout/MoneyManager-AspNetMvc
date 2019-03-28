using System.Collections.Generic;

namespace NBlackout.MoneyManager.Models
{
    public class OrganizationModel : BaseModel
    {
        public long CategoryId { get; set; }

        public string Label { get; set; }

        public virtual OrganizationCategoryModel Category { get; set; }

        public virtual ICollection<AutomationElementModel> AutomationElements { get; set; }
    }
}