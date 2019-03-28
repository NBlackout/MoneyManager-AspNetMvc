using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class OrganizationCategoryViewModel : BaseViewModel
    {
        [Display(Name = "Labels_Label", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_LabelRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Label { get; set; }

        [Display(Name = "Labels_Category", ResourceType = typeof(Resource))]
        public long? CategoryId { get; set; }

        [Display(Name = "Labels_Type", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_TypeRequired", ErrorMessageResourceType = typeof(Resource))]
        public TransactionTypeViewModel Type { get; set; }

        [Display(Name = "Labels_Recurrent", ResourceType = typeof(Resource))]
        public bool Recurrent { get; set; }

        public OrganizationCategoryViewModel Category { get; set; }

        public IEnumerable<OrganizationCategoryViewModel> Categories { get; set; }

        #region Collections

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }

        #endregion

        #region Read-only

        public string FullLabel { get { return ((Category != null) ? Category.Label + "/" : String.Empty) + Label; } }

        public string TypeDisplayName { get { return Type.DisplayName(); } }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var o = obj as OrganizationCategoryViewModel;

            return Type == o.Type && Label == o.Label;
        }

        public override int GetHashCode()
        {
            var labelHashCode = (Label != null) ? Label.GetHashCode() : 0;

            return Type.GetHashCode() ^ labelHashCode;
        }
    }
}