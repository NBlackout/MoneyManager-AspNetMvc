var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var OrganizationCategories;
            (function(OrganizationCategories) {
                var List;
                (function(List) {
                    List.DataGrid = new NBlackout.UI.AjaxContainer();
                })(List = NBlackout.MoneyManager.Administration.OrganizationCategories.List || (NBlackout.MoneyManager.Administration.OrganizationCategories.List = {}));
            })(OrganizationCategories = NBlackout.MoneyManager.Administration.OrganizationCategories || (NBlackout.MoneyManager.Administration.OrganizationCategories = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleOrganizationCategoriesListEvents() {
    $(document).on('organization-categories-updated', function() {
        NBlackout.MoneyManager.Administration.OrganizationCategories.List.DataGrid.refresh();
    });

    NBlackout.MoneyManager.Administration.OrganizationCategories.List.DataGrid.load();
}