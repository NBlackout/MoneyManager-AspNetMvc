var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Consultation;
        (function(Consultation) {
            var Accounts;
            (function(Accounts) {
                var ListPartial;
                (function(ListPartial) {
                    ListPartial.Activity = new NBlackout.UI.AjaxContainer();
                })(ListPartial = NBlackout.MoneyManager.Consultation.Accounts.ListPartial || (NBlackout.MoneyManager.Consultation.Accounts.ListPartial = {}));
            })(Accounts = NBlackout.MoneyManager.Consultation.Accounts || (NBlackout.MoneyManager.Consultation.Accounts = {}));
        })(Consultation = NBlackout.MoneyManager.Consultation || (NBlackout.MoneyManager.Consultation = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleAccountsListPartialEvents() {
    $('#accountPills > li').on('click', '[data-action="activity"]', function(e) {
        var control = $(this);

        var pill = control.closest('li');
        if (pill.is('.active') === false) {
            var url = control.attr('href');
            var id = control.data('id');

            window.history.pushState({
                path: url
            }, url, url);

            NBlackout.MoneyManager.Consultation.Accounts.ListPartial.Activity.load({
                url: url,
                data: {
                    id: id
                }, success: function() {
                    $('#accountPills > li').removeClass('active');
                    pill.toggleClass('active');
                }
            });
        }

        e.preventDefault();
    });

    var currentAccountId = NBlackout.MoneyManager.Consultation.Accounts.CurrentAccountId;
    if (currentAccountId !== 0)
        $('[data-action="activity"][data-id="' + currentAccountId + '"]').get(0).click();;
}