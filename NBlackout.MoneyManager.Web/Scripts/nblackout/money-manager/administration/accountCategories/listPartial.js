var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var AccountCategories;
            (function(AccountCategories) {
                var ListPartial;
                (function(ListPartial) {
                    ListPartial.Crud = new NBlackout.UI.DialogForm();
                })(Customers = NBlackout.MoneyManager.Administration.AccountCategories.ListPartial || (NBlackout.MoneyManager.Administration.AccountCategories.ListPartial = {}));
            })(AccountCategories = NBlackout.MoneyManager.Administration.AccountCategories || (NBlackout.MoneyManager.Administration.AccountCategories = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function showCreateOrEditAccountCategoryDialog(control) {
    var action = control.data('action');

    if (action === 'create') {
        NBlackout.MoneyManager.Administration.AccountCategories.ListPartial.Crud.create();
    } else {
        var id = control.data('id');
        var options = { data: { id: id } };

        if (action === 'edit') {
            NBlackout.MoneyManager.Administration.AccountCategories.ListPartial.Crud.edit(options);
        } else {
            NBlackout.MoneyManager.Administration.AccountCategories.ListPartial.Crud.delete(options);
        }
    }
}

function handleAccountCategoriesListPartialEvents() {
    $('#accountCategoriesListPartialContent .actions, #accountCategoriesListPartialContent .table').on('click', '[data-action]', function(e) {
        var control = $(this);

        showCreateOrEditAccountCategoryDialog(control);

        return false;
    });

    $('#accountCategoriesListPartialContent .table').on('dblclick', 'td', function() {
        var cell = $(this);
        if (!cell.is('first-child')) {
            var control = $(this).siblings().addBack().find('[data-action]');

            showCreateOrEditAccountCategoryDialog(control);
        }
    });
}