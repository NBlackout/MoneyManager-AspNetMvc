var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Classification;
            (function(Classification) {
                var SuggestionsPartial;
                (function(SuggestionsPartial) {
                    var Crud = (function() {
                        var updateOrganizationUrl = null;
                        var updateOrganizationsUrl = null;

                        var init = function(options) {
                            updateOrganizationUrl = options.updateOrganizationUrl;
                            updateOrganizationsUrl = options.updateOrganizationsUrl;
                        };

                        var updateOrganization = function(options) {
                            NBlackout.Core.ajax({
                                url: updateOrganizationUrl,
                                data: options.data,
                                method: 'POST',
                                success: function() {
                                    if (!NBlackout.Core.isNull(options.success)) {
                                        options.success();
                                    }
                                }
                            });
                        };

                        var updateOrganizations = function(options) {
                            NBlackout.Core.ajax({
                                url: updateOrganizationsUrl,
                                data: JSON.stringify(options.data),
                                method: 'POST',
                                contentCategory: 'application/json; charset=utf-8',
                                success: function() {
                                    if (!NBlackout.Core.isNull(options.success)) {
                                        options.success();
                                    }
                                }
                            });
                        };

                        return {
                            init: init,
                            updateOrganization: updateOrganization,
                            updateOrganizations: updateOrganizations
                        };
                    })();
                    SuggestionsPartial.Crud = Crud;
                })(SuggestionsPartial = NBlackout.MoneyManager.Administration.Classification.SuggestionsPartial || (NBlackout.MoneyManager.Administration.Classification.SuggestionsPartial = {}));
            })(Classification = NBlackout.MoneyManager.Administration.Classification || (NBlackout.MoneyManager.Administration.Classification = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleClassificationSuggestionsPartialEvents() {
    $('[data-action="update-all"]').click(function(e) {
        e.preventDefault();

        var data = [];

        $('#classificationSuggestionsPartialContent .table [data-action="update"]').each(function() {
            var link = $(this);
            var transactionId = link.data('transaction-id');
            var organizationId = link.data('organization-id');

            data.push({
                transactionId: transactionId,
                organizationId: organizationId
            });
        });

        NBlackout.MoneyManager.Administration.Classification.SuggestionsPartial.Crud.updateOrganizations({
            data: data,
            success: function() {
                noty({
                    text: 'Modifications effectuées'
                });

                $(document).trigger('suggestions-updated');
            }
        });
    });

    $('#classificationSuggestionsPartialContent .table').on('click', '[data-action="update"]', function(e) {
        var link = $(this);
        var transactionId = link.data('transaction-id');
        var organizationId = link.data('organization-id');

        NBlackout.MoneyManager.Administration.Classification.SuggestionsPartial.Crud.updateOrganization({
            data: {
                transactionId: transactionId,
                organizationId: organizationId
            },
            success: function() {
                noty({
                    text: 'Modification effectuée'
                });

                $(document).trigger('suggestions-updated');
            }
        });

        e.preventDefault();
    });
}