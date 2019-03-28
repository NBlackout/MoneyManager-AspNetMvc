using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class TransactionViewModel : BaseViewModel
    {
        public long AccountId { get; set; }

        [Display(Name = "Labels_Organization", ResourceType = typeof(Resource))]
        public long? OrganizationId { get; set; }

        [Display(Name = "Labels_Number", ResourceType = typeof(Resource))]
        public string Number { get; set; }

        [Display(Name = "Labels_Date", ResourceType = typeof(Resource))]
        public DateTime Date { get; set; }

        [Display(Name = "Labels_Label", ResourceType = typeof(Resource))]
        public string Label { get; set; }

        [Display(Name = "Labels_Amount", ResourceType = typeof(Resource))]
        public decimal Amount { get; set; }

        [Display(Name = "Labels_Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        public OrganizationViewModel Organization { get; set; }

        #region Collections

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }

        #endregion
    }
}