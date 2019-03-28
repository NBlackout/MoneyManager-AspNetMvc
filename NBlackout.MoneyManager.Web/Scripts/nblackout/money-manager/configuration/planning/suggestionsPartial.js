var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Planning;
            (function(Planning) {
                var SuggestionsPartial;
                (function(SuggestionsPartial) {
                    var Crud = (function() {
                        var updateTransactionForecastHitUrl = null;
                        var updateTransactionForecastHitsUrl = null;

                        var init = function(options) {
                            updateTransactionForecastHitUrl = options.updateTransactionForecastHitUrl;
                            updateTransactionForecastHitsUrl = options.updateTransactionForecastHitsUrl;
                        };

                        var updateTransactionForecastHit = function(options) {
                            NBlackout.Core.ajax({
                                url: updateTransactionForecastHitUrl,
                                data: options.data,
                                method: 'POST',
                                success: function() {
                                    if (!NBlackout.Core.isNull(options.success)) {
                                        options.success();
                                    }
                                }
                            });
                        };

                        var updateTransactionForecastHits = function(options) {
                            NBlackout.Core.ajax({
                                url: updateTransactionForecastHitsUrl,
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
                            updateTransactionForecastHit: updateTransactionForecastHit,
                            updateTransactionForecastHits: updateTransactionForecastHits
                        };
                    })();
                    SuggestionsPartial.Crud = Crud;
                })(SuggestionsPartial = NBlackout.MoneyManager.Configuration.Planning.SuggestionsPartial || (NBlackout.MoneyManager.Configuration.Planning.SuggestionsPartial = {}));
            })(Planning = NBlackout.MoneyManager.Configuration.Planning || (NBlackout.MoneyManager.Configuration.Planning = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handlePlanningSuggestionsPartialEvents() {
    $('[data-action="update-all"]').click(function(e) {
        e.preventDefault();

        var data = [];

        $('#planningSuggestionsPartialContent .table [data-action="update"]').each(function() {
            var link = $(this);
            var transactionForecastHitId = link.data('transaction-forecast-hit-id');
            var transactionId = link.data('transaction-id');

            data.push({
                transactionForecastHitId: transactionForecastHitId,
                transactionId: transactionId
            });
        });

        NBlackout.MoneyManager.Configuration.Planning.SuggestionsPartial.Crud.updateTransactionForecastHits({
            data: data,
            success: function() {
                noty({
                    text: 'Modifications effectuées'
                });

                $(document).trigger('suggestions-updated');
            }
        });
    });

    $('#planningSuggestionsPartialContent .table').on('click', '[data-action="update"]', function(e) {
        var link = $(this);
        var transactionForecastHitId = link.data('transaction-forecast-hit-id');
        var transactionId = link.data('transaction-id');

        NBlackout.MoneyManager.Configuration.Planning.SuggestionsPartial.Crud.updateTransactionForecastHit({
            data: {
                transactionForecastHitId: transactionForecastHitId,
                transactionId: transactionId
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