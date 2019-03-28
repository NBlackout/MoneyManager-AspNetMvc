namespace NBlackout.MoneyManager.Models
{
    public class UserCustomerModel : BaseModel
    {
        public string UserName { get; set; }

        public long CustomerId { get; set; }

        public virtual CustomerModel Customer { get; set; }
    }
}
