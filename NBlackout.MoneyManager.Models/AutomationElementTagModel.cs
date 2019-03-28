namespace NBlackout.MoneyManager.Models
{
    public class AutomationElementTagModel : BaseModel
    {
        public string Label { get; set; }

        public long AutomationElementId { get; set; }

        public virtual AutomationElementModel AutomationElement { get; set; }
    }
}
