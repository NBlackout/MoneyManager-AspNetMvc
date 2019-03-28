var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var AccountCategories;
            (function(AccountCategories) {
                var Index;
                (function(Index) {
                    Index.List = new NBlackout.UI.AjaxContainer();
                })(Index = NBlackout.MoneyManager.Administration.AccountCategories.Index || (NBlackout.MoneyManager.Administration.AccountCategories.Index = {}));
            })(AccountCategories = NBlackout.MoneyManager.Administration.AccountCategories || (NBlackout.MoneyManager.Administration.AccountCategories = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleAccountCategoriesIndexEvents() {
    NBlackout.MoneyManager.Administration.AccountCategories.Index.List.load();
}