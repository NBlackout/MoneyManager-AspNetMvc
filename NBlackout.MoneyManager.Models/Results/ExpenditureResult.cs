namespace NBlackout.MoneyManager.Models.Results
{
    public class ExpenditureResult
    {
        public long? OrganizationId { get; set; }

        public string OrganizationLabel { get; set; }

        public string CategoryLabel { get; set; }

        public decimal Amount { get; set; }
    }
}