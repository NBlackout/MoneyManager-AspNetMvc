var NBlackout;
(function(NBlackout) {
    var Core;
    (function(Core) {
        var isNull = function(value) {
            return (typeof value === 'undefined' || value === null);
        };
        var ajax = function(options) {
            options = $.extend({}, options);

            var success = options.success;
            var error = options.error;

            options.success = function(data, status, request) {
                var responseHeader = request.getResponseHeader('X-Responded-JSON');

                if (!isNull(responseHeader)) {
                    var respondedJson = JSON.parse(responseHeader);
                    var respondedJsonStatus = respondedJson.status;

                    if (respondedJsonStatus === 401) {
                        var location = respondedJson.headers['location'];

                        window.location = location;
                    }
                } else if (!isNull(success)) {
                    var viewContainer = $(data).find('#viewContainer');
                    if (viewContainer.length === 1) {
                        data = viewContainer.html();
                    }

                    success(data);
                }
            };

            options.error = function(request) {
                var response = request.responseText;

                if (request.status === 400) {
                    if (!isNull(options.badRequest)) {
                        options.badRequest(response);
                    } else {
                        noty({
                            text: 'Requête incorrecte',
                            type: 'error'
                        });
                    }
                } else if (!isNull(error)) {
                    error(response);
                } else {
                    noty({
                        text: 'Une erreur est survenue',
                        type: 'error'
                    });
                }
            };

            $.ajax(options);
        };
        var ajaxNavigate = function(options) {
            options = $.extend({}, options);

            var url = options.url;

            ajax({
                url: url,
                success: function(response) {
                    window.history.pushState({
                        path: url
                    }, url, url);

                    if (!isNull(options.success)) {
                        options.success(response);
                    }
                }
            });
        };

        Core.isNull = isNull;
        Core.ajax = ajax;
        Core.ajaxNavigate = ajaxNavigate;
    })(Core = NBlackout.Core || (NBlackout.Core = {}));
})(NBlackout || (NBlackout = {}));