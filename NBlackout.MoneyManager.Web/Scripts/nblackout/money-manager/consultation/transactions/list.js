var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Consultation;
        (function(Consultation) {
            var Transactions;
            (function(Transactions) {
                Transactions.List = new NBlackout.UI.DialogForm();
            })(Transactions = NBlackout.MoneyManager.Consultation.Transactions || (NBlackout.MoneyManager.Consultation.Transactions = {}));
        })(Consultation = NBlackout.MoneyManager.Consultation || (NBlackout.MoneyManager.Consultation = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleTransactionsListEvents() {
    $('#transactionsListPartialContent .table').on('click', '[data-action="edit"]', function(e) {
        var link = $(this);
        var id = link.data('transaction-id');

        NBlackout.MoneyManager.Consultation.Transactions.List.edit({
            data: {
                id: id
            }
        });

        e.preventDefault();
    });

    $('#transactionsListPartialContent .table').on('click', '[data-action="schedule"]', function(e) {
        var link = $(this);
        var id = link.data('transaction-id');

        NBlackout.MoneyManager.Consultation.Accounts.TransactionForecastHitsOfMonthPartial.Crud.create({
            data: {
                transactionId: id
            }
        });

        e.preventDefault();
    });
}