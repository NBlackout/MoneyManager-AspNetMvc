using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class ReassignCustomerViewModel
    {
        [Required(ErrorMessageResourceName = "Messages_SourceCustomerRequired", ErrorMessageResourceType = typeof(Resource))]
        public long SourceCustomerId { get; set; }

        public long? TargetCustomerId { get; set; }

        public IEnumerable<CustomerViewModel> Customers { get; set; }
    }
}
