using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class OrganizationViewModel : BaseViewModel
    {
        [Display(Name = "Labels_Category", ResourceType = typeof(Resource))]
        public long CategoryId { get; set; }

        [Display(Name = "Labels_Label", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_LabelRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Label { get; set; }

        public OrganizationCategoryViewModel Category { get; set; }

        public IEnumerable<AutomationElementViewModel> AutomationElements { get; set; }

        #region Collections

        public IEnumerable<OrganizationCategoryViewModel> Categories { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var o = obj as OrganizationViewModel;

            return Label == o.Label;
        }

        public override int GetHashCode()
        {
            var labelHashCode = (Label != null) ? Label.GetHashCode() : 0;

            return labelHashCode;
        }
    }
}