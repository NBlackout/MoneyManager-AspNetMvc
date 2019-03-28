var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Consultation;
        (function(Consultation) {
            var Accounts;
            (function(Accounts) {
                var List;
                (function(List) {
                    List.DataGrid = new NBlackout.UI.AjaxContainer();
                })(List = NBlackout.MoneyManager.Consultation.Accounts.List || (NBlackout.MoneyManager.Consultation.Accounts.List = {}));
            })(Accounts = NBlackout.MoneyManager.Consultation.Accounts || (NBlackout.MoneyManager.Consultation.Accounts = {}));
        })(Consultation = NBlackout.MoneyManager.Consultation || (NBlackout.MoneyManager.Consultation = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleAccountsListEvents() {
    NBlackout.MoneyManager.Consultation.Accounts.List.DataGrid.load();
}