var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var Customers;
            (function(Customers) {
                var Index;
                (function(Index) {
                    Index.List = new NBlackout.UI.AjaxContainer();
                })(Index = NBlackout.MoneyManager.Administration.Customers.Index || (NBlackout.MoneyManager.Administration.Customers.Index = {}));
            })(Customers = NBlackout.MoneyManager.Administration.Customers || (NBlackout.MoneyManager.Administration.Customers = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleCustomersIndexEvents() {
    NBlackout.MoneyManager.Administration.Customers.Index.List.load();
}