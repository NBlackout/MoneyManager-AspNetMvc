var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Organizations;
            (function(Organizations) {
                var List;
                (function(List) {
                    List.DataGrid = new NBlackout.UI.AjaxContainer();
                })(List = NBlackout.MoneyManager.Configuration.Organizations.List || (NBlackout.MoneyManager.Configuration.Organizations.List = {}));
            })(Organizations = NBlackout.MoneyManager.Configuration.Organizations || (NBlackout.MoneyManager.Configuration.Organizations = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleOrganizationsListEvents() {
    $(document).on('organizations-updated', function() {
        NBlackout.MoneyManager.Configuration.Organizations.List.DataGrid.refresh();
    });

    NBlackout.MoneyManager.Configuration.Organizations.List.DataGrid.load();
}