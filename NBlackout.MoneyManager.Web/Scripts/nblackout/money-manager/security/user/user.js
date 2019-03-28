var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Security;
        (function(Security) {
            var User;
            (function(User) {
                User.Password = new NBlackout.UI.DialogForm();
                User.Email = new NBlackout.UI.DialogForm();
                User.PhoneNumber = new NBlackout.UI.DialogForm();
                User.VerifyPhoneNumber = new NBlackout.UI.Dialog();
            })(User = NBlackout.MoneyManager.Security.User || (NBlackout.MoneyManager.Security.User = {}));
        })(Security = NBlackout.MoneyManager.Security || (NBlackout.MoneyManager.Security = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));