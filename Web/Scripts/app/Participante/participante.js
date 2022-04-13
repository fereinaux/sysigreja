var realista = {}
eventoId = 0
function CarregarTabelaParticipante() {
    if ($("#participante-eventoid").val() != eventoId) {
        $.ajax({
            url: '/Participante/GetPadrinhos',
            data: { eventoId: $("#participante-eventoid").val() },
            datatype: "json",
            type: "GET",
            success: (result) => {
                eventoId = $("#participante-eventoid").val()
                $("#participante-padrinhoid").html(`
<option value=0>Selecione</option>
${result.Padrinhos.map(p => `<option value=${p.Id}>${p.Nome}</option>`)}
`)
            }
        });

        $.ajax({
            url: '/Etiqueta/GetEtiquetas',
            datatype: "json",
            type: "POST",
            success: (result) => {
                eventoId = $("#participante-eventoid").val()
                $("#participante-marcadores").html(`
${result.data.map(p => `<option value=${p.Id}>${p.Nome}</option>`)}
`)
                $("#participante-nao-marcadores").html(`
${result.data.map(p => `<option value=${p.Id}>${p.Nome}</option>`)}
`)
                $('#participante-marcadores').select2();
                $('#participante-nao-marcadores').select2();
            }
        });
    }



    const tableParticipanteConfig = {
        language: languageConfig,
        searchDelay: 750,
        lengthMenu: [10, 30, 50, 100, 200],
        colReorder: false,
        serverSide: true,
        scrollX: true,
        scrollXollapse: true,
        deferloading: 0,
        orderCellsTop: true,
        fixedHeader: true,
        filter: true,
        orderMulti: false,
        responsive: true, stateSave: true,
        destroy: true,
        dom: domConfig,
        buttons: getButtonsConfig(`Participantes ${$("#participante-eventoid option:selected").text()}`),
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
                    return `<span style = "font-size:18px;color:${cor};" class="p-l-xs pointer"> <i class="fa ${icon}" aria-hidden="true" title="${data}"></i></span >`;
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
            { data: "Idade", name: "Idade", width: "5%", },
            { data: "Padrinho", name: "Padrinho", width: "25%" },
            {
                data: "Status", name: "Status", width: "5%", render: function (data, type, row) {
                    if (row.Checkin) {
                        data = "Presente";
                        cor = "warning";
                    }
                    else if (data === Confirmado)
                        cor = "primary";
                    else if (data === Cancelado)
                        cor = "danger";
                    else if (data === Inscrito)
                        cor = "info";
                    else if (data === Espera)
                        cor = "default";
                    return `<span style="font-size:13px" class="text-center label label-${cor}">${data}</span>`;
                }
            },

            {
                data: "Id", name: "Id", orderable: false, width: "25%",
                "render": function (data, type, row) {
                    return row.Status != Cancelado && row.Status != Espera ?

                        `<form enctype="multipart/form-data" id="frm-vacina${data}" method="post" novalidate="novalidate">
${GetButton('Pagamentos', JSON.stringify(row), 'verde', 'far fa-money-bill-alt', 'Pagamentos')}
                        ${!row.HasVacina ? ` <label for="arquivo${data}" class="inputFile">
                                <span style="font-size:18px" class="text-mutted pointer p-l-xs"><i class="fa fa-syringe" aria-hidden="true" title="Vacina"></i></span>
                                <input onchange='PostVacina(${data},${JSON.stringify(row)})' style="display: none;" class="custom-file-input inputFile" id="arquivo${data}" name="arquivo${data}" type="file" value="">
                            </label>`: `<span style="font-size:18px" class="text-success p-l-xs pointer" onclick="toggleVacina(${data})"><i class="fa fa-syringe" aria-hidden="true" title="Vacina"></i></span>`}                        
                        ${!row.HasFoto ? ` <label for="foto${data}" class="inputFile">
                                <span style="font-size:18px" class="text-mutted pointer p-l-xs"><i class="fa fa-camera" aria-hidden="true" title="Foto"></i></span>
                                <input accept="image/*" onchange='Foto(${JSON.stringify(row)})' style="display: none;" class="custom-file-input inputFile" id="foto${data}" name="foto${data}" type="file" value="">
                            </label>`: `<span style="font-size:18px" class="text-success p-l-xs pointer" onclick="toggleFoto(${data})"><i class="fa fa-camera" aria-hidden="true" title="Foto"></i></span>`
                        }
                            ${GetAnexosButton('Anexos', data, row.QtdAnexos)}
                            ${GetIconWhatsApp(row.Fone)}
                            ${GetButton('EditParticipante', data, 'blue', 'fa-edit', 'Editar')}      
                         
                            ${GetButton('Opcoes', JSON.stringify(row), 'cinza', 'fas fa-info-circle', 'Opções')}
                            
                            ${$("#participante-eventoid option:selected").data('status') == 'Encerrado' ? `<a target="_blank" href='${GetLinkWhatsApp(row.Fone, `Olá *${row.Nome}*,

Você gostaria de trabalhar no próximo Realidade de 25 a 27 de Março nossa primeira reunião será na quarta feira (16/02) às 19h30 na *Catedral da Trindade*.

Esse convite é pessoal e *intransferível*.

Carol Bastos.`)}' style="font-size:18px; color:#23c6c8; " class="pointer p-l-xs"><i class="fas fa-envelope" aria-hidden="true" title="${row.Fone}"></i></a>
${GetButton('MakeEquipante', data, 'green', 'fa-broom', 'Equipante')}
` : ''}

                            ${GetButton('CancelarInscricao', JSON.stringify(row), 'red', 'fa-times', 'Cancelar Inscrição')}
                    </form>`
                        : ''
                }
            }
        ],
        order: [
            [2, "asc"]
        ],
        ajax: {
            url: '/Participante/GetParticipantesDatatable',
            data: { EventoId: $("#participante-eventoid").val(), PadrinhoId: $("#participante-padrinhoid").val(), Status: $("#participante-status").val() != 999 ? $("#participante-status").val() : null, Etiquetas: $("#participante-marcadores").val(), NaoEtiquetas: $("#participante-nao-marcadores").val() },
            datatype: "json",
            type: "POST"
        }
    };

    tableParticipanteConfig.buttons.forEach(function (o) {
        if (o.extend === "excel") {
            o.action = function (e, dt, button, config) {
                $.post(
                    tableParticipanteConfig.ajax.url + "?extract=excel",
                    tableParticipanteConfig.ajax.data,
                    function (o) {
                        window.location = `/Participante/DownloadTempFile?fileName=Participantes ${$("#participante-eventoid option:selected").text()}.xlsx&g=` + o;
                    }
                );
            };
        }
    });

    $("#table-participante").DataTable(tableParticipanteConfig);
}

function ConfirmFoto() {

    $("#main-cropper")
        .croppie("result", {
            type: "canvas",
            size: { height: 750, width: 500 }
        })
        .then(function (resp) {
            var dataToPost = new FormData();
            dataToPost.set('ParticipanteId', realista.Id)
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
                        CarregarTabelaParticipante()

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
        convertSize: 1000000,
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
            url: '/Arquivo/GetArquivosParticipante',
            data: { participanteid: id ? id : $("#ParticipanteIdModal").val() },
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


function PostVacina(id, realista) {
    var dataToPost = new FormData($(`#frm-vacina${id}`)[0]);
    dataToPost.set('ParticipanteId', id)
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
                $.ajax(
                    {
                        datatype: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        url: "Participante/ToggleVacina",
                        data: JSON.stringify(
                            {
                                Id: id
                            }),

                        success: function () {
                            CarregarTabelaParticipante()

                        }
                    });

            }
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
                    url: "Arquivo/DeleteFotoParticipante",
                    data: JSON.stringify(
                        {
                            Id: id
                        }),

                    success: function () {
                        CarregarTabelaParticipante()

                    }
                });
        }
    }
    )
}


function MakeEquipante(id) {
    ConfirmMessage("Transformar em Equipante?").then((result) => {
        if (result) {
            $.ajax(
                {
                    datatype: "json",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    url: "Participante/MakeEquipante",
                    data: JSON.stringify(
                        {
                            Id: id
                        }),

                    success: function () {


                    }
                });
        }
    })
}


function toggleVacina(id) {
    ConfirmMessage("Essa ação removerá a vacina, deseja continuar?").then((result) => {
        $.ajax(
            {
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                url: "Participante/ToggleVacina",
                data: JSON.stringify(
                    {
                        Id: id
                    }),

                success: function () {
                    CarregarTabelaParticipante()

                }
            });
    })
}

function PostArquivo() {

    var dataToPost = new FormData($('#frm-upload-arquivo-modal')[0]);
    var filename = dataToPost.get('arquivo-modal').name
    var arquivo = new File([dataToPost.get('arquivo-modal')], 'Pagamento ' + realista.Nome + filename.substr(filename.indexOf('.')));
    dataToPost.set('Arquivo', arquivo)
    dataToPost.set('ParticipanteId', dataToPost.get('ParticipanteIdModal'))
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

function Anexos(id) {
    $("#ParticipanteIdModal").val(id);
    $("#LancamentoIdModal").val('');
    GetAnexos(id);
    $("#modal-anexos").modal();
}

function AnexosLancamento(row) {
    $("#LancamentoIdModal").val(row.Id);
    $("#ParticipanteIdModal").val(row.ParticipanteId);
    GetAnexosLancamento(row.Id)
    $("#modal-pagamentos").modal('hide');
    $("#modal-anexos").modal();
}


$("#arquivo-modal").change(function () {
    PostArquivo();
});

$("#modal-anexos").on('hidden.bs.modal', function () {
    CarregarTabelaParticipante()
});

var tipoGlobal = 'pagamento'
$(`.${tipoGlobal}`).addClass('moldura-modal')
var destinatarioGlobal = 'mae'
$(`.${destinatarioGlobal}`).addClass('moldura-modal')

function enviar() {
    var windowReference = window.open('_blank');
    $.ajax({
        url: "/Mensagem/GetMensagem/",
        data: { Id: $("#msg-list").val() },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var text = data.Mensagem.Conteudo.replaceAll('${Nome Contato}', getNome(destinatarioGlobal)).replaceAll('${Nome Participante}', getNome('realista'));
            windowReference.location = GetLinkWhatsApp(getTelefone(destinatarioGlobal), text)

        }
    });


}

//function enviar() {
//    var text = ''
//    switch (tipoGlobal) {
//        case 'covid':
//            text = `Olá, *${getNome(destinatarioGlobal)}*!

//Estou vendo aqui que a inscrição de *${getNome('realista')}* para o *Realidade* já foi paga e sua vaga está garantida, sendo assim, tenho uns avisos:

//Tendo em vista a situação do novo Coronavírus e os decretos do governo do estado, a realização e apresentação do resultado do RT-PCR ou Teste Rápido é OPCIONAL , mas ainda fica obrigatório a apresentação do Cartão de Vacinação do participante com pelo menos 2 doses da vacina.

//- Caso vá realizar o exeme, a realização do RT-PCR deverá ser feita em até 48h antes do dia do evento (a partir do dia 23/03). Já o Teste Rápido deverá ser realizado em até 24h antes do dia do evento (a partir do dia 24/03). O resultado deve ser NEGATIVO e apresentado no dia do evento ou enviado previamente.

//- Caso o resultado do participante dê *POSITIVO*, o valor do evento será reembolsado.

//${RodapeEvento($("#participante-eventoid option:selected").text())}`
//            break;
//        case 'pagamento':
//            text = `Olá, *${getNome(destinatarioGlobal)}*!

//Estamos com a inscrição de *${getNome('realista')}* para o Realidade, cursilho para jovens da IECB Catedral da Trindade. Porém, para confirmá-la é preciso efetuar o pagamento.

//Como ainda estamos em pandemia, precisamos tomar um cuidado extra e por isso teremos *apenas 100 vagas*;

//O investimento está custando *R$ 300,00*, e poderá ser feito através do PIX (celular): 81996557814, cartão ou dinheiro. *A secretaria estará na frente da livraria após os cultos das quartas e domingos para recebê-lo(a).*

//No caso do PIX, lembra de enviar o comprovante de pagamento para mim! 

//*Corre para garantir tua vaga!*  🥳

//${RodapeEvento($("#participante-eventoid option:selected").text())}`
//            break;
//        case 'info':
//            text = `Olá, *${getNome(destinatarioGlobal)}*!

//Seguem AVISOS IMPORTANTES sobre o Realidade:

//PAIS, LEIAM COM ATENÇÃO

//• O Realidade começará as 19h da sexta-feira dia 25 e se encerrará às 18h do dia 27 de março. 🥳

//• Onde será:📍
//Colonial Aldeia, Km 11,5
//R. Sete de Setembro - Aldeia dos Camarás
//https://goo.gl/maps/ZYcmct2f4jrMa1bw9

//• Na mala, coloque máscaras suficientes para troca durante o dia, pois o uso será obrigatório durante todo o evento. 😷

//• Levar roupa de cama e banho, produtos de higiene pessoal 🧼 e medicações, caso o adolescente faça uso.💊

//• O nosso plenário é bem frio então sugerimos levar casaco. 🧥

//• Os quartos serão divididos com outras 5 pessoas, respeitando o distanciamento durante as dormidas.

//⚠️⚠️⚠️
//* Vocês precisarão providenciar mensagens, escritas ou impressas, de amigos próximos e da família*

//Estaremos recebendo as mensagens no sábado do evento das 8h30 até as 12h30 na Catedral da Trindade, Rua Carneiro Vilela 569.

//VOCÊ JÁ FEZ CURSILHO OU REALIDADE?
//Se sim, me confirme por mensagem!!

//${RodapeEvento($("#participante-eventoid option:selected").text())}`
//            break;
//        case 'carta':
//            text = `Ficamos felizes por você ter participado de um dos nossos Cursilhos e temos um convite a lhe fazer! 😍

//Temos um momento no Realidade que se parece muito com a Mañanitas, é a nossa Noitita que acontece no sábado à noite, a partir das 17h.

//Então você e outras pessoas próximas do/da realista que já participaram de algum Cursilho/Realidade são muito bem-vindos.

//É um momento muito especial onde demonstramos a importância de ser parte do corpo de Cristo para eles!

//Lembramos que esse não é um momento entre família e filhos e sim um momento de CURSILHISTAS com os REALISTAS

//Para participar da nossa Noitita, pedimos que siga algumas orientações:

//- Use máscara  durante todo o momento e respeite o distanciamento orientado pela nossa equipe.

//- Caso você esteja com sintomas de gripe, pedimos que não compareça, para sua segurança e a nossa.
//(teremos mais de 200 adolescente entre participantes e equipe de trabalho na bolha do Realidade.)

//Pode ficar tranquilo  que seu realista vai receber todo o amor e cuidado que o momento sugere.


//Te esperamos lá! 🥰
//*Equipe da Secretaria | ${$("#participante-eventoid option:selected").text()}*`
//            break;
//        case 'foto':
//            text = `Oi, *${getNome('realista')}*! Como estão as expectativas para o Realidade? Espero que boas! 🥳

//Somos da secretaria do Realidade e devido a pandemia e ao uso obrigatório da máscara no evento, vamos precisar de uma foto sua para o seu crachá de identificação.

//Orientações:

//1. Foto estilo retrato
//2. Formato vertical
//3. Sem óculos de sol ou máscara

//Escolhe e me manda o mais rápido possível.

//${RodapeEvento($("#participante-eventoid option:selected").text())}`
//            break;
//        default:
//            break;
//    }

//    window.open(GetLinkWhatsApp(getTelefone(tipoGlobal == 'foto' ? 'realista' : destinatarioGlobal), text), '_blank').focus();

//}




function select1(tipo) {
    $('.covid').removeClass('moldura-modal')
    $('.pagamento').removeClass('moldura-modal')
    $('.carta').removeClass('moldura-modal')
    $('.info').removeClass('moldura-modal')
    tipoGlobal = tipo
    $(`.${tipo}`).addClass('moldura-modal')
}

function select2(destinatario) {
    $('.realista').removeClass('moldura-modal')
    $('.mae').removeClass('moldura-modal')
    $('.pai').removeClass('moldura-modal')
    $('.convite').removeClass('moldura-modal')
    $('.contato').removeClass('moldura-modal')
    $('.realista').removeClass('moldura-modal')
    destinatarioGlobal = destinatario
    $(`.${destinatario}`).addClass('moldura-modal')
    $('.btn-ligar').attr("href", `tel:${getTelefone(destinatario)}`)
}


function getNome(destinatario) {
    switch (destinatario) {
        case 'realista':
            return realista.Nome.trim()
            break;
        case 'mae':
            return realista.NomeMae.trim()
            break;
        case 'pai':
            return realista.NomePai.trim()
            break;
        case 'convite':
            return realista.NomeConvite.trim()
            break;
        case 'contato':
            return realista.NomeContato.trim()
            break;
        default:
            break;
    }
}

function getTelefone(destinatario) {
    switch (destinatario) {
        case 'realista':
            return realista.Fone
            break;
        case 'mae':
            return realista.FoneMae
            break;
        case 'pai':
            return realista.FonePai
            break;
        case 'convite':
            return realista.FoneConvite
            break;
        case 'contato':
            return realista.FoneContato
            break;
        default:
            break;
    }
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
                            ${GetButton('DeletePagamento', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Lancamento/GetPagamentos',
            data: { ParticipanteId: id },
            datatype: "json",
            type: "POST"
        }
    };
    $("#table-pagamentos").DataTable(tablePagamentosConfig);
}

$(document).ready(function () {
    HideMenu()
    CarregarTabelaParticipante();
});


function Pagamentos(row) {
    realista = row;
    $("#pagamentos-whatsapp").val(row.Fone);
    $("#pagamentos-valor").val($("#pagamentos-valor").data("valor"));
    $("#pagamentos-participanteid").val(row.Id);
    $("#pagamentos-meiopagamento").val($("#pagamentos-meiopagamento option:first").val());
    CarregarTabelaPagamentos(row.Id);
    $("#modal-pagamentos").modal();
}

$("#modal-pagamentos").on('hidden.bs.modal', function () {
    if (!$('#LancamentoIdModal').val()) {
        CarregarTabelaParticipante();
    }
})

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
                    CarregarTabelaPagamentos($("#pagamentos-participanteid").val());
                }
            });
        }
    });
}

function ToggleSexo(id) {
    ConfirmMessage("Confirma a mudança de gênero?").then((result) => {
        if (result) {
            $.ajax({
                url: "/Participante/ToggleSexo/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: id
                    }),
                success: function () {
                    SuccessMesageOperation();
                    CarregarTabelaParticipante();
                }
            });
        }
    });
}

function CancelarInscricao(row) {
    ConfirmMessageCancelar(row.Nome).then((result) => {
        if (result) {
            $.ajax({
                url: "/Participante/CancelarInscricao/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: row.Id
                    }),
                success: function () {
                    SuccessMesageOperation();
                    CarregarTabelaParticipante();
                }
            });
        }
    });
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
                    EventoId: $("#participante-eventoid").val(),
                    ParticipanteId: $("#pagamentos-participanteid").val(),
                    MeioPagamentoId: $("#pagamentos-meiopagamento").val(),
                    ContaBancariaId: $('.contabancaria').hasClass('d-none') ? 0 : $("#pagamentos-contabancaria").val(),
                    Valor: Number($("#pagamentos-valor").val())
                }),
            success: function () {
                CarregarTabelaPagamentos($("#pagamentos-participanteid").val());
                SuccessMesageOperation();
            }
        });
    }
}


function Opcoes(row) {
    realista = row;
    $('.participante-etiquetas').select2({ dropdownParent: $("#form-opcoes") });
    $.ajax({
        url: "/Participante/GetParticipante/",
        data: { Id: row.Id },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            realista = data.Participante
            select1(realista.Status == 'Inscrito' ? 'pagamento' : 'covid')
            $('.maetext').text(realista.NomeMae)
            $('.realista-nome').text(realista.Nome)
            $('.paitext').text(realista.NomePai)
            $('.convitetext').text(realista.NomeConvite)
            $('.contatotext').text(realista.NomeContato)

            $('.pagamento').show()
            $('#participante-obs').val(realista.Observacao)
            $(`#participante-msgcovid`).iCheck(realista.MsgVacina ? 'check' : 'uncheck');
            $(`#participante-msgpagamento`).iCheck(realista.MsgPagamento ? 'check' : 'uncheck');
            $(`#participante-msgnoitita`).iCheck(realista.MsgNoitita ? 'check' : 'uncheck');
            $(`#participante-msggeral`).iCheck(realista.MsgGeral ? 'check' : 'uncheck');
            $(`#participante-msgfoto`).iCheck(realista.MsgFoto ? 'check' : 'uncheck');

            $.ajax({
                url: "/Mensagem/GetMensagens/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                success: function (dataMsg) {
                    $("#msg-list").html(`
${dataMsg.data.map(p => `<option value=${p.Id}>${p.Titulo}</option>`)}
`)

                }
            })

            $('#participante-etiquetas').html(`${data.Etiquetas.map(etiqueta => `<option data-cor="${etiqueta.Cor}" value=${etiqueta.Id}>${etiqueta.Nome}</option>`)
                }`)
            $('#participante-etiquetas').val(data.Participante.Etiquetas.map(etiqueta => etiqueta.Id))
            if (realista.Status == "Confirmado") {
                $('.pagamento').hide()
            }
            $("#modal-opcoes").modal();
        }
    });


}

$("#modal-opcoes").on('hidden.bs.modal', function () {
    $.ajax({
        url: "/Participante/PostInfo/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                Id: realista.Id,
                Observacao: $('#participante-obs').val(),
                MsgVacina: $(`#participante-msgcovid`).prop("checked"),
                MsgPagamento: $(`#participante-msgpagamento`).prop("checked"),
                MsgNoitita: $(`#participante-msgnoitita`).prop("checked"),
                MsgGeral: $(`#participante-msggeral`).prop("checked"),
                MsgFoto: $(`#participante-msgfoto`).prop("checked"),
                Etiquetas: $('.participante-etiquetas').val()
            }),
        success: function () {
            CarregarTabelaParticipante()
        }
    });
});

function GetParticipanteContato(id) {
    $.ajax({
        url: "/Participante/GetParticipante/",
        data: { Id: id },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#participantecontato-id").val(data.Participante.Id);
            $(`#participante-nome`).val(data.Participante.Nome);
            $(`#participante-apelido`).val(data.Participante.Apelido);
            $("#participante-data-nascimento").val(moment(data.Participante.DataNascimento).format('DD/MM/YYYY'));
            $(`#participante-email`).val(data.Participante.Email);
            $(`#participante-fone`).val(data.Participante.Fone);
            $(`#participante-nomepai`).val(data.Participante.NomePai);
            $(`#participante-fonepai`).val(data.Participante.FonePai);
            $(`#participante-nomemae`).val(data.Participante.NomeMae);
            $(`#participante-fonemae`).val(data.Participante.FoneMae);
            $(`#participante-nomeconvite`).val(data.Participante.NomeConvite);
            $(`#participante-foneconvite`).val(data.Participante.FoneConvite);
            $(`#participante-nomecontato`).val(data.Participante.NomeContato);
            $(`#participante-fonecontato`).val(data.Participante.FoneContato);
            $(`#participante-restricaoalimentar`).val(data.Participante.RestricaoAlimentar);
            $(`#participante-medicacao`).val(data.Participante.Medicacao);
            $(`#participante-alergia`).val(data.Participante.Alergia);
            $(`input[type=radio][name=participante-sexo][value=${data.Participante.Sexo}]`).iCheck('check');
            $(`input[type=radio][name=participante-hasalergia][value=${data.Participante.HasAlergia}]`).iCheck('check');
            $(`input[type=radio][name=participante-hasmedicacao][value=${data.Participante.HasMedicacao}]`).iCheck('check');
            $(`input[type=radio][name=participante-hasrestricaoalimentar][value=${data.Participante.HasRestricaoAlimentar}]`).iCheck('check');

            $("#participante-numeracao").val(data.Participante.Numeracao);
        }
    });
}

function GetParticipante(id) {
    if (id > 0) {
        $.ajax({
            url: "/Participante/GetParticipante/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#participante-id").val(data.Participante.Id);
                $(`#participante-nome`).val(data.Participante.Nome);
                $(`#participante-apelido`).val(data.Participante.Apelido);
                $("#participante-data-nascimento").val(moment(data.Participante.DataNascimento).format('DD/MM/YYYY'));
                $(`#participante-email`).val(data.Participante.Email);
                $(`#participante-fone`).val(data.Participante.Fone);
                $(`#participante-nomepai`).val(data.Participante.NomePai);
                $(`#participante-fonepai`).val(data.Participante.FonePai);
                $(`#participante-nomemae`).val(data.Participante.NomeMae);
                $(`#participante-fonemae`).val(data.Participante.FoneMae);
                $(`#participante-nomeconvite`).val(data.Participante.NomeConvite);
                $(`#participante-foneconvite`).val(data.Participante.FoneConvite);
                $(`#participante-nomecontato`).val(data.Participante.NomeContato);
                $(`#participante-fonecontato`).val(data.Participante.FoneContato);
                $(`#participante-restricaoalimentar`).val(data.Participante.RestricaoAlimentar);
                $(`#participante-medicacao`).val(data.Participante.Medicacao);
                $(`#participante-alergia`).val(data.Participante.Alergia);
                $(`input[type=radio][name=participante-sexo][value=${data.Participante.Sexo}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasalergia][value=${data.Participante.HasAlergia}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasmedicacao][value=${data.Participante.HasMedicacao}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasrestricaoalimentar][value=${data.Participante.HasRestricaoAlimentar}]`).iCheck('check');

                $("#participante-numeracao").val(data.Participante.Numeracao);
            }
        });
    }
    else {
        $("#participante-id").val(0);
        $(`#participante-nome`).val("");
        $(`#participante-apelido`).val("");
        $("#participante-data-nascimento").val("");
        $(`#participante-email`).val("");
        $(`#participante-fone`).val("");
        $(`#participante-restricaoalimentar`).val("");
        $(`#participante-medicacao`).val("");
        $(`#participante-alergia`).val("");
        $(`#participante-nomepai`).val("");
        $(`#participante-fonepai`).val("");
        $(`#participante-nomemae`).val("");
        $(`#participante-fonemae`).val("");
        $(`#participante-nomeconvite`).val("");
        $(`#participante-foneconvite`).val("");
        $(`#participante-nomecontato`).val("");
        $(`#participante-fonecontato`).val("");
        $(`input[type=radio][name=participante-sexo][value=1]`).iCheck('check');
        $(`input[type=radio][name=participante-hasalergia][value=false]`).iCheck('check');
        $(`input[type=radio][name=participante-hasmedicacao][value=false]`).iCheck('check');
        $(`input[type=radio][name=participante-hasrestricaoalimentar][value=false]`).iCheck('check');
    }
}

function EditParticipante(id) {
    GetParticipante(id);
    $("#modal-participantes").modal();
}


function PostParticipante() {
    if (ValidateForm(`#form-participante`)) {
        $.ajax({
            url: "/Inscricoes/PostInscricao/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Id: $("#participante-id").val(),
                    EventoId: $("#participante-eventoid").val(),
                    Nome: $(`#participante-nome`).val(),
                    Apelido: $(`#participante-apelido`).val(),
                    DataNascimento: moment($("#participante-data-nascimento").val(), 'DD/MM/YYYY', 'pt-br').toJSON(),
                    Email: $(`#participante-email`).val(),
                    Fone: $(`#participante-fone`).val(),
                    NomePai: $(`#participante-nomepai`).val(),
                    FonePai: $(`#participante-fonepai`).val(),
                    NomeMae: $(`#participante-nomemae`).val(),
                    FoneMae: $(`#participante-fonemae`).val(),
                    NomeConvite: $(`#participante-nomeconvite`).val(),
                    FoneConvite: $(`#participante-foneconvite`).val(),
                    NomeContato: $(`#participante-nomecontato`).val(),
                    FoneContato: $(`#participante-fonecontato`).val(),
                    HasRestricaoAlimentar: $("input[type=radio][name=participante-hasrestricaoalimentar]:checked").val(),
                    RestricaoAlimentar: $(`#participante-restricaoalimentar`).val(),
                    HasMedicacao: $("input[type=radio][name=participante-hasmedicacao]:checked").val(),
                    Medicacao: $(`#participante-medicacao`).val(),
                    HasAlergia: $("input[type=radio][name=participante-hasalergia]:checked").val(),
                    Alergia: $(`#participante-alergia`).val(),
                    Sexo: $("input[type=radio][name=participante-sexo]:checked").val()
                }),
            success: function () {
                SuccessMesageOperation();
                CarregarTabelaParticipante();
                $("#modal-participantes").modal("hide");
            }
        });
    }
}


$('#has-medicacao').on('ifChecked', function (event) {
    $('.medicacao').removeClass('d-none');
    $("#participante-medicacao").addClass('required');
});

$('#not-medicacao').on('ifChecked', function (event) {
    $('.medicacao').addClass('d-none');
    $("#participante-medicacao").removeClass('required');
});


$('#has-alergia').on('ifChecked', function (event) {
    $('.alergia').removeClass('d-none');
    $("#participante-alergia").addClass('required');
});

$('#not-alergia').on('ifChecked', function (event) {
    $('.alergia').addClass('d-none');
    $("#participante-alergia").removeClass('required');
});

$('#has-restricaoalimentar').on('ifChecked', function (event) {
    $('.restricaoalimentar').removeClass('d-none');
    $("#participante-restricaoalimentar").addClass('required');
});

$('#not-restricaoalimentar').on('ifChecked', function (event) {
    $('.restricaoalimentar').addClass('d-none');
    $("#participante-restricaoalimentar").removeClass('required');
});
