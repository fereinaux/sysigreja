function CarregarTabelaContaBancaria() {
    const tableContaBancariaConfig = {
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
        buttons: getButtonsConfig('Contas Bancárias'),
        columns: [
            { data: "Banco", name: "Banco", autoWidth: true },
            { data: "Conta", name: "Conta", autoWidth: true },
            { data: "Agencia", name: "Agencia", autoWidth: true },            
            {
                data: "Id", name: "Id",orderable: false, width: "15%",
                "render": function (data, type, row) {
                    return `${GetButton('EditContaBancaria', data, 'blue', 'fa-edit', 'Editar')}                            
                            ${GetButton('DeleteContaBancaria', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [1, "asc"]
        ],
        ajax: {
            url: '/ContaBancaria/GetContasBancarias',
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-contas-bancarias").DataTable(tableContaBancariaConfig);
}

function GetContaBancaria(id) {
    if (id > 0) {
        $.ajax({
            url: "/ContaBancaria/GetContaBancaria/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#conta-bancaria-id").val(data.ContaBancaria.Id);
                $("#conta-bancaria-nome").val(data.ContaBancaria.Nome);
                $("#conta-bancaria-operacao").val(data.ContaBancaria.Operacao);
                $("#conta-bancaria-conta").val(data.ContaBancaria.Conta);
                $("#conta-bancaria-agencia").val(data.ContaBancaria.Agencia);
                $("#conta-bancaria-cpf").val(data.ContaBancaria.CPF);
                $("#conta-bancaria-banco").val(data.ContaBancaria.Banco).trigger("chosen:updated");
            }
        });
    }
    else {
        $("#conta-bancaria-id").val(0);
        $("#conta-bancaria-nome").val("");
        $("#conta-bancaria-cpf").val("");
        $("#conta-bancaria-conta").val("");
        $("#conta-bancaria-agencia").val("");
        $("#conta-bancaria-operacao").val("");
        $("#conta-bancaria-banco").val($("#conta-bancaria-banco option:first").val()).trigger("chosen:updated");
    }
}

function EditContaBancaria(id) {
    GetContaBancaria(id);
    $("#modal-contas-bancarias").modal();
}

function DeleteContaBancaria(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/ContaBancaria/DeleteContaBancaria/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaContaBancaria();
                }
            });
        }
    });
}

function PostContaBancaria() {
    if (ValidateForm(`#form-conta-bancaria`)) {
        $.ajax({
            url: "/ContaBancaria/PostContaBancaria/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#conta-bancaria-id").val(),                                        
                    Nome: $("#conta-bancaria-nome").val(),
                    CPF: $("#conta-bancaria-cpf").val(),
                    Conta: $("#conta-bancaria-conta").val(),
                    Agencia: $("#conta-bancaria-agencia").val(),
                    Operacao: $("#conta-bancaria-operacao").val(),
                    Banco: $("#conta-bancaria-banco").val()
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaContaBancaria();
                $("#modal-contas-bancarias").modal("hide");
            }
        });
    } 
}

$(document).ready(function () {
    CarregarTabelaContaBancaria();
});


