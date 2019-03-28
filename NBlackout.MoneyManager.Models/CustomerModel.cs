using System.Collections.Generic;

namespace NBlackout.MoneyManager.Models
{
    public class CustomerModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<UserCustomerModel> UserCustomers { get; set; }
    }
}