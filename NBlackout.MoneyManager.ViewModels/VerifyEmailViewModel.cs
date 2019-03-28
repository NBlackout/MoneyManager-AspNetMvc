using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessageResourceName = "Messages_UserRequired", ErrorMessageResourceType = typeof(Resource))]
        public long UserId { get; set; }

        [Display(Name = "Labels_Token", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Messages_TokenRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Token { get; set; }
    }
}