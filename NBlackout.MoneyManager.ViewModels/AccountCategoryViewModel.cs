using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class AccountCategoryViewModel : BaseViewModel
    {
        [Display(Name = "Labels_Type", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_TypeRequired", ErrorMessageResourceType = typeof(Resource))]
        public AccountTypeViewModel Type { get; set; }

        [Display(Name = "Labels_Label", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_LabelRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Label { get; set; }

        #region Read-only

        public string TypeDisplayName { get { return Type.DisplayName(); } }

        #endregion
    }
}