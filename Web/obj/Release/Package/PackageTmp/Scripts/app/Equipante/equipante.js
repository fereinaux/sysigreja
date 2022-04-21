var realista;
$.ajax({
    url: '/Etiqueta/GetEtiquetas',
    datatype: "json",
    type: "POST",
    success: (result) => {
        $("#equipante-marcadores").html(`
${result.data.map(p => `<option value=${p.Id}>${p.Nome}</option>`)}
`)
        $("#equipante-nao-marcadores").html(`
${result.data.map(p => `<option value=${p.Id}>${p.Nome}</option>`)}
`)
        $('#equipante-marcadores').select2();
        $('#equipante-nao-marcadores').select2();
    }
});

function CarregarTabelaEquipante() {


    const tableEquipanteConfig = {
        language: languageConfig,
        searchDelay: 750,
        lengthMenu: [10, 30, 50, 100, 200],
        colReorder: false,
        serverSide: true,
        deferloading: 0,
        orderCellsTop: true,
        fixedHeader: true,
        filter: true,
        orderMulti: false,
        responsive: true,
        stateSave: true,
        destroy: true,
        dom: domConfig,
        buttons: getButtonsConfig('Equipantes'),
        columns: [
            { data: "Sexo", name: "Sexo", visible: false },
            {
                data: "Sexo", orderData: 0, name: "Sexo", className: "text-center", width: "5%",
                "render": function (data, type, row) {
                    if (data == "Masculino") {
                        icon = "fa-male";
                        cor = "#0095ff";
                    }
                    else {
                        icon = "fa-female";
                        cor = "#ff00d4";
                    }
                    return `<span onclick="ToggleSexo(${row.Id})" style = "font-size:18px;color:${cor};" class="p-l-xs pointer"> <i class="fa ${icon}" aria-hidden="true" title="${data}"></i></span >`;
                }
            },
            {
                data: "Nome", name: "Nome", width: "25%", render: function (data, type, row) {
                    return `<div>
                        <span>${row.Nome}</br></span>
                        ${row.Etiquetas.map(etiqueta => `<span  class="badge m-r-xs" style="background-color:${etiqueta.Cor};color:#fff">${etiqueta.Nome}</span>`).join().replace(/,/g, '')}
                    </div>`
                }
            },
            { data: "Idade", name: "Idade", autoWidth: true },
            { data: "Equipe", name: "Equipe", autoWidth: true },
            { data: "Faltas", name: "Faltas", autoWidth: true },
            {
                data: "HasOferta", name: "HasOferta", autoWidth: true, render: function (data, type, row) {
                    if (row.Status == "Em Espera") {
                        return `<span style="font-size:13px" class="text-center label label-default}">Em Espera</span>`;
                    }
                    return `<span style="font-size:13px" class="text-center label label-${row.HasOferta ? 'primary' : 'warning'}">${row.HasOferta ? 'Pago' : 'Pendente'}</span>`;
                }
            },
            {
                data: "Id", name: "Id", orderable: false, width: "20%",
                "render": function (data, type, row) {
                    return `   

<form enctype="multipart/form-data" id="frm-vacina${data}" method="post" novalidate="novalidate">
${GetAnexosButton('Anexos', data, row.QtdAnexos)}
                                ${!row.HasFoto ? ` <label for="foto${data}" class="inputFile">
                                <span style="font-size:18px" class="text-mutted pointer p-l-xs"><i class="fa fa-camera" aria-hidden="true" title="Foto"></i></span>
                                <input accept="image/*" onchange='Foto(${JSON.stringify(row)})' style="display: none;" class="custom-file-input inputFile" id="foto${data}" name="foto${data}" type="file" value="">
                            </label>`: `<span style="font-size:18px" class="text-success p-l-xs pointer" onclick="toggleFoto(${data})"><i class="fa fa-camera" aria-hidden="true" title="Foto"></i></span>`
                        }
${!row.HasVacina ? ` <label for="arquivo${data}" class="inputFile">
                                <span style="font-size:18px" class="text-mutted pointer p-l-xs"><i class="fa fa-syringe" aria-hidden="true" title="Vacina"></i></span>
                                <input onchange='PostVacina(${data},${JSON.stringify(row)})' style="display: none;" class="custom-file-input inputFile" id="arquivo${data}" name="arquivo${data}" type="file" value="">
                            </label>`: `<span style="font-size:18px" class="text-success p-l-xs pointer" onclick="toggleVacina(${data})"><i class="fa fa-syringe" aria-hidden="true" title="Vacina"></i></span>`}
                           
${GetIconWhatsApp(row.Fone)}
                            ${GetIconTel(row.Fone)}
                            ${GetButton('Pagamentos', JSON.stringify(row), 'verde', 'far fa-money-bill-alt', 'Pagamentos')}
                            ${GetButton('EditEquipante', data, 'blue', 'fa-edit', 'Editar')}                            
                            ${GetButton('DeleteEquipante', data, 'red', 'fa-trash', 'Excluir')}
                        </form> 
`;
                }
            }
        ],
        order: [
            [2, "asc"]
        ],
        ajax: {
            url: '/Equipante/GetEquipantesDataTable',
            
            data: { EventoId: $("#equipante-eventoid-filtro").val() != 999 ? $("#equipante-eventoid-filtro").val() : null, Status: $("#equipante-status").val(), Etiquetas: $("#equipante-marcadores").val(), NaoEtiquetas: $("#equipante-nao-marcadores").val(), Equipe: $("#equipe-select").val() != 999 ? $("#equipe-select").val() : null },
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-equipantes").DataTable(tableEquipanteConfig);
}

function getEquipes() {
    $.ajax({
        url: '/Equipe/GetEquipes',
        datatype: "json",
        type: "POST",
        success: (result) => {
            console.log(result);
            $("#equipe-select").html(`
<option value=999>Selecione</option>
${result.data.map(p => `<option value=${p.Id}>${p.Equipe}</option>`)}
`)
        }
    });
}

function Anexos(id) {
    $("#EquipanteIdModal").val(id);
    $("#LancamentoIdModal").val('');
    $("#EventoIdModal").val($("#equipante-eventoid").val());
    GetAnexos(id);
    $("#modal-anexos").modal();
}


function GetAnexosLancamento(id) {
    const tableArquivoConfig = {
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
            { data: "Nome", name: "Nome", autoWidth: true },
            { data: "Extensao", name: "Extensao", autoWidth: true },
            {
                data: "Id", name: "Id", orderable: false, width: "15%",
                "render": function (data, type, row) {
                    return `${GetButton('GetArquivo', data, 'blue', 'fa-download', 'Download')}
                            ${GetButton('DeleteArquivo', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Arquivo/GetArquivosLancamento',
            data: { id: id ? id : $("#LancamentoIdModal").val() },
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-anexos").DataTable(tableArquivoConfig);
}

function AnexosLancamento(row) {
    $("#LancamentoIdModal").val(row.Id);
    $("#ParticipanteIdModal").val(row.ParticipanteId);
    GetAnexosLancamento(row.Id)
    $("#modal-pagamentos").modal('hide');
    $("#modal-anexos").modal();
}




function GetAnexos(id) {
    const tableArquivoConfig = {
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
            { data: "Nome", name: "Nome", autoWidth: true },
            { data: "Extensao", name: "Extensao", autoWidth: true },
            {
                data: "Id", name: "Id", orderable: false, width: "15%",
                "render": function (data, type, row) {
                    return `${GetButton('GetArquivo', data, 'blue', 'fa-download', 'Download')}
                            ${GetButton('DeleteArquivo', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Arquivo/GetArquivosEquipanteEvento',
            data: { equipanteid: id ? id : $("#EquipanteIdModal").val(), eventoid: $("#equipante-eventoid").val() },
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-anexos").DataTable(tableArquivoConfig);
}

function GetArquivo(id) {
    window.open(`/Arquivo/GetArquivo/${id}`)
}

function DeleteArquivo(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/Arquivo/DeleteArquivo/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    GetAnexos();
                }
            });
        }
    });
}

function ConfirmFoto() {

    $("#main-cropper")
        .croppie("result", {
            type: "canvas",
            size: { height: 750, width: 500 }
        })
        .then(function (resp) {
            var dataToPost = new FormData();
            dataToPost.set('EquipanteId', realista.Id)
            dataToPost.set('Arquivo', dataURLtoFile(resp, `Foto ${realista.Nome}.jpg`))
            dataToPost.set('IsFoto', true)
            $.ajax(
                {
                    processData: false,
                    contentType: false,
                    type: "POST",
                    data: dataToPost,
                    url: "Arquivo/PostArquivo",
                    success: function () {
                        $("#modal-fotos").modal("hide");
                        CarregarTabelaEquipante()

                    }
                });
        });
}

function dataURLtoFile(dataurl, filename) {

    var arr = dataurl.split(','),
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]),
        n = bstr.length,
        u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime })

}


function Foto(row) {

    realista = row
    console.log(realista)

    var input = $(`#foto${realista.Id}`)[0]

    const file = input.files[0];


    if (!file) {
        return;
    }

    new Compressor(file, {
        quality: 0.6,
        convertSize: 1000000 ,
        // The compression process is asynchronous,
        // which means you have to access the `result` in the `success` hook function.
        success(result) {

            var reader = new FileReader();

            reader.onload = function (e) {
                $("#main-cropper").croppie("bind", {
                    url: e.target.result
                });

            };

            reader.readAsDataURL(result);
   

            $("#modal-fotos").modal();
            var boundaryWidth = $("#fotocontent").width();

            var boundaryHeight = boundaryWidth * 1.5;

            var viewportWidth = boundaryWidth - (boundaryWidth / 100 * 25);

            var viewportHeight = boundaryHeight - (boundaryHeight / 100 * 25);
            console.log(boundaryWidth, boundaryHeight, viewportHeight, viewportWidth)

            $("#main-cropper").croppie({

                viewport: { width: viewportWidth, height: viewportHeight },
                boundary: { width: boundaryWidth, height: boundaryHeight },
                enableOrientation: true,
                showZoomer: true,
                enableExif: true,
                enableResize: false,

            });
        },
        error(err) {
            console.log(err.message);
        },
    });
}

function toggleFoto(id) {
    ConfirmMessage("Essa ação removerá a foto, deseja continuar?").then((result) => {
        if (result) {
            $.ajax(
                {
                    datatype: "json",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    url: "Arquivo/DeleteFotoEquipante",
                    data: JSON.stringify(
                        {
                            Id: id
                        }),

                    success: function () {
                        CarregarTabelaEquipante()

                    }
                });
        }
    }
    )
}

function PostVacina(id,realista) {
    var dataToPost = new FormData($(`#frm-vacina${id}`)[0]);
    dataToPost.set('EquipanteId', id)
    var filename = dataToPost.get(`arquivo${id}`).name
    var arquivo = new File([dataToPost.get(`arquivo${id}`)], 'Vacina ' + realista.Nome + filename.substr(filename.indexOf('.')));
    dataToPost.set('Arquivo', arquivo)
    $.ajax(
        {
            processData: false,
            contentType: false,
            type: "POST",
            data: dataToPost,
            url: "Arquivo/PostArquivo",
            success: function () {
                toggleVacina(id)

            }
        });
}

function toggleVacina(id) {
    $.ajax(
        {
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: "Equipante/ToggleVacina",
            data: JSON.stringify(
                {
                    Id: id
                }),

            success: function () {
                CarregarTabelaEquipante()

            }
        });
}

function PostArquivo() {

    var dataToPost = new FormData($('#frm-upload-arquivo-modal')[0]);
    var filename = dataToPost.get('arquivo-modal').name
    var arquivo = new File([dataToPost.get('arquivo-modal')], 'Pagamento ' + realista.Nome + filename.substr(filename.indexOf('.')));
    dataToPost.set('Arquivo', arquivo)
    dataToPost.set('EventoId', $("#equipante-eventoid").val())
    dataToPost.set('EquipanteId', dataToPost.get('EquipanteIdModal'))
    dataToPost.set('LancamentoId', dataToPost.get('LancamentoIdModal'))
    $.ajax(
        {
            processData: false,
            contentType: false,
            type: "POST",
            data: dataToPost,
            url: "Arquivo/PostArquivo",
            success: function () {
                if (dataToPost.get('LancamentoIdModal')) {
                    GetAnexosLancamento();
                } else {
                    GetAnexos();
                }

            }
        });
}

$("#arquivo-modal").change(function () {
    PostArquivo();
});

$("#equipante-eventoid").change(function () {
    GetAnexos($("#EquipanteIdModal").val())
});


$("#modal-anexos").on('hidden.bs.modal', function () {
    CarregarTabelaEquipante()
});

$("#modal-pagamentos").on('hidden.bs.modal', function () {
    if (!$('#LancamentoIdModal').val()) {
        CarregarTabelaEquipante();

    }
})



function ToggleSexo(id) {
    ConfirmMessage("Confirma a mudança de gênero?").then((result) => {
        if (result) {
            $.ajax({
                url: "/Equipante/ToggleSexo/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageOperation();
                    CarregarTabelaEquipante();
                }
            });
        }
    });
}



function CarregarTabelaPagamentos(id) {
    const tablePagamentosConfig = {
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
            { data: "FormaPagamento", name: "FormaPagamento", autoWidth: true },
            { data: "Valor", name: "Valor", autoWidth: true },
            {
                data: "Id", name: "Id", orderable: false, width: "15%",
                "render": function (data, type, row) {
                    return `${GetAnexosButton('AnexosLancamento', JSON.stringify(row), row.QtdAnexos)}
                            ${GetIconWhatsApp($("#pagamentos-whatsapp").val(), RebciboPagamento(row.Valor, row.FormaPagamento))}
                            ${GetButton('DeletePagamento', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Lancamento/GetPagamentos',
            data: { EquipanteId: id },
            datatype: "json",
            type: "POST"
        }
    };
    $("#table-pagamentos").DataTable(tablePagamentosConfig);
}

function Pagamentos(row) {
    $("#pagamentos-whatsapp").val(row.Fone);
    $("#pagamentos-valor").val($("#pagamentos-valor").data("valor"));
    $("#pagamentos-equipanteid").val(row.Id);
    $("#pagamentos-meiopagamento").val($("#pagamentos-meiopagamento option:first").val());
    CarregarTabelaPagamentos(row.Id);
    realista = row
    $("#modal-pagamentos").modal();
}

function GetEquipante(id) {
                $('.equipante-etiquetas').select2({ dropdownParent: $("#form-equipante") });
    if (id > 0) {
        $.ajax({
            url: "/Equipante/GetEquipante/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#equipante-id").val(data.Equipante.Id);
                $(`#equipante-nome`).val(data.Equipante.Nome);
                $(`#equipante-apelido`).val(data.Equipante.Apelido);
                $("#equipante-data-nascimento").val(moment(data.Equipante.DataNascimento).format('DD/MM/YYYY'));
                $(`#equipante-email`).val(data.Equipante.Email);
                $(`#equipante-fone`).val(data.Equipante.Fone);
                $(`#equipante-restricaoalimentar`).val(data.Equipante.RestricaoAlimentar);
                $(`#equipante-medicacao`).val(data.Equipante.Medicacao);
                $(`#equipante-alergia`).val(data.Equipante.Alergia);
                $(`input[type=radio][name=equipante-sexo][value=${data.Equipante.Sexo}]`).iCheck('check');
                $(`input[type=radio][name=equipante-hasalergia][value=${data.Equipante.HasAlergia}]`).iCheck('check');
                $(`input[type=radio][name=equipante-hasmedicacao][value=${data.Equipante.HasMedicacao}]`).iCheck('check');
                $(`input[type=radio][name=equipante-hasrestricaoalimentar][value=${data.Equipante.HasRestricaoAlimentar}]`).iCheck('check');
                $('#equipante-etiquetas').html(`${data.Etiquetas.map(etiqueta => `<option data-cor="${etiqueta.Cor}" value=${etiqueta.Id}>${etiqueta.Nome}</option>`)
                    }`)
                $('#equipante-etiquetas').val(data.Equipante.EtiquetasList.map(etiqueta => etiqueta.Id))
                $("#equipante-numeracao").val(data.Equipante.Numeracao);
            }
        });
    }
    else {
        $("#equipante-id").val(0);
        $(`#equipante-nome`).val("");
        $(`#equipante-apelido`).val("");
        $("#equipante-data-nascimento").val("");
        $(`#equipante-email`).val("");
        $(`#equipante-fone`).val("");
        $(`#equipante-restricaoalimentar`).val("");
        $(`#equipante-medicacao`).val("");
        $(`#equipante-alergia`).val("");
        $(`input[type=radio][name=equipante-sexo][value=1]`).iCheck('check');
        $(`input[type=radio][name=equipante-hasalergia][value=false]`).iCheck('check');
        $(`input[type=radio][name=equipante-hasmedicacao][value=false]`).iCheck('check');
        $(`input[type=radio][name=equipante-hasrestricaoalimentar][value=false]`).iCheck('check');
    }
}

function EditEquipante(id) {
    GetEquipante(id);
    $("#modal-equipantes").modal();
}

function DeleteEquipante(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/Equipante/DeleteEquipante/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaEquipante();
                }
            });
        }
    });
}

function PostEquipante() {
    if (ValidateForm(`#form-equipante`)) {
        $.ajax({
            url: "/Equipante/PostEquipante/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#equipante-id").val(),
                    Nome: $(`#equipante-nome`).val(),
                    Apelido: $(`#equipante-apelido`).val(),
                    DataNascimento: moment($("#equipante-data-nascimento").val(), 'DD/MM/YYYY', 'pt-br').toJSON(),
                    Email: $(`#equipante-email`).val(),
                    Fone: $(`#equipante-fone`).val(),
                    HasRestricaoAlimentar: $("input[type=radio][name=equipante-hasrestricaoalimentar]:checked").val(),
                    RestricaoAlimentar: $(`#equipante-restricaoalimentar`).val(),
                    HasMedicacao: $("input[type=radio][name=equipante-hasmedicacao]:checked").val(),
                    Medicacao: $(`#equipante-medicacao`).val(),
                    HasAlergia: $("input[type=radio][name=equipante-hasalergia]:checked").val(),
                    Alergia: $(`#equipante-alergia`).val(),
                    Sexo: $("input[type=radio][name=equipante-sexo]:checked").val(),
                    Etiquetas: $('.equipante-etiquetas').val()
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaEquipante();
                $("#modal-equipantes").modal("hide");
            }
        });
    }
}


function DeletePagamento(id) {
    ConfirmMessageDelete().then((result) => {
        if (result) {
            $.ajax({
                url: "/Lancamento/DeletePagamento/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageDelete();
                    CarregarTabelaPagamentos($("#pagamentos-equipanteid").val());
                }
            });
        }
    });
}

function CarregarValorTaxa() {
    optionSelected = $("#pagamentos-meiopagamento option:selected");
    if ((optionSelected.text() == Transferencia) || (optionSelected.text() == Boleto))
        $('.contabancaria').removeClass('d-none');
    else
        $('.contabancaria').addClass('d-none');
    taxa = parseFloat(String(optionSelected.data("taxa")).replace(",", "."));
    valor = parseFloat($("#pagamentos-valor").data("valor"));
    if (taxa > 0)
        $("#pagamentos-valor").val(valor + (valor * taxa / 100));
    else
        $("#pagamentos-valor").val(valor);

}

function PostPagamento() {
    if (ValidateForm(`#form-pagamento`)) {
        $.ajax({
            url: "/Lancamento/PostPagamento/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    EquipanteId: $("#pagamentos-equipanteid").val(),
                    MeioPagamentoId: $("#pagamentos-meiopagamento").val(),
                    ContaBancariaId: $('.contabancaria').hasClass('d-none') ? 0 : $("#pagamentos-contabancaria").val(),
                    Valor: Number($("#pagamentos-valor").val())
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaPagamentos($("#pagamentos-equipanteid").val());
            }
        });
    }
}

$(document).ready(function () {
    CarregarTabelaEquipante();
    getEquipes()
});


$('#has-medicacao').on('ifChecked', function (event) {
    $('.medicacao').removeClass('d-none');
    $("#equipante-medicacao").addClass('required');
});

$('#not-medicacao').on('ifChecked', function (event) {
    $('.medicacao').addClass('d-none');
    $("#equipante-medicacao").removeClass('required');
});

$('#has-alergia').on('ifChecked', function (event) {
    $('.alergia').removeClass('d-none');
    $("#equipante-alergia").addClass('required');
});

$('#not-alergia').on('ifChecked', function (event) {
    $('.alergia').addClass('d-none');
    $("#equipante-alergia").removeClass('required');
});

$('#has-restricaoalimentar').on('ifChecked', function (event) {
    $('.restricaoalimentar').removeClass('d-none');
    $("#equipante-restricaoalimentar").addClass('required');
});

$('#not-restricaoalimentar').on('ifChecked', function (event) {
    $('.restricaoalimentar').addClass('d-none');
    $("#equipante-restricaoalimentar").removeClass('required');
});
