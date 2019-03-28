using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class ReassignAccountCategoryViewModel
    {
        [Required(ErrorMessageResourceName = "Messages_SourceCategoryRequired", ErrorMessageResourceType = typeof(Resource))]
        public long SourceCategoryId { get; set; }

        public long? TargetCategoryId { get; set; }

        public IEnumerable<AccountCategoryViewModel> Categories { get; set; }
    }
}
