var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var DataManagement;
            (function(DataManagement) {
                var ImportHistories;
                (function(ImportHistories) {
                    ImportHistories.DataGrid = new NBlackout.UI.AjaxContainer();
                })(ImportHistories = NBlackout.MoneyManager.Configuration.DataManagement.ImportHistories || (NBlackout.MoneyManager.Configuration.DataManagement.ImportHistories = {}));
            })(DataManagement = NBlackout.MoneyManager.Configuration.DataManagement || (NBlackout.MoneyManager.Configuration.DataManagement = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleImportHistoriesEvents() {
    NBlackout.MoneyManager.Configuration.DataManagement.ImportHistories.DataGrid.load();
}