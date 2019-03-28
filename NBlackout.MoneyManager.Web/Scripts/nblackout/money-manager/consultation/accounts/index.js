var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Consultation;
        (function(Consultation) {
            var Accounts;
            (function(Accounts) {
                var Index;
                (function(Index) {
                    Index.List = new NBlackout.UI.AjaxContainer();
                })(Index = NBlackout.MoneyManager.Consultation.Accounts.Index || (NBlackout.MoneyManager.Consultation.Accounts.Index = {}));
            })(Accounts = NBlackout.MoneyManager.Consultation.Accounts || (NBlackout.MoneyManager.Consultation.Accounts = {}));
        })(Consultation = NBlackout.MoneyManager.Consultation || (NBlackout.MoneyManager.Consultation = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleAccountsIndexEvents() {
    NBlackout.MoneyManager.Consultation.Accounts.Index.List.load();
}