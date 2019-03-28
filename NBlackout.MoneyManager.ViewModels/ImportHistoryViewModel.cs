using System;
using System.ComponentModel.DataAnnotations;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class ImportHistoryViewModel : BaseViewModel
    {
        [Display(Name = "Labels_FileName", ResourceType = typeof(Resource))]
        public string FileName { get; set; }

        [Display(Name = "Labels_BeginDate", ResourceType = typeof(Resource))]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Labels_EndDate", ResourceType = typeof(Resource))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Labels_User", ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        #region Read-only

        [Display(Name = "Labels_Duration", ResourceType = typeof(Resource))]
        public int Duration { get { return (int)(EndDate - BeginDate).TotalMilliseconds; } }

        #endregion
    }
}
