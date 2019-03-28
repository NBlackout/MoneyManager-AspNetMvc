namespace NBlackout.MoneyManager.Models
{
    public class AccountCategoryModel : BaseModel
    {
        public AccountTypeModel Type { get; set; }

        public string Label { get; set; }
    }
}