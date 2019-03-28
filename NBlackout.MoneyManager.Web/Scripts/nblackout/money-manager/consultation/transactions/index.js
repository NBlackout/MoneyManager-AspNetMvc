var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Consultation;
        (function(Consultation) {
            var Transactions;
            (function(Transactions) {
                Transactions.Index = new NBlackout.UI.AjaxContainer();
            })(Transactions = NBlackout.MoneyManager.Consultation.Transactions || (NBlackout.MoneyManager.Consultation.Transactions = {}));
        })(Consultation = NBlackout.MoneyManager.Consultation || (NBlackout.MoneyManager.Consultation = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleTransactionsIndexEvents() {
    $(document).on('transactions-updated', function() {
        NBlackout.MoneyManager.Consultation.Transactions.Index.refresh();
    });

    $('#searchTransactionsForm').submit(function() {
        var form = $(this);
        var url = form.attr('action');
        var data = form.serialize();

        NBlackout.MoneyManager.Consultation.Transactions.Index.load({
            url: url,
            data: data,
            badRequest: function(response) {
                var id = form.attr('id');
                var responseForm = $(response).find('#' + id);

                form.html(responseForm.html());
            }
        });

        return false;
    });
}