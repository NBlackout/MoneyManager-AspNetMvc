using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class ReassignOrganizationViewModel
    {
        [Required(ErrorMessageResourceName = "Messages_SourceOrganizationRequired", ErrorMessageResourceType = typeof(Resource))]
        public long SourceOrganizationId { get; set; }

        public long? TargetOrganizationId { get; set; }

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }
    }
}
