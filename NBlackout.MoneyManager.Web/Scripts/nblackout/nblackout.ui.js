var NBlackout;
(function(NBlackout) {
    var UI;
    (function(UI) {
        function AjaxContainer() {
            this.container = null;
            this.settings = null;
        }
        AjaxContainer.prototype = {
            init: function(options) {
                options = $.extend({}, options);

                this.container = $(options.container);
                this.settings = { url: options.url };
            },
            load: function(options) {
                options = $.extend({}, options);

                this.settings.success = null;
                $.extend(this.settings, options);

                var success = this.settings.success;
                this.settings.success = function(response) {
                    this.container.html(response);

                    if (!NBlackout.Core.isNull(success)) {
                        success(response);
                    }
                }.bind(this);

                NBlackout.Core.ajax(this.settings);
            },
            refresh: function() {
                NBlackout.Core.ajax(this.settings);
            }
        };

        function Dialog() {
            this.url = null;
        }
        Dialog.prototype = {
            init: function(options) {
                options = $.extend({}, $.ui.dialog.prototype.options, {
                    autoOpen: false,
                    close: function() {
                        $(this).empty();
                    },
                    draggable: false,
                    modal: true,
                    open: function(event, ui) {
                        $(this).addClass('container-fluid');
                    },
                    resizable: false,
                    width: 600
                }, options);

                this.container = $(options.container);
                this.url = options.url;

                this.container.dialog(options);
            },
            load: function(params) {
                if (!NBlackout.Core.isNull(this.url)) {
                    NBlackout.Core.ajax({
                        url: this.url,
                        data: params,
                        success: function(data) {
                            this.open(data);
                        }.bind(this)
                    });
                } else {
                    this.open();
                }
            },
            open: function(data) {
                if (!NBlackout.Core.isNull(data)) {
                    this.container.html(data);
                }

                this.container.dialog('open');
            },
            close: function() {
                this.container.dialog('close');
            }
        };

        function DialogForm() {
            this.dialog = null;
            this.deleteDialog = null;

            this.createUrl = null;
            this.editUrl = null;
            this.deleteUrl = null;
        }
        DialogForm.prototype = {
            init: function(options) {
                options = $.extend({}, options);

                this.dialog = new Dialog();
                this.dialog.init({
                    container: options.container
                });

                this.deleteDialog = new Dialog();
                this.deleteDialog.init({
                    container: options.deleteContainer
                });

                this.createUrl = options.createUrl;
                this.editUrl = options.editUrl;
                this.deleteUrl = options.deleteUrl;
            },
            create: function(options) {
                options = $.extend({}, options);

                NBlackout.Core.ajax({
                    url: this.createUrl,
                    data: options.data,
                    success: function(response) {
                        this.dialog.open(response);
                    }.bind(this)
                });
            },
            edit: function(options) {
                options = $.extend({}, options);

                NBlackout.Core.ajax({
                    url: this.editUrl,
                    data: options.data,
                    success: function(response) {
                        this.dialog.open(response);
                    }.bind(this)
                });
            },
            delete: function(options) {
                options = $.extend({}, options);

                NBlackout.Core.ajax({
                    url: this.deleteUrl,
                    data: options.data,
                    success: function(response) {
                        this.deleteDialog.open(response);
                    }.bind(this)
                });
            },
            confirmDeletion: function(options) {
                options = $.extend({}, options);

                var success = options.success;
                options.success = function() {
                    this.deleteDialog.close();

                    if (!NBlackout.Core.isNull(success)) {
                        success();
                    }
                }.bind(this);

                this.save(options);
            },
            save: function(options) {
                options = $.extend({}, options);

                var form = options.form;
                var url = form.attr('action');
                var method = form.attr('method');
                var data = form.serialize();

                NBlackout.Core.ajax({
                    url: url,
                    method: method,
                    data: data,
                    success: function(response) {
                        this.dialog.close();

                        if (!NBlackout.Core.isNull(options.success)) {
                            options.success(response);
                        }
                    }.bind(this),
                    badRequest: function(response) {
                        var errors = $(response).find('.input-validation-error[category!="hidden"]');
                        $.each(errors, function() {
                            var name = $(this).attr('name');

                            var messageSelector = '[data-valmsg-for="' + name + '"]';
                            var existingMessage = form.find(messageSelector);
                            var newMessage = $(response).find(messageSelector);
                            if (existingMessage.length) {
                                existingMessage.closest('.form-group').addClass('has-error');
                                existingMessage.removeClass('field-validation-valid').addClass('field-validation-error');
                                existingMessage.text(newMessage.text()).show();
                            } else {
                                form.find('[name="' + name + '"]').after(newMessage.text());
                            }
                        });

                        if (!NBlackout.Core.isNull(options.badRequest)) {
                            options.badRequest();
                        }
                    },
                    error: options.error
                });
            }
        };

        UI.AjaxContainer = AjaxContainer;
        UI.Dialog = Dialog;
        UI.DialogForm = DialogForm;
    })(UI = NBlackout.UI || (NBlackout.UI = {}));
})(NBlackout || (NBlackout = {}));