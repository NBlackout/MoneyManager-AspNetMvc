var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Planning;
            (function(Planning) {
                var ForecastHits;
                (function(ForecastHits) {
                    ForecastHits.DataGrid = new NBlackout.UI.AjaxContainer();
                })(ForecastHits = NBlackout.MoneyManager.Configuration.Planning.ForecastHits || (NBlackout.MoneyManager.Configuration.Planning.ForecastHits = {}));
            })(Planning = NBlackout.MoneyManager.Configuration.Planning || (NBlackout.MoneyManager.Configuration.Planning = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handlePlanningForecastHitsEvents() {
    $(document).on('forecasts-updated forecast-hits-updated', function() {
        NBlackout.MoneyManager.Configuration.Planning.ForecastHits.DataGrid.refresh();
    });

    NBlackout.MoneyManager.Configuration.Planning.ForecastHits.DataGrid.load();
}