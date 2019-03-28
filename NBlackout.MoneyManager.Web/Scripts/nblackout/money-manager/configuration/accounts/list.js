var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Accounts;
            (function(Accounts) {
                var List;
                (function(List) {
                    List.DataGrid = new NBlackout.UI.AjaxContainer();
                })(List = NBlackout.MoneyManager.Configuration.Accounts.List || (NBlackout.MoneyManager.Configuration.Accounts.List = {}));
            })(Accounts = NBlackout.MoneyManager.Configuration.Accounts || (NBlackout.MoneyManager.Configuration.Accounts = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleAccountsListEvents() {
    $(document).on('accounts-updated', function() {
        NBlackout.MoneyManager.Configuration.Accounts.List.DataGrid.refresh();
    });

    NBlackout.MoneyManager.Configuration.Accounts.List.DataGrid.load();
}