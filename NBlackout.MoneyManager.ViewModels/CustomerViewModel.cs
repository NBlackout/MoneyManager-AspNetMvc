using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        [Display(Name = "Labels_FirstName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_FirstNameRequired", ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = "Labels_LastName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_LastNameRequired", ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        public IEnumerable<UserCustomerViewModel> UserCustomers { get; set; }

        #region Read-only

        [Display(Name = "Labels_FullName", ResourceType = typeof(Resource))]
        public string FullName
        {
            get { return String.Concat(FirstName, " ", LastName); }
        }

        #endregion
    }
}