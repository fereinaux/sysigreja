function CarregarTabelaPresenca() {
    if ($("#presenca-eventoid").val() && $("#presenca-reuniaoid").val() && $("#presenca-equipeid").val()) {
        const tablePresencaConfig = {
            language: languageConfig,
            lengthMenu: [200,500,1000],
            colReorder: false,
            serverSide: false,
            deferloading: 0,
            orderCellsTop: true,
            fixedHeader: true,
            filter: true,
            orderMulti: false,
            responsive: true,stateSave: true,
            destroy: true,
            dom: domConfigNoButtons,
            columns: [
                { data: "Nome", name: "Nome", autoWidth: true },
                {
                    data: "Id", name: "Id", orderable: false, width: "15%",
                    "render": function (data, type, row) {
                        return `${GetCheckBox('TogglePresenca', data, data, row.Presenca)}`;
                    }
                }
            ],
            order: [
                [0, "asc"]
            ],
            initComplete: function () {
                $('.i-checks-green').iCheck({
                    checkboxClass: 'icheckbox_square-green',
                    radioClass: 'iradio_square-green'
                });
                $('.i-checks-green').on('ifClicked', function (event) {
                    TogglePresenca($(event.target).data("id"));
                });
            },
            ajax: {
                url: '/Equipe/GetPresenca',
                datatype: "json",
                data: { EventoId: $("#presenca-eventoid").val(), EquipeId: $("#presenca-equipeid").val(), ReuniaoId: $("#presenca-reuniaoid").val() },
                type: "POST"
            }
        };
        $("#table-presenca").DataTable(tablePresencaConfig);
    }
}


$(document).ready(function () {
    getReunioes();
    getPresencas();
});

function TogglePresenca(id) {
    $.ajax({
        url: "/Equipe/TogglePresenca/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                EquipanteEventoId: id,
                ReuniaoId: $("#presenca-reuniaoid").val()
            })
    });
}

function getReunioes() {
    $("#presenca-reuniaoid").empty();

    $.ajax({
        url: "/Equipe/GetReunioes/",
        data: { EventoId: $("#presenca-eventoid").val() },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            data.Reunioes.forEach(function (reuniao, index, array) {
                $('#presenca-reuniaoid').append($(`<option value="${reuniao.Id}">${moment(reuniao.DataReuniao).format('DD/MM/YYYY')}</option>`));
            });
            $("#presenca-reuniaoid").val($("#presenca-reuniaoid option:first").val()).trigger("chosen:updated");
            CarregarTabelaPresenca();
        }
    });
}

function getPresencas() {
    $("#presenca-equipeid").empty();

    $.ajax({
        url: "/Equipe/GetEquipesByUser/",
        data: { EventoId: $("#presenca-eventoid").val() },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            data.Equipes.forEach(function (equipe, index, array) {
                $('#presenca-equipeid').append($(`<option value="${equipe.Id}">${equipe.Description}</option>`));
            });
            $("#presenca-equipeid").val($("#presenca-equipeid option:first").val()).trigger("chosen:updated");
            CarregarTabelaPresenca();
        }
    });
}
