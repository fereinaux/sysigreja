function CarregarTabelaEtiquetas() {
    const tableEtiquetasConfig = {
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
        buttons: getButtonsConfig('Marcadores'),
        columns: [
            { data: "Nome", name: "Nome", autoWidth: true },
            {
                data: "Cor", className:"pos-relative", name: "Cor", autoWidth: true, render: function (data, type, row) {

                    return `<span style="background-color:${data};position:absolute;top:0;left:0;right:0;bottom:0"></span>`;
                } },
            {
                data: "Id", name: "Id", orderable: false, width: "25%",
                "render": function (data, type, row) {

                    return `${GetButton('EditEtiqueta', data, 'blue', 'fa-edit', 'Editar')}                            
                            ${GetButton('DeleteEtiqueta', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [1, "asc"]
        ],
        ajax: {
            url: '/Etiqueta/GetEtiquetas',
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-etiquetas").DataTable(tableEtiquetasConfig);
}

function GetEtiqueta(id) {
    if (id > 0) {
        $.ajax({
            url: "/Etiqueta/GetEtiqueta/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#etiqueta-id").val(data.Etiqueta.Id);
                $("#etiqueta-nome").val(data.Etiqueta.Nome);
                $("#etiqueta-cor").val(data.Etiqueta.Cor)
            }
        });
    }
    else {
        $("#etiqueta-id").val(0);
        $("#etiqueta-nome").val("");
        $("#etiqueta-cor").val("#bada55");
    }
}

function EditEtiqueta(id) {
    GetEtiqueta(id);
    $("#modal-etiqueta").modal();
}

function DeleteEtiqueta(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/Etiqueta/DeleteEtiqueta/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaEtiquetas();
                }
            });
        }
    });
}

function PostEtiqueta() {
    if (ValidateForm(`#form-etiqueta`)) {
        $.ajax({
            url: "/Etiqueta/PostEtiqueta/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#etiqueta-id").val(),
                    Nome: $("#etiqueta-nome").val(),
                    Cor: $("#etiqueta-cor").val()
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaEtiquetas();
                $("#modal-etiqueta").modal("hide");
            }
        });
    } 
}

$(document).ready(function () {
    CarregarTabelaEtiquetas();
});


