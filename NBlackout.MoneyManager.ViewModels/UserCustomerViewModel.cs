namespace NBlackout.MoneyManager.ViewModels
{
    public class UserCustomerViewModel : BaseViewModel
    {
        public string UserName { get; set; }

        public long CustomerId { get; set; }

        public CustomerViewModel Customer { get; set; }
    }
}
