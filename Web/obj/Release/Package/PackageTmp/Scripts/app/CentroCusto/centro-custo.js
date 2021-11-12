function CarregarTabelaCentroCusto() {
    const tableCentroCustoConfig = {
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
        dom: domConfig,
        buttons: getButtonsConfig('Centros de Custo'),
        columns: [
            { data: "Descricao", name: "Descricao", autoWidth: true },   
            { data: "Tipo", name: "Tipo", autoWidth: true },   
            {
                data: "Id", name: "Id", orderable: false, width: "25%",
                "render": function (data, type, row) {

                    return `${GetButton('EditCentroCusto', data, 'blue', 'fa-edit', 'Editar')}                            
                            ${GetButton('DeleteCentroCusto', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [1, "asc"]
        ],
        ajax: {
            url: '/CentroCusto/GetCentroCustos',
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-centro-custos").DataTable(tableCentroCustoConfig);
}

function GetCentroCusto(id) {
    if (id > 0) {
        $.ajax({
            url: "/CentroCusto/GetCentroCusto/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#centro-custo-id").val(data.CentroCusto.Id);
                $("#centro-custo-descricao").val(data.CentroCusto.Descricao);
                $("#centro-custo-tipo").val(data.CentroCusto.Tipo).trigger("chosen:updated");
            }
        });
    }
    else {
        $("#centro-custo-id").val(0);
        $("#centro-custo-descricao").val("");
        $("#centro-custo-tipo").val($("#centro-custo-tipo option:first").val()).trigger("chosen:updated");
    }
}

function EditCentroCusto(id) {
    GetCentroCusto(id);
    $("#modal-centro-custos").modal();
}

function DeleteCentroCusto(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/CentroCusto/DeleteCentroCusto/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaCentroCusto();
                }
            });
        }
    });
}

function PostCentroCusto() {
    if (ValidateForm(`#form-centro-custo`)) {
        $.ajax({
            url: "/CentroCusto/PostCentroCusto/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#centro-custo-id").val(),                                        
                    Descricao: $("#centro-custo-descricao").val(),
                    Tipo: $("#centro-custo-tipo").val()
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaCentroCusto();
                $("#modal-centro-custos").modal("hide");
            }
        });
    } 
}

$(document).ready(function () {
    CarregarTabelaCentroCusto();
});


