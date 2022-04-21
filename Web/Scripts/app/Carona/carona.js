const map = initMap('map')
const markerLayer = createMarkerLayer(map)
map.setView([-8.050000, -34.900002], 10);
setInterval(function () {
    map.invalidateSize();
}, 100);

function CarregarTabelaCarona() {

    $('#gerenciar').text("Gerenciar Caronas");
    $('#participantes-sem').text("Participantes sem Carona");
    var columnsTb = [
        { data: "Motorista", name: "Motorista", autoWidth: true },
        { data: "Capacidade", name: "Capacidade", autoWidth: true },
        {
            data: "Id", name: "Id", orderable: false, width: "15%",
            "render": function (data, type, row) {
                return `
                            ${GetButton('PrintCarona', JSON.stringify(row), 'green', 'fa-print', 'Imprimir')}  
                            ${GetButton('EditCarona', JSON.stringify(row), 'blue', 'fa-edit', 'Editar')}                            
                            ${GetButton('DeleteCarona', data, 'red', 'fa-trash', 'Excluir')}`;
            }
        }
    ]


    const tableCaronaConfig = {
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
        columns: columnsTb,
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Carona/GetCaronas',
            datatype: "json",
            data: { EventoId: $("#carona-eventoid").val() },
            type: "POST"
        }
    };
    $("#table-carona").DataTable(tableCaronaConfig);
}

$(document).ready(function () {

    $("#Participante").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#table-participantes tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    CaronaRefresh()
});


function CaronaRefresh() {

    CarregarTabelaCarona();
    GetParticipantesSemCarona();
    GetCaronasComParticipantes();
}

function PrintCarona(row) {
    $.ajax({
        url: '/Participante/GetParticipantesByCarona',
        data: { CaronaId: row.Id },
        datatype: "json",
        type: "GET",
        success: (result) => {
            var doc = CriarPDFA4();

            var evento = $("#carona-eventoid option:selected").text()


            var img = new Image();
            img.src = `/Images/logo-preto.png`;

            doc.setFont('helvetica', "normal")
            doc.setFontSize(12);
            doc.addImage(img, 'PNG', 10, 10, 64, 21);
            doc.text(77, 15, $("#carona-eventoid option:selected").text());



            doc.text(77, 20, `Carona ${row.Cor}`);
            doc.text(77, 25, `${row.Dirigente1}`);

            doc.text(77, 30, `Data de Impressão: ${moment().format('DD/MM/YYYY HH:mm')}`);;
            doc.line(10, 38, 195, 38);

            doc.setFont('helvetica', "bold")
            doc.text(12, 43, "Nome");
            doc.text(117, 43, "Apelido");
            doc.text(152, 43, "Whatsapp");

            doc.line(10, 45, 195, 45);
            doc.setFont('helvetica', "normal")
            height = 50;

            $(result.data).each((index, participante) => {
                doc.text(12, height, participante.Nome);
                doc.text(117, height, participante.Apelido);
                doc.text(152, height, participante.Fone);
                height += 6;
            });

            AddCount(doc, result.data, height);

            printDoc(doc);
        }
    });
}

function GetCarona(id, cor) {
    if (id > 0) {
        $.ajax({
            url: "/Carona/GetCarona/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#carona-id").val(data.Carona.Id);
                $('#carona-motorista').append($(`<option value="${data.Carona.MotoristaId}">${data.Carona.Motorista}</option>`));
                $("#carona-motorista").val(data.Carona.MotoristaId).trigger("chosen:updated");



            }
        });
    }
    else {
        $("#carona-id").val(0);
    }
}

function EditCarona(row) {
    GetEquipantes();
    GetCarona(row.Id, row.Cor);
    $("#modal-carona").modal();
}


function DeleteCarona(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/Carona/DeleteCarona/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaCarona();
                }
            });
        }
    });
}

function PostCarona() {
    if (ValidateForm(`#form-carona`)) {
        $.ajax({
            url: "/Carona/PostCarona/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#carona-id").val(),
                    EventoId: $("#carona-eventoid").val(),
                    MotoristaId: $("#carona-motorista").val(),
                    Capacidade: $("#carona-capacidade").val()                    
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaCarona();
                GetCaronasComParticipantes();
                $("#modal-carona").modal("hide");
            }
        });
    }
}

function DistribuirCaronas() {
    $.ajax({
        url: "/Carona/DistribuirCaronas/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                EventoId: $("#carona-eventoid").val()
            }),
        success: function () {
            SuccessMesageOperation();
            CarregarTabelaCarona();
            GetParticipantesSemCarona();
            GetCaronasComParticipantes();
            $("#modal-carona").modal("hide");
        }
    });
}


function GetEquipantes(id) {
    $("#carona-motorista").empty();

    $.ajax({
        url: "/Carona/GetEquipantes/",
        data: { EventoId: $("#carona-eventoid").val(),  },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            data.Equipantes.forEach(function (equipante, index, array) {
                $('#carona-motorista').append($(`<option value="${equipante.Id}">${equipante.Nome}</option>`));
            });
            $("#carona-motorista").val($("#carona-motorista option:first").val()).trigger("chosen:updated");
            if ($("#carona-equipantes option").length === 0 && id == 0) {
                ErrorMessage("Não existem Equipantes disponíveis");
                $("#modal-carona").modal("hide");
            }
        }
    });
}


function GetParticipantesSemCarona() {
    $("#table-participantes").empty();

    $.ajax({
        url: "/Carona/GetParticipantesSemCarona/",
        data: { EventoId: $("#carona-eventoid").val() },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            data.Participantes.forEach(function (participante, index, array) {
                $('#table-participantes').append($(`<tr><td class="participante" data-id="${participante.Id}">${participante.Nome}</td></tr>`));
            });

            DragDropg();
        }
    });
}



function addMapa(lat, long, nome, cor,id) {
    var marker = L.marker([lat, long], { icon: getIcon(cor.toLowerCase().replaceAll(' ', '-')) }).on('click', function (e) { clickMarker(id) }).addTo(markerLayer);


}

function clickMarker(id) {
    $.ajax({
        url: "/Participante/GetParticipante/",
        data: { Id: id },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#participante-nome").text(data.Participante.Nome)
            $("#participante-id").val(data.Participante.Id)

            $('#participante-cor').val($(`#participante-cor option:contains(${data.DadosAdicionais.Carona})`).val()).trigger("chosen:updated");
            $("#modal-cores").modal();
        }
    })

   
}

$("#modal-cores").on('hidden.bs.modal', function () {
    ChangeCarona($("#participante-id").val(), $('#participante-cor').val())
});


function GetCaronasComParticipantes() {
    $("#caronas").empty();

    $.ajax({
        url: '/Carona/GetCaronas',
        datatype: "json",
        data: { EventoId: $("#carona-eventoid").val() },
        type: "POST",
        success: function (data) {
            data.data.forEach(function (carona, index, array) {
                $("#caronas").append($(`<div data-id="${carona.Id}" style="margin-bottom:25px;background-color:${GetCor(carona.Cor)};background-clip: content-box;border-radius: 28px;" class="p-xs col-xs-12 col-lg-4 pg text-center text-white">                     
                  <h4 style="padding-top:5px">${carona.Dirigente1}</h4>                    
                                    <table class="table">
                                        <tbody id="pg-${carona.Id}">
                                            
                                        </tbody>
                                    </table>
 <button type="button" class="btn btn-rounded btn-default print-button" onclick='PrintCarona(${JSON.stringify(carona)})'><i class="fa fa-2x fa-print"></i></button>
                                </div>`));
            });

            $.ajax({
                url: "/Carona/GetCaronasComParticipantes/",
                data: { EventoId: $("#carona-eventoid").val() },
                datatype: "json",
                type: "GET",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {                   
                    markerLayer.getLayers().forEach(mark => mark.remove())
                    data.Caronas.forEach(function (carona, index, array) {
                        if (carona.Latitude && carona.Longitude) {
                            addMapa(carona.Latitude, carona.Longitude, carona.Nome, carona.Cor, carona.ParticipanteId)

                        }

                        $(`#pg-${carona.CaronaId}`).append($(`<tr><td class="participante" data-id="${carona.ParticipanteId}">${carona.Nome}</td></tr>`));
                    });
                    $('.div-map').css('display', 'block')
                    DragDropg();
                }
            });
        }
    });
}

function DragDropg() {
    Drag();

    $('.pg').droppable({
        drop: function (event, ui) {
            $(ui.draggable).parent().remove();
            ChangeCarona($(ui.draggable).data('id'), $(this).data('id'));
            if ($(this).data('id')) {
                $(`#pg-${$(this).data('id')}`).append($(`<tr><td class="participante" data-id="${$(ui.draggable).data('id')}">${$(ui.draggable).text()}</td></tr>`));
            } else {
                $('#table-participantes').append($(`<tr><td class="participante" data-id="${$(ui.draggable).data('id')}">${$(ui.draggable).text()}</td></tr>`));
            }
            Drag();
        }
    });
}

function Drag() {
    $('.participante').each(function () {
        $(this).css("cursor", "move");
        $(this).draggable({
            zIndex: 999,
            revert: true,
            revertDuration: 0
        });
    });
}

function ChangeCarona(participanteId, destinoId) {
    $.ajax({
        url: "/Carona/ChangeCarona/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                ParticipanteId: participanteId,
                DestinoId: destinoId
            }),
        success: function () {
            CaronaRefresh();
        }
    });
}

