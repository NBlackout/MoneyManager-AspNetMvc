var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var AccountCategories;
            (function(AccountCategories) {
                var List;
                (function(List) {
                    List.DataGrid = new NBlackout.UI.AjaxContainer();
                })(List = NBlackout.MoneyManager.Administration.AccountCategories.List || (NBlackout.MoneyManager.Administration.AccountCategories.List = {}));
            })(AccountCategories = NBlackout.MoneyManager.Administration.AccountCategories || (NBlackout.MoneyManager.Administration.AccountCategories = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleAccountCategoriesListEvents() {
    $(document).on('account-categories-updated', function() {
        NBlackout.MoneyManager.Administration.AccountCategories.List.DataGrid.refresh();
    });

    NBlackout.MoneyManager.Administration.AccountCategories.List.DataGrid.load();
}