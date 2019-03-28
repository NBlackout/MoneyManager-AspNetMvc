using System;
using System.Collections.Generic;

namespace NBlackout.MoneyManager.Models
{
    public class AutomationElementModel : BaseModel
    {
        public long OrganizationId { get; set; }

        public long AccountId { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Tolerance { get; set; }

        public virtual OrganizationModel Organization { get; set; }

        public virtual AccountModel Account { get; set; }

        public virtual ICollection<AutomationElementTagModel> Tags { get; set; }

        #region Read-only

        public decimal MinAmount { get { return (Amount.HasValue) ? (Amount.Value - EffectiveTolerance) : Decimal.MinValue; } }

        public decimal MaxAmount { get { return (Amount.HasValue) ? (Amount.Value + EffectiveTolerance) : Decimal.MaxValue; } }

        public decimal EffectiveTolerance { get { return (Tolerance.HasValue) ? Math.Abs(Tolerance.Value) : 0m; } }

        #endregion

        public AutomationElementModel()
        {
            Tags = new List<AutomationElementTagModel>();
        }
    }
}