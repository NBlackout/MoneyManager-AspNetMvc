var NBlackout;
(function(NBlackout) {
    NBlackout.version = '1.0.0';
})(NBlackout || (NBlackout = {}));

/* Notifications */
$.extend($.noty.defaults, {
    killer: true,
    layout: 'topRight',
    timeout: 3000,
    type: 'success'
});