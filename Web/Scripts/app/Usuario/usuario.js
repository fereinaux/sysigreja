function CarregarTabelaUsuario() {
    const tableUsuarioConfig = {
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
        buttons: getButtonsConfig('Usuarios'),
        columns: [
            { data: "UserName", name: "UserName", autoWidth: true },
            { data: "Perfil", name: "Perfil", autoWidth: true },
            {
                data: "Id", name: "Id", orderable: false, width: "15%",
                "render": function (data, type, row) {
                    if (!(Ativo == row.Status)) {
                        var color = 'red';
                        var title = 'Inativo';
                    } else {
                        var color = 'green';
                        var title = 'Ativo';
                    }

                    return `${GetLabel('ToggleUsuarioStatus', "'" + data + "'", color, title)}
                            ${GetButton('EditUsuario', '"' + data + '"', 'blue', 'fa-edit', 'Editar')}
                            ${GetButton('DeleteUsuario', '"' + data + '"', 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Account/GetUsuarios',
            datatype: "json",
            type: "POST"
        }
    };
    $("#table-usuarios").DataTable(tableUsuarioConfig);
}

function GetUsuario(id) {
    if (id) {
        $.ajax({
            url: "/Account/GetUsuario/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#usuario-id").val(data.Usuario.Id);
                $("#usuario-login").val(data.Usuario.UserName);
                $("#usuario-senha").val(data.Usuario.Senha);
                $("#usuario-oldsenha").val(data.Usuario.Senha);
                $(`input[type=radio][value=${data.Usuario.Perfil}]`).iCheck('check');
                $("#usuario-equipanteid").val(data.Usuario.EquipanteId > 0 ? data.Usuario.EquipanteId : "Selecione").trigger("chosen:updated");
            }
        });
    }
    else {
        $("#usuario-id").val("");
        $("#usuario-login").val("");
        $("#usuario-senha").val("");
        $("#usuario-oldsenha").val("");
        $(`input[type=radio][value=1]`).iCheck('check');
        $("#usuario-equipanteid").val("Selecione").trigger("chosen:updated");
    }
}

function EditUsuario(id) {
    GetEquipantes(id);
    $("#modal-usuarios").modal();
}

function ToggleUsuarioStatus(id) {
    $.ajax({
        url: "/Account/ToggleUsuarioStatus/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                Id: id
            }),
        success: function () {
            CarregarTabelaUsuario();
        }
    });
}

function DeleteUsuario(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/Account/DeleteUsuario/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    CarregarTabelaUsuario();
                }
            });
        }
    })
}

function PostUsuario() {
    if (ValidateForm(`#form-usuario`)) {
        $.ajax({
            url: "/Account/Register/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#usuario-id").val(),
                    UserName: $("#usuario-login").val(),
                    Password: $("#usuario-senha").val(),
                    Perfil: $("input[type=radio][name=usuario-perfil]:checked").val(),
                    OldPassword: $("#usuario-oldsenha").val(),
                    EquipanteId: $("#usuario-equipanteid").val() != "Selecione" ? $("#usuario-equipanteid").val() : 0
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaUsuario();
                $("#modal-usuarios").modal("hide");
            }
        });
    }
}

$(document).ready(function () {
    CarregarTabelaUsuario();

    $(".password-click").mousedown(function () {
        $("#usuario-senha").attr("type", "text");
    });

    $(".password-click").mouseup(function () {
        $("#usuario-senha").attr("type", "password");
    });
});

function GetEquipantes(id) {

    $("#usuario-equipanteid").empty();
    $('#usuario-equipanteid').append($('<option>Selecione</option>'));

    $.ajax({
        url: "/Account/GetEquipantes/",
        data: { Id: id },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            data.Equipantes.forEach(function (equipante, index, array) {
                $('#usuario-equipanteid').append($(`<option value="${equipante.Id}">${equipante.Nome}</option>`));
            });
            GetUsuario(id);
        }
    });

}