let EquipeId = 0
let Titulo = ''

function CarregarTabelaEquipe() {
    const tableEquipeConfig = {
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
        dom: domConfigNoButtons,
        columns: [
            { data: "Equipe", name: "Equipe", autoWidth: true },
            { data: "QuantidadeMembros", name: "QuantidadeMembros", autoWidth: true },
            {
                data: "Id", name: "Id", orderable: false, width: "15%",
                "render": function (data, type, row) {
                    return `${GetButton('ListarEquipe', JSON.stringify(row), 'blue', 'fa-list-alt', 'Listar membros da Equipe')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Equipe/GetEquipes',
            datatype: "json",
            data: { EventoId: $("#equipe-eventoid").val() },
            type: "POST"
        }
    };
    $("#table-equipe").DataTable(tableEquipeConfig);
}

function header(doc, evento, page) {

    var img = new Image();
    img.src = `/Images/logo-preto.png`;
    doc.setFont("helvetica", "normal");
    doc.setFontSize(12);
    doc.addImage(img, 'PNG', 10, 10, 50, 21);
    doc.text(64, 14, evento);
    doc.text(64, 22, Titulo);
    doc.text(64, 30, `Data de Impressão: ${moment().format('DD/MM/YYYY HH:mm')} - Página ${page}`);

    var widthP = 285
    doc.line(10, 38, widthP, 38);

    doc.setFont('helvetica', "bold")
    doc.text(17, 43, "Nome Completo");
    doc.text(127, 43, "Crachá");
    doc.text(172, 43, "Idade/Data de Nascimento");
    doc.text(240, 43, "Whatsapp");

    doc.line(10, 45, widthP, 45);
    doc.setFont('helvetica', "normal")
}

function PrintEquipe() {
    $.ajax({
        url: '/Equipe/GetMembrosEquipe',
        data: { EventoId: $("#equipe-eventoid").val(), EquipeId: EquipeId },
        datatype: "json",
        type: "POST",
        success: (result) => {
            var doc = new jsPDF('l', 'mm', "a4");
            var widthP = 285
            var evento = $("#equipe-eventoid option:selected").text();
            header(doc, evento, 1)


            height = 50;

            $(result.data).each((index, participante) => {
                if (index == 19) {
                    doc.addPage()
                    header(doc, evento, 2)
                    height = 50;
                }

                doc.setFont()

                doc.setFont("helvetica", participante.Tipo == 'Coordenador' ? "bold" : "normal");
                doc.text(17, height, participante.Tipo == 'Coordenador' ? `Coord: ${participante.Nome}` : participante.Nome);
                doc.text(127, height, participante.Apelido);
                doc.text(195, height, `${participante.Idade}`);
                doc.text(240, height, `${participante.Fone}`);
                height += 2;
                doc.line(10, height, widthP, height);
                height += 6;

            });

            for (var i = height; i < 192; i += 8) {
                doc.line(10, i, widthP, i);
            }

            AddCount(doc, result.data, 200, widthP);

            printDoc(doc);
        }
    });
}

function CarregarTabelaMembrosEquipe(equipeId, titulo) {
    const tableMembrosEquipeConfig = {
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
        buttons: getButtonsConfig(titulo),
        columns: [
            { data: "Nome", name: "Nome", autoWidth: true },
            { data: "Idade", name: "Idade", width: "5%" },
            {
                data: "Id", name: "Id", orderable: false, width: "35%",
                "render": function (data, type, row) {
                    var color = !(Coordenador == row.Tipo) ? 'info' : 'yellow';

                    return `${GetLabel('ToggleMembroEquipeTipo', data, color, row.Tipo)}
                            ${GetButton('DeleteMembroEquipe', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Equipe/GetMembrosEquipe',
            data: { EventoId: $("#equipe-eventoid").val(), EquipeId: equipeId },
            datatype: "json",
            type: "POST"
        }
    };
    GetEquipantes();
    $("#table-membros-equipe").DataTable(tableMembrosEquipeConfig);
}

$(document).ready(function () {
    CarregarTabelaEquipe();
});

$("#modal-membros-equipe").on('hidden.bs.modal', function () {
    CarregarTabelaEquipe();
});

function ListarEquipe(row) {
    $("#equipe-equipantes").val("Pesquisar").trigger("chosen:updated");
    EquipeId = row.Id
    Titulo = `${row.Equipe} - ${$("#equipe-eventoid option:selected").text()}`;
    CarregarTabelaMembrosEquipe(row.Id, Titulo);
    $("#equipe-id").val(row.Id);
    $('.titulo-equipe').text(Titulo);
    $("#modal-membros-equipe").modal();
}

function AddMembroEquipe() {
    if ($("#equipe-equipantes").val() != "Pesquisar") {
        $.ajax({
            url: "/Equipe/AddMembroEquipe/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    EquipanteId: $("#equipe-equipantes").val(),
                    EventoId: $("#equipe-eventoid").val(),
                    Equipe: $("#equipe-id").val()
                }),
            success: function () {
                SuccessMesageOperation();
                $("#equipe-equipantes").val("Pesquisar").trigger("chosen:updated");
                CarregarTabelaMembrosEquipe($("#equipe-id").val(), $('.titulo-equipe').text());
            }
        });
    }
}

function ToggleMembroEquipeTipo(id) {
    $.ajax({
        url: "/Equipe/ToggleMembroEquipeTipo/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                Id: id
            }),
        success: function () {
            CarregarTabelaMembrosEquipe($("#equipe-id").val(), $('.titulo-equipe').text());
        },
        error: function (error) {
            ErrorMessage(error.statusText);
        }
    });
}

function DeleteMembroEquipe(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/Equipe/DeleteMembroEquipe/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaMembrosEquipe($("#equipe-id").val(), $('.titulo-equipe').text());
                }
            });
        }
    });
}

function GetEquipantes() {

    $("#equipe-equipantes").empty();
    $('#equipe-equipantes').append($('<option>Pesquisar</option>'));

    $.ajax({
        url: "/Equipe/GetEquipantes/",
        data: { EventoId: $("#equipe-eventoid").val() },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            data.Equipantes.forEach(function (equipante, index, array) {
                $('#equipe-equipantes').append($(`<option value="${equipante.Id}">${equipante.Nome}</option>`));
            });
            $("#equipe-equipantes").val("Pesquisar").trigger("chosen:updated");
        }
    });

}
