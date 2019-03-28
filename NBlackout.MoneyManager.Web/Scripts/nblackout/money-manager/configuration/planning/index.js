var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Planning;
            (function(Planning) {
                var Index;
                (function(Index) {
                    Index.Suggestions = new NBlackout.UI.AjaxContainer();
                    Index.ForecastHits = new NBlackout.UI.AjaxContainer();
                })(Index = NBlackout.MoneyManager.Configuration.Planning.Index || (NBlackout.MoneyManager.Configuration.Planning.Index = {}));
            })(Planning = NBlackout.MoneyManager.Configuration.Planning || (NBlackout.MoneyManager.Configuration.Planning = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handlePlanningIndexEvents() {
    NBlackout.MoneyManager.Configuration.Planning.Index.Suggestions.load();
    NBlackout.MoneyManager.Configuration.Planning.Index.ForecastHits.load();
}