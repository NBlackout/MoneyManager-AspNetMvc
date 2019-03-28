var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Planning;
            (function(Planning) {
                var Suggestions;
                (function(Suggestions) {
                    Suggestions.DataGrid = new NBlackout.UI.AjaxContainer();
                })(Suggestions = NBlackout.MoneyManager.Configuration.Planning.Suggestions || (NBlackout.MoneyManager.Configuration.Planning.Suggestions = {}));
            })(Planning = NBlackout.MoneyManager.Configuration.Planning || (NBlackout.MoneyManager.Configuration.Planning = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handlePlanningSuggestionsEvents() {
    $(document).on('forecast-hits-updated suggestions-updated', function() {
        NBlackout.MoneyManager.Configuration.Planning.Suggestions.DataGrid.refresh();
    });

    NBlackout.MoneyManager.Configuration.Planning.Suggestions.DataGrid.load();
}