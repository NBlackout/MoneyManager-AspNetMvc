var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Consultation;
        (function(Consultation) {
            var Accounts;
            (function(Accounts) {
                var TransactionForecastHitsOfMonthPartial;
                (function(TransactionForecastHitsOfMonthPartial) {
                    function Crud() {
                        NBlackout.UI.DialogForm.call(this);

                        this.toggleForecastAutoRescheduleUrl = null;
                    }
                    Crud.prototype = Object.create(NBlackout.UI.DialogForm.prototype, {
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
                    Crud.prototype.constructor = Crud;

                    TransactionForecastHitsOfMonthPartial.Crud = new Crud();
                })(TransactionForecastHitsOfMonthPartial = NBlackout.MoneyManager.Consultation.Accounts.TransactionForecastHitsOfMonthPartial || (NBlackout.MoneyManager.Consultation.Accounts.TransactionForecastHitsOfMonthPartial = {}));
            })(Accounts = NBlackout.MoneyManager.Consultation.Accounts || (NBlackout.MoneyManager.Consultation.Accounts = {}));
        })(Consultation = NBlackout.MoneyManager.Consultation || (NBlackout.MoneyManager.Consultation = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));