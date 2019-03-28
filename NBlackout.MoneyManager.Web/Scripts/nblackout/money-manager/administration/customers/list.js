var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var Customers;
            (function(Customers) {
                var List;
                (function(List) {
                    List.DataGrid = new NBlackout.UI.AjaxContainer();
                })(List = NBlackout.MoneyManager.Administration.Customers.List || (NBlackout.MoneyManager.Administration.Customers.List = {}));
            })(Customers = NBlackout.MoneyManager.Administration.Customers || (NBlackout.MoneyManager.Administration.Customers = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleCustomersListEvents() {
    $(document).on('customers-updated', function() {
        NBlackout.MoneyManager.Administration.Customers.List.DataGrid.refresh();
    });

    NBlackout.MoneyManager.Administration.Customers.List.DataGrid.load();
}