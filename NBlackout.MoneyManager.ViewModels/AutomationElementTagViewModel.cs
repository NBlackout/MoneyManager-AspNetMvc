namespace NBlackout.MoneyManager.ViewModels
{
    public class AutomationElementTagViewModel : BaseViewModel
    {
        public string Label { get; set; }

        public long AutomationElementId { get; set; }

        public AutomationElementViewModel AutomationElement { get; set; }
    }
}
