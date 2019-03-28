using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class ReassignOrganizationCategoryViewModel
    {
        [Required(ErrorMessageResourceName = "Messages_SourceCategoryRequired", ErrorMessageResourceType = typeof(Resource))]
        public long SourceCategoryId { get; set; }

        [Required(ErrorMessageResourceName = "Messages_TargetCategoryRequired", ErrorMessageResourceType = typeof(Resource))]
        public long TargetCategoryId { get; set; }

        public IEnumerable<OrganizationCategoryViewModel> Categories { get; set; }
    }
}
