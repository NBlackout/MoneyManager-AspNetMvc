var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Classification;
            (function(Classification) {
                var Suggestions;
                (function(Suggestions) {
                    Suggestions.DataGrid = new NBlackout.UI.AjaxContainer();
                })(Suggestions = NBlackout.MoneyManager.Administration.Classification.Suggestions || (NBlackout.MoneyManager.Administration.Classification.Suggestions = {}));
            })(Classification = NBlackout.MoneyManager.Administration.Classification || (NBlackout.MoneyManager.Administration.Classification = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleClassificationSuggestionsEvents() {
    $(document).on('organizations-updated suggestions-updated', function() {
        NBlackout.MoneyManager.Administration.Classification.Suggestions.DataGrid.refresh();
    });

    NBlackout.MoneyManager.Administration.Classification.Suggestions.DataGrid.load();
}