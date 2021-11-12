function CarregarTabelaBoletos() {
    const tableBoletosConfig = {
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
            {
                data: "Id", name: "Id", orderable: false, width: "5%",
                "render": function (data, type, row) {
                    return `${GetCheckBox('ToggleBoleto', data, data, row.PendenciaBoleto)}`;
                }
            },
            { data: "Nome", name: "Nome", autoWidth: true },
            { data: "DataCadastro", name: "DataCadastro", autoWidth: true },            
            {
                data: "Id", name: "Id", orderable: false, autoWidth: true,
                "render": function (data, type, row) {
                    return `${GetIconWhatsApp(row.Fone, TextoBoleto(row.Nome))}`;
                }
            }
        ],
        order: [
            [2, "asc"]
        ],
        initComplete: function () {
            $('.i-checks-green').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green'
            });
            $('.i-checks-green').on('ifClicked', function (event) {
                ToggleBoleto($(event.target).data("id"));
            });
        },
        ajax: {
            url: '/Participante/GetBoletos',
            data: { EventoId: $("#boletos-eventoid").val() },
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-boletos").DataTable(tableBoletosConfig);
}

$(document).ready(function () {
    CarregarTabelaBoletos();
});

function ToggleBoleto(id) {
    $.ajax({
        url: "/Participante/ToggleBoleto/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                ParticipanteId: id
            })
    });
}

