function CarregarTabelaMensagem() {
    const tableMensagemConfig = {
        language: languageConfig,
        lengthMenu: [200, 500, 1000],
        colReorder: false,
        serverSide: false,
        deferloading: 0,
        orderCellsTop: true,
        fixedHeader: true,
        filter: true,
        orderMulti: false,
        responsive: true, stateSave: true,
        destroy: true,
        dom: domConfig,
        buttons: getButtonsConfig('Mensagens'),
        columns: [
            { data: "Titulo", name: "Titulo", autoWidth: true },
            {
                data: "Id", name: "Id", orderable: false, width: "25%",
                "render": function (data, type, row) {

                    return `${GetButton('EditMensagem', data, 'blue', 'fa-edit', 'Editar')}                            
                            ${GetButton('DeleteMensagem', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [1, "asc"]
        ],
        ajax: {
            url: '/Mensagem/GetMensagens',
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-mensagem").DataTable(tableMensagemConfig);
}

function GetMensagem(id) {
    if (id > 0) {
        $.ajax({
            url: "/Mensagem/GetMensagem/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#mensagem-id").val(data.Mensagem.Id);
                $("#mensagem-titulo").val(data.Mensagem.Titulo);
                $("#mensagem-conteudo").val(data.Mensagem.Conteudo);
            }
        });
    }
    else {
        $("#mensagem-id").val(0);
        $("#mensagem-titulo").val("");
        $("#mensagem-conteudo").val("");
    }
}

function EditMensagem(id) {
    GetMensagem(id);
    $("#modal-mensagem").modal();
}

function DeleteMensagem(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/Mensagem/DeleteMensasgem/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaMensagem();
                }
            });
        }
    });
}

function AddText(text) {
    var cursorPos = $('#mensagem-conteudo').prop('selectionStart');
    var v = $('#mensagem-conteudo').val();
    var textBefore = v.substring(0, cursorPos);
    var textAfter = v.substring(cursorPos, v.length);

    $('#mensagem-conteudo').val(textBefore + text + textAfter);
}

function PostMensagem() {
    if (ValidateForm(`#form-mensagem`)) {
        $.ajax({
            url: "/Mensagem/PostMensagem/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#mensagem-id").val(),
                    Titulo: $("#mensagem-titulo").val(),
                    Conteudo: $("#mensagem-conteudo").val()
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaMensagem();
                $("#modal-mensagem").modal("hide");
            }
        });
    }
}

$(document).ready(function () {
    CarregarTabelaMensagem();
});


