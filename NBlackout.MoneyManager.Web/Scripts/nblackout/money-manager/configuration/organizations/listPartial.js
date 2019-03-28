var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Organizations;
            (function(Organizations) {
                var ListPartial;
                (function(ListPartial) {
                    ListPartial.Crud = new NBlackout.UI.DialogForm();
                    ListPartial.AutomationElements = new NBlackout.UI.DialogForm();
                })(ListPartial = NBlackout.MoneyManager.Configuration.Organizations.ListPartial || (NBlackout.MoneyManager.Configuration.Organizations.ListPartial = {}));
            })(Organizations = NBlackout.MoneyManager.Configuration.Organizations || (NBlackout.MoneyManager.Configuration.Organizations = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function showCreateOrEditOrganizationDialog(control) {
    var action = control.data('action');
    var target = control.data('target');

    if (action === 'create') {
        if (target === 'organization') {
            NBlackout.MoneyManager.Configuration.Organizations.ListPartial.Crud.create();
        } else {
            var organizationId = control.attr('data-organization-id');

            NBlackout.MoneyManager.Configuration.Organizations.ListPartial.AutomationElements.create({
                data: {
                    organizationId: organizationId
                }
            });
        }
    } else {
        var id = control.data('id');
        var options = { data: { id: id } };

        if (action === 'edit') {
            if (target === 'organization') {
                NBlackout.MoneyManager.Configuration.Organizations.ListPartial.Crud.edit(options);
            } else {
                NBlackout.MoneyManager.Configuration.Organizations.ListPartial.AutomationElements.edit(options);
            }
        } else {
            if (target === 'organization') {
                NBlackout.MoneyManager.Configuration.Organizations.ListPartial.Crud.delete(options);
            } else {
                NBlackout.MoneyManager.Configuration.Organizations.ListPartial.AutomationElements.delete(options);
            }
        }
    }
}

function handleOrganizationsListPartialEvents() {
    $('#organizationsListPartialContent .actions, #organizationsListPartialContent .table').on('click', '[data-action]', function(e) {
        var control = $(this);

        showCreateOrEditOrganizationDialog(control);

        return false;
    });

    $('#organizationsListPartialContent .table').on('dblclick', 'td', function() {
        var cell = $(this);
        if (!cell.is('first-child')) {
            var control = $(this).siblings().addBack().find('[data-action]');

            showCreateOrEditOrganizationDialog(control);
        }
    });
}