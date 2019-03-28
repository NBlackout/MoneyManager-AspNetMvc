var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var Customers;
            (function(Customers) {
                var ListPartial;
                (function(ListPartial) {
                    ListPartial.Crud = new NBlackout.UI.DialogForm();
                })(Customers = NBlackout.MoneyManager.Administration.Customers.ListPartial || (NBlackout.MoneyManager.Administration.Customers.ListPartial = {}));
            })(Customers = NBlackout.MoneyManager.Administration.Customers || (NBlackout.MoneyManager.Administration.Customers = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function showCreateOrEditCustomerDialog(control) {
    var action = control.data('action');

    if (action === 'create') {
        NBlackout.MoneyManager.Administration.Customers.ListPartial.Crud.create();
    } else {
        var id = control.data('id');
        var options = { data: { id: id } };

        if (action === 'edit') {
            NBlackout.MoneyManager.Administration.Customers.ListPartial.Crud.edit(options);
        } else {
            NBlackout.MoneyManager.Administration.Customers.ListPartial.Crud.delete(options);
        }
    }
}

function handleCustomersListPartialEvents() {
    $('#customersListPartialContent .actions, #customersListPartialContent .table').on('click', '[data-action]', function(e) {
        var control = $(this);

        showCreateOrEditCustomerDialog(control);
        return false;
    });

    $('#customersListPartialContent .table').on('dblclick', 'td', function() {
        var cell = $(this);
        if (!cell.is('first-child')) {
            var control = $(this).siblings().addBack().find('[data-action]');

            showCreateOrEditCustomerDialog(control);
        }
    });
}