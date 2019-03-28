var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Accounts;
            (function(Accounts) {
                var Index;
                (function(Index) {
                    Index.List = new NBlackout.UI.AjaxContainer();
                })(Index = NBlackout.MoneyManager.Configuration.Accounts.Index || (NBlackout.MoneyManager.Configuration.Accounts.Index = {}));
            })(Accounts = NBlackout.MoneyManager.Configuration.Accounts || (NBlackout.MoneyManager.Configuration.Accounts = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleAccountsIndexEvents() {
    NBlackout.MoneyManager.Configuration.Accounts.Index.List.load();
}