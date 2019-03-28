var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Organizations;
            (function(Organizations) {
                var Index;
                (function(Index) {
                    Index.List = new NBlackout.UI.AjaxContainer();
                })(Index = NBlackout.MoneyManager.Configuration.Organizations.Index || (NBlackout.MoneyManager.Configuration.Organizations.Index = {}));
            })(Organizations = NBlackout.MoneyManager.Configuration.Organizations || (NBlackout.MoneyManager.Configuration.Organizations = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleOrganizationsIndexEvents() {
    NBlackout.MoneyManager.Configuration.Organizations.Index.List.load();
}