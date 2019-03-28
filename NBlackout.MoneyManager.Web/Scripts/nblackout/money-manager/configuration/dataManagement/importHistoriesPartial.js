var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var DataManagement;
            (function(DataManagement) {
                var ImportHistoriesPartial;
                (function(ImportHistoriesPartial) {
                    ImportHistoriesPartial.Transactions = new NBlackout.UI.Dialog();
                })(DataManagement = NBlackout.MoneyManager.Configuration.DataManagement.ImportHistoriesPartial || (NBlackout.MoneyManager.Configuration.DataManagement.ImportHistoriesPartial = {}));
            })(DataManagement = NBlackout.MoneyManager.Configuration.DataManagement || (NBlackout.MoneyManager.Configuration.DataManagement = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleImportHistoriesPartialEvents() {
    $('[data-action="transactions"]').click(function(e) {
        e.preventDefault();

        var importHistoryId = $(this).data('id');

        NBlackout.MoneyManager.Configuration.DataManagement.ImportHistoriesPartial.Transactions.load({
            importHistoryId: importHistoryId
        });
    });
}