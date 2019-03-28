using System.Collections.Generic;

namespace NBlackout.MoneyManager.Models
{
    public class OrganizationCategoryModel : BaseModel
    {
        public string Label { get; set; }

        public long? CategoryId { get; set; }

        public TransactionTypeModel Type { get; set; }

        public bool Recurrent { get; set; }

        public virtual OrganizationCategoryModel Category { get; set; }

        public virtual ICollection<OrganizationCategoryModel> Categories { get; set; }

        public virtual ICollection<OrganizationModel> Organizations { get; set; }
    }
}