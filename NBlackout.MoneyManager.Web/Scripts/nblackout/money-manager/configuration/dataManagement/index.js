var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Configuration;
        (function(Configuration) {
            var DataManagement;
            (function(DataManagement) {
                var Index;
                (function(Index) {
                    Index.ImportHistories = new NBlackout.UI.AjaxContainer();
                })(Index = NBlackout.MoneyManager.Configuration.DataManagement.Index || (NBlackout.MoneyManager.Configuration.DataManagement.Index = {}));
            })(DataManagement = NBlackout.MoneyManager.Configuration.DataManagement || (NBlackout.MoneyManager.Configuration.DataManagement = {}));
        })(Configuration = NBlackout.MoneyManager.Configuration || (NBlackout.MoneyManager.Configuration = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function handleDataManagementEvents() {
    $('#files').change(function() {
        var file = $(this)[0].files[0];
        if (!NBlackout.Core.isNull(file)) {
            var fileName = file.name;

            $('#chosenFile').val(fileName);
            $('#importButton').prop('disabled', false);
        }
    });

    $('#chooseFile').click(function(e) {
        e.preventDefault();

        $('#chosenFile').val('');
        $('#importButton').prop('disabled', true);

        $('#files').trigger('click');
    });

    $('#uploadForm').submit(function() {
        var form = $(this);
        var url = form.attr('action');
        var method = form.attr('method');
        var data = new FormData(form.get(0));

        var container = $('#importSuccessContainer').clone();

        NBlackout.Core.ajax({
            url: url,
            method: method,
            data: data,
            processData: false,
            contentCategory: false,
            beforeSend: function() {
                $('#chosenFile').val('');
                $('#importButton').prop('disabled', true);

                noty({
                    text: 'Traitement du fichier',
                    type: 'information'
                });
            },
            success: function(totalTransactionsAdded) {
                if (totalTransactionsAdded !== 0) {
                    container.find('#importSuccessCount').text(totalTransactionsAdded);

                    noty({
                        text: container.html(),
                        timeout: 5000,
                        type: 'success'
                    });

                    NBlackout.MoneyManager.Configuration.DataManagement.Index.ImportHistories.refresh();
                } else {
                    noty({
                        text: 'Déjà à jour',
                        type: 'warning'
                    });
                }
            }
        });

        return false;
    });

    NBlackout.MoneyManager.Configuration.DataManagement.Index.ImportHistories.load();
}