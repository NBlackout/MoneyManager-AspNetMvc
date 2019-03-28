var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var OrganizationCategories;
            (function(OrganizationCategories) {
                var Index;
                (function(Index) {
                    Index.List = new NBlackout.UI.AjaxContainer();
                })(Index = NBlackout.MoneyManager.Administration.OrganizationCategories.Index || (NBlackout.MoneyManager.Administration.OrganizationCategories.Index = {}));
            })(OrganizationCategories = NBlackout.MoneyManager.Administration.OrganizationCategories || (NBlackout.MoneyManager.Administration.OrganizationCategories = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleOrganizationCategoriesIndexEvents() {
    NBlackout.MoneyManager.Administration.OrganizationCategories.Index.List.load();
}