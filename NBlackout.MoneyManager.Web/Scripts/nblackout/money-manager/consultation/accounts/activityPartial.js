var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Consultation;
        (function(Consultation) {
            var Accounts;
            (function(Accounts) {
                var ActivityPartial;
                (function(ActivityPartial) {
                    ActivityPartial.Details = new NBlackout.UI.Dialog();
                    ActivityPartial.TransactionForecastHitsOfMonth = new NBlackout.UI.AjaxContainer();
                    ActivityPartial.TransactionsOfMonth = new NBlackout.UI.AjaxContainer();
                    ActivityPartial.AllTransactionForecastHitsOfMonth = new NBlackout.UI.Dialog();
                })(ActivityPartial = NBlackout.MoneyManager.Consultation.Accounts.ActivityPartial || (NBlackout.MoneyManager.Consultation.Accounts.ActivityPartial = {}));
            })(Accounts = NBlackout.MoneyManager.Consultation.Accounts || (NBlackout.MoneyManager.Consultation.Accounts = {}));
        })(Consultation = NBlackout.MoneyManager.Consultation || (NBlackout.MoneyManager.Consultation = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function getMonthParameters() {
    var dates = $('#dates');
    var date = dates.find('option:selected');

    return {
        accountId: dates.data('id'),
        year: date.data('year'),
        month: date.data('month')
    };
}

function updateTransactions() {
    var data = getMonthParameters();

    NBlackout.MoneyManager.Consultation.Accounts.ActivityPartial.TransactionForecastHitsOfMonth.load({
        data: data
    });

    NBlackout.MoneyManager.Consultation.Accounts.ActivityPartial.TransactionsOfMonth.load({
        data: data
    });
}

function handleAccountsActivityPartialEvents() {
    $(document).on('forecast-hits-updated', function() {
        NBlackout.MoneyManager.Consultation.Accounts.ActivityPartial.TransactionForecastHitsOfMonth.refresh();
    });

    $(document).on('transactions-updated', function() {
        NBlackout.MoneyManager.Consultation.Accounts.ActivityPartial.TransactionsOfMonth.refresh();
    });

    $('#accountsActivityPartialContent .account-header').on('click', '[data-action="details"]', function(e) {
        var id = $(this).data('id');

        NBlackout.MoneyManager.Consultation.Accounts.ActivityPartial.Details.load({
            id: id
        });

        e.preventDefault();
    });

    $('[data-action="date"]').click(function(e) {
        e.preventDefault();

        var value = $('#dates').val();

        var target = $(this).data('target');
        switch (target) {
            case 'fast-backward': value = $('#dates option:first').val(); break;
            case 'step-backward': value = $('#dates option:selected').prev().val(); break;
            case 'step-forward': value = $('#dates option:selected').next().val(); break;
            case 'fast-forward': value = $('#dates option:last').val(); break;
            default: break;
        }

        $('#dates').val(value);
        $('#dates').change();
    });

    $('#dates').change(function() {
        var selected = $(this).val();
        var first = $('#dates option:first').val();
        var last = $('#dates option:last').val();

        $('[data-action="date"][data-target="fast-backward"]').prop('disabled', (selected === first));
        $('[data-action="date"][data-target="step-backward"]').prop('disabled', (selected === first));
        $('[data-action="date"][data-target="step-forward"]').prop('disabled', (selected === last));
        $('[data-action="date"][data-target="fast-forward"]').prop('disabled', (selected === last));

        updateTransactions();
    });

    $('[data-action="show-all"]').click(function(e) {
        var params = getMonthParameters();

        NBlackout.MoneyManager.Consultation.Accounts.ActivityPartial.AllTransactionForecastHitsOfMonth.load(params);

        e.preventDefault();
    });

    updateTransactions();
}