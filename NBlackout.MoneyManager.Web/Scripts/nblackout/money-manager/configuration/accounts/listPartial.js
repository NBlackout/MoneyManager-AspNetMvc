var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Accounts;
            (function(Accounts) {
                var ListPartial;
                (function(ListPartial) {
                    ListPartial.Crud = new NBlackout.UI.DialogForm();
                })(ListPartial = NBlackout.MoneyManager.Configuration.Accounts.ListPartial || (NBlackout.MoneyManager.Configuration.Accounts.ListPartial = {}));
            })(Accounts = NBlackout.MoneyManager.Configuration.Accounts || (NBlackout.MoneyManager.Configuration.Accounts = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function showEditAccountCategoryDialog(control) {
    var id = control.data('id');

    NBlackout.MoneyManager.Configuration.Accounts.ListPartial.Crud.edit({
        data: { id: id }
    });
}

function handleAccountsListPartialEvents() {
    $('#accountsListPartialContent .table').on('click', '[data-action]', function(e) {
        var control = $(this);

        showEditAccountCategoryDialog(control);

        return false;
    });

    $('#accountsListPartialContent .table').on('dblclick', 'td', function() {
        var cell = $(this);
        if (!cell.is('first-child')) {
            var control = $(this).siblings().addBack().find('[data-action]');

            showEditAccountCategoryDialog(control);
        }
    });
}