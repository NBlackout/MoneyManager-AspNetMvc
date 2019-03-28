var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Administration;
        (function(Administration) {
            var OrganizationCategories;
            (function(OrganizationCategories) {
                var ListPartial;
                (function(ListPartial) {
                    ListPartial.Crud = new NBlackout.UI.DialogForm();
                })(ListPartial = NBlackout.MoneyManager.Administration.OrganizationCategories.ListPartial || (NBlackout.MoneyManager.Administration.OrganizationCategories.ListPartial = {}));
            })(OrganizationCategories = NBlackout.MoneyManager.Administration.OrganizationCategories || (NBlackout.MoneyManager.Administration.OrganizationCategories = {}));
        })(Administration = NBlackout.MoneyManager.Administration || (NBlackout.MoneyManager.Administration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function showCreateOrEditOrganizationCategoryDialog(control) {
    var action = control.data('action');

    if (action === 'create') {
        var data = {};

        if (control.is('[data-category-id]')) {
            data.categoryId = control.attr('data-category-id');
        }

        NBlackout.MoneyManager.Administration.OrganizationCategories.ListPartial.Crud.create({ data: data });
    } else {
        var id = control.data('id');
        var options = { data: { id: id } };

        if (action === 'edit') {
            NBlackout.MoneyManager.Administration.OrganizationCategories.ListPartial.Crud.edit(options);
        } else {
            NBlackout.MoneyManager.Administration.OrganizationCategories.ListPartial.Crud.delete(options);
        }
    }
}

function handleOrganizationCategoriesListPartialEvents() {
    $('#organizationCategoriesListPartialContent .actions, #organizationCategoriesListPartialContent .table').on('click', '[data-action]', function(e) {
        var control = $(this);

        showCreateOrEditOrganizationCategoryDialog(control);

        return false;
    });

    $('#organizationCategoriesListPartialContent .table').on('dblclick', 'td', function() {
        var cell = $(this);
        if (!cell.is('first-child')) {
            var control = $(this).siblings().addBack().find('[data-action]');

            showCreateOrEditOrganizationCategoryDialog(control);
        }
    });
}