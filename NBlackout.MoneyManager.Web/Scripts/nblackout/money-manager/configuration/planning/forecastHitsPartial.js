var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var Planning;
            (function(Planning) {
                var ForecastHitsPartial;
                (function(ForecastHitsPartial) {
                    function ForecastHits() {
                        NBlackout.UI.DialogForm.call(this);

                        this.toggleForecastAutoRescheduleUrl = null;
                    }
                    ForecastHits.prototype = Object.create(NBlackout.UI.DialogForm.prototype, {
                        init: {
                            value: function(options) {
                                this.toggleForecastAutoRescheduleUrl = options.toggleForecastAutoRescheduleUrl;

                                NBlackout.UI.DialogForm.prototype.init.call(this, options);
                            }
                        },
                        toggleForecastAutoReschedule: {
                            value: function(options) {
                                NBlackout.Core.ajax({
                                    url: this.toggleForecastAutoRescheduleUrl,
                                    data: options.data,
                                    method: 'POST',
                                    success: function() {
                                        if (!NBlackout.Core.isNull(options.success)) {
                                            options.success();
                                        }
                                    }
                                });
                            }
                        }
                    });
                    ForecastHits.prototype.constructor = ForecastHits;

                    ForecastHitsPartial.Forecasts = new NBlackout.UI.DialogForm();
                    ForecastHitsPartial.ForecastHits = new ForecastHits();
                })(ForecastHitsPartial = NBlackout.MoneyManager.Configuration.Planning.ForecastHitsPartial || (NBlackout.MoneyManager.Configuration.Planning.ForecastHitsPartial = {}));
            })(Planning = NBlackout.MoneyManager.Configuration.Planning || (NBlackout.MoneyManager.Configuration.Planning = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function showCreateOrEditForecastHitDialog(control) {
    var action = control.data('action');
    var target = control.data('target');

    if (action === 'create') {
        if (target === 'forecast') {
            NBlackout.MoneyManager.Configuration.Planning.ForecastHitsPartial.Forecasts.create();
        } else {
            var transactionForecastId = control.attr('data-forecast-id');

            NBlackout.MoneyManager.Configuration.Planning.ForecastHitsPartial.ForecastHits.create({
                data: {
                    transactionForecastId: transactionForecastId
                }
            });
        }
    } else {
        var id = control.data('id');
        var options = { data: { id: id } };

        if (action === 'edit') {
            if (target === 'forecast') {
                NBlackout.MoneyManager.Configuration.Planning.ForecastHitsPartial.Forecasts.edit(options);
            } else {
                NBlackout.MoneyManager.Configuration.Planning.ForecastHitsPartial.ForecastHits.edit(options);
            }
        } else {
            if (target === 'forecast') {
                NBlackout.MoneyManager.Configuration.Planning.ForecastHitsPartial.Forecasts.delete(options);
            } else {
                NBlackout.MoneyManager.Configuration.Planning.ForecastHitsPartial.ForecastHits.delete(options);
            }
        }
    }
}

function handlePlanningForecastHitsPartialEvents() {
    $('#forecastHitsContent .actions, #forecastHitsContent .table').on('click', '[data-action]', function(e) {
        var control = $(this);

        showCreateOrEditForecastHitDialog(control);
        return false;
    });

    $('#forecastHitsContent .table').on('activated', 'tr', function() {
        var control = $(this).find('[data-id]');
        var isForecast = control.is('[data-target="forecast"]');
        var createHitButton = $('#forecastHitsContent .actions [data-target="hit"]');

        createHitButton.prop('disabled', !isForecast);

        if (isForecast) {
            var transactionForecastId = control.data('id');
            createHitButton.attr('data-forecast-id', transactionForecastId);
        }
    });

    $('#forecastHitsContent .table').on('dblclick', 'td', function() {
        var cell = $(this);
        if (!cell.is('first-child')) {
            var control = $(this).siblings().addBack().find('[data-action]');

            showCreateOrEditForecastHitDialog(control);
        }
    });
}