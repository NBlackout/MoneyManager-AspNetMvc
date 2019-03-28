var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var currentArea = null;
        var currentController = null;
        var userInfoUrl = null;

        var ajaxNavigate = function(options) {
            var success = options.success;

            options.success = function(response) {
                var container = $('#viewContainer');

                container.hide();
                container.find('.ui-dialog-content').dialog('destroy');
                container.html(response);
                container.show();

                if (!NBlackout.Core.isNull(success)) {
                    success();
                }
            };

            NBlackout.Core.ajaxNavigate(options);
        };

        MoneyManager.currentArea = currentArea;
        MoneyManager.currentController = currentController;
        MoneyManager.userInfoUrl = userInfoUrl;
        MoneyManager.ajaxNavigate = ajaxNavigate;
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function initializeHighcharts() {
    Highcharts.setOptions({
        chart: {
            backgroundColor: null
        },
        colors: [
            '#058DC7', '#F45B5B', '#55BF3B', '#434348', '#F7A35C', '#2B908F', '#64E572', '#F15C80', '#24CBE5', '#E4D354', '#8d4654', '#91E8E1'
        ],
        credits: {
            enabled: false
        },
        legend: {
            borderWidth: 1,
            shadow: true
        },
        tooltip: {
            valueDecimals: 2,
            valueSuffix: '€'
        }
    });
}

function handleWindowEvents() {
    // History back button
    $(window).bind('popstate', function() {
        var url = location.pathname + location.search;

        NBlackout.Core.ajax({
            url: url,
            success: function(response) {
                var currentArea = /nblackout\.moneyManager\.currentArea = '(.*?)';/g.exec(response);
                var currentController = /nblackout\.moneyManager\.currentController = '(.*?)';/g.exec(response);
                if (!NBlackout.Core.isNull(currentArea) && !NBlackout.Core.isNull(currentController)) {
                    NBlackout.MoneyManager.currentArea = currentArea[1];
                    NBlackout.MoneyManager.currentController = currentController[1];

                    $('#navbarContainer').trigger('navigate');
                }

                var container = $('#viewContainer');
                var content = $(response).find('#viewContainer').html();

                container.hide();
                container.find('.ui-dialog-content').dialog('destroy');
                container.html(content);
                container.show();
            }
        });
    });
}

function handleAjaxEvents() {
    var ajaxRequestCount = 0;
    var timeout = null;

    // Start ajax request
    $.ajaxSetup({
        beforeSend: function() {
            if (++ajaxRequestCount === 1) {
                timeout = setTimeout(function() {
                    $('#loader').toggleClass('hidden', false);
                }, 250);
            }
        },
        complete: function() {
            if (--ajaxRequestCount === 0) {
                $('#loader').toggleClass('hidden', true);
                clearTimeout(timeout);
            }
        }
    });
}

function handleMenuEvents() {
    var slideMenu = function(area) {
        $('.menu-item[data-area]').not('[data-area="' + area + '"]').find('.submenu').slideUp(300, function() {
            $('.menu-item[data-area]').not('[data-area="' + NBlackout.MoneyManager.currentArea + '"]').removeClass('active');
            $('.menu-item[data-area="' + area + '"]').addClass('active');

            $('.menu-item[data-area="' + area + '"] .submenu').slideDown(300);
        });
    };

    // Handle navigation
    $('#navbarContainer').on('navigate', function() {
        var area = NBlackout.MoneyManager.currentArea;
        var controller = NBlackout.MoneyManager.currentController;

        $('.menu-item').removeClass('active');
        $('.menu-item[data-area="' + area + '"] .menu-item[data-controller="' + controller + '"]').addClass('active');
        slideMenu(area);
    });

    // Area and controller menu selection
    $('.menu-item a').click(function(e) {
        if (e.which === 1) {
            var link = $(this);
            var menuItem = link.closest('.menu-item');

            var area = menuItem.data('area');
            if (!NBlackout.Core.isNull(area)) {
                slideMenu(area);
            } else {
                var url = link.attr('href');

                NBlackout.MoneyManager.ajaxNavigate({
                    url: url,
                    success: function() {
                        NBlackout.MoneyManager.currentArea = link.closest('[data-area]').data('area');
                        NBlackout.MoneyManager.currentController = link.closest('[data-controller]').data('controller');

                        $('#navbarContainer').trigger('navigate');
                    }
                });
            }
        }

        e.preventDefault();
    });

    $('#navbarContainer').trigger('navigate');
}

function handlerUserSettingsEvents() {
    $('#gravatar').on('load', function() {
        $('#userMenu').show();
    });

    setTimeout(function() {
        NBlackout.Core.ajax({
            url: NBlackout.MoneyManager.userInfoUrl,
            success: function(result) {
                $('#gravatar').attr('src', result.Gravatar);
            }
        });
    }, 500);
}

function handleTableEvents() {
    $(document).on('click', '.table-collapse tr[data-target]', function() {
        var row = $(this);
        var control = row.find('[data-icon="collapse"]');

        var shown = control.hasClass('glyphicon-chevron-down');
        var from = shown ? 'glyphicon-chevron-down' : 'glyphicon-chevron-right';
        var to = shown ? 'glyphicon-chevron-right' : 'glyphicon-chevron-down';

        control.switchClass(from, to);
    });

    $(document).on('click', '.table-hover tr', function() {
        var row = $(this);

        row.siblings().trigger('inactivated').trigger('mouseout');
        row.trigger('activated');
    });

    $(document).on('mouseover', '.table-hover tr', function() {
        $(this).find('[data-action]').toggleClass('invisible', false);
    });

    $(document).on('mouseout', '.table-hover tr', function() {
        var row = $(this);

        if (!row.is('.active')) {
            row.find('[data-action]').toggleClass('invisible', true);
        }
    });

    $(document).on('shown.bs.collapse hidden.bs.collapse', '.table-collapse tr', function(e) {
        if (e.category === 'hidden') {
            var row = $(this);
            var control = row.find('[data-icon="collapse"]');
            var selector = row.data('selector');
            var children = row.siblings('[data-parent="' + selector + '"]');

            control.switchClass('glyphicon-chevron-down', 'glyphicon-chevron-right');
            children.collapse('hide');
        }
    });

    $(document).on('activated', '.table-hover tr', function() {
        $(this).toggleClass('active', true);
    });

    $(document).on('inactivated', '.table-hover tr', function() {
        $(this).toggleClass('active', false);
    });
}

function handleDocumentEvents() {
    $('#manageAccount').click(function(e) {
        var url = $(this).attr('href');

        NBlackout.MoneyManager.ajaxNavigate({
            url: url
        });

        e.preventDefault();
    });
}

function initializeComponents() {
    initializeHighcharts();
}

function handleMoneyManagerEvents() {
    handleWindowEvents();
    handleAjaxEvents();
    handleMenuEvents();
    handlerUserSettingsEvents();
    handleTableEvents();
    handleDocumentEvents();
}