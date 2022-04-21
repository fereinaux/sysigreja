function CarregarTabelaMeioPagamento() {
    const tableMeioPagamentoConfig = {
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
        buttons: getButtonsConfig('Formas de Pagamento'),
        columns: [
            { data: "Descricao", name: "Descricao", autoWidth: true },
            { data: "Taxa", name: "Taxa", autoWidth: true },
            {
                data: "Id", name: "Id", orderable: false, width: "10%",
                "render": function (data, type, row) {   
                    return `${GetButton('EditMeioPagamento', data, 'blue', 'fa-edit', 'Editar')}                            
                            ${GetButton('DeleteMeioPagamento', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/MeioPagamento/GetMeioPagamentos',
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-meio-pagamento").DataTable(tableMeioPagamentoConfig);
}

function GetMeioPagamento(id) {
    if (id > 0) {
        $.ajax({
            url: "/MeioPagamento/GetMeioPagamento/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#meio-pagamento-id").val(data.MeioPagamento.Id);
                $("#meio-pagamento-descricao").val(data.MeioPagamento.Descricao);
                $("#meio-pagamento-taxa").val(data.MeioPagamento.Taxa);
            }
        });
    }
    else {
        $("#meio-pagamento-id").val(0);
        $("#meio-pagamento-descricao").val("");
        $("#meio-pagamento-taxa").val("");
    }
}

function EditMeioPagamento(id) {
    GetMeioPagamento(id);
    $("#modal-meio-pagamento").modal();
}

function DeleteMeioPagamento(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/MeioPagamento/DeleteMeioPagamento/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaMeioPagamento();
                }
            });
        }
    });
}

function PostMeioPagamento() {
    if (ValidateForm(`#form-meio-pagamento`)) {
        $.ajax({
            url: "/MeioPagamento/PostMeioPagamento/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#meio-pagamento-id").val(),                                        
                    Descricao: $("#meio-pagamento-descricao").val(),
                    Taxa: Number($("#meio-pagamento-taxa").val())
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaMeioPagamento();
                $("#modal-meio-pagamento").modal("hide");
            }
        });
    } 
}

$(document).ready(function () {
    CarregarTabelaMeioPagamento();
});
