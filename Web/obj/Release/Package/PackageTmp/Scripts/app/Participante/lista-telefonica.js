function CarregarTabelaListaTelefonica() {
    const tableListaTelefonicaConfig = {
        language: languageConfig,
        lengthMenu: [200, 500, 1000],
        colReorder: false,
        serverSide: false,
        deferloading: 0,
        orderCellsTop: true,
        fixedHeader: true,
        filter: true,
        orderMulti: false,
        responsive: true,stateSave: true,
        destroy: true,
        dom: domConfigNoButtons,
        columns: [
            //{
            //    data: "Id", name: "Id", orderable: false, width: "5%",
            //    "render": function (data, type, row) {
            //        return `${GetCheckBox('ToggleContato', data, data, row.PendenciaContato)}`;
            //    }
            //},
            { data: "Sexo", name: "Sexo", visible: false },
            {
                data: "Sexo", orderData: 0, name: "Sexo", className: "text-center", width: "5%",
                "render": function (data, type, row) {
                    if (row.Sexo == "Masculino") {
                        icon = "fa-male";
                        cor = "#0095ff";
                    }
                    else {
                        icon = "fa-female";
                        cor = "#ff00d4";
                    }
                    
                    return `<span style = "font-size:18px;color:${cor};" class="p-l-xs"> <i class="fa ${icon}" aria-hidden="true" title="${row.Sexo}"></i></span>`;
                }
            },
            { data: "Nome", name: "Nome", width: "40%" },
            { data: "Padrinho", name: "Padrinho", width: "20%" },
            {
                data: "Id", name: "Id", autoWidth: true,
                "render": function (data, type, row) {
                    if (row.Status === Confirmado)
                        corStatus = "primary";
                    if (row.Status === Cancelado)
                        corStatus = "danger";
                    if (row.Status === Inscrito)
                        corStatus = "info";
                    return `<span style="font-size:13px" class="text-center label label-${corStatus}">${row.Status}</span>`;
                }
            },
                 {
                     data: "Id", name: "Id", orderable: false, autoWidth: true,
                "render": function (data, type, row) {
                    return `${row.Status != Cancelado ? `<span style="font-size:13px; margin-right:10px" onclick='CancelarInscricao(${JSON.stringify(row)})' class="text-center pointer label label-danger">Cancelar</span>` : ""}
                    <span style="font-size:13px" onclick='Opcoes(${JSON.stringify(row)})' class="text-center label pointer label-primary">Contatos</span>
`;
                }
            }
        ],
        order: [
            [2, "asc"]
        ],
        initComplete: function () {
            $('.i-checks-green').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green'
            });
            $('.i-checks-green').on('ifClicked', function (event) {
                ToggleContato($(event.target).data("id"));
            });
        },
        ajax: {
            url: '/Participante/GetListaTelefonica',
            data: { EventoId: $("#lista-telefonica-eventoid").val() },
            datatype: "json",
            type: "POST"
        }
    };

    $("#table-lista-telefonica").DataTable(tableListaTelefonicaConfig);
}

$(document).ready(function () {
    CarregarTabelaListaTelefonica();
});

var realista = {}

function Opcoes(row) {
    realista = row;
    $('.maetext').text(row.NomeMae)
    $('.paitext').text(row.NomePai)
    $('.convitetext').text(row.NomeConvite)
    $('.pagamento').show()
    if (realista.Status == "Confirmado") {
        $('.pagamento').hide()
    } 
    $("#modal-opcoes").modal();
}


function getParentText(parent, realista) {

    return `Olá *${parent}*, Estamos com a ficha de *${realista}* que fará o nosso Realidade`
}

function ToggleContato(id) {
    $.ajax({
        url: "/Participante/ToggleContato/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                ParticipanteId: id
            })
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
                    CarregarTabelaListaTelefonica();
                }
            });
        }
    });
}

function GetContatosText(contato) {
    return `Olá ${contato}, /n/nAguardamos o pagamento da sua inscrição`
}

function enviar() {
    var text = ''
    switch (tipoGlobal) {
        case 'covid':
            text = `Olá, *${getNome(destinatarioGlobal)}*!

Estou vendo aqui que a inscrição de *${getNome('realista')}* para o *Realidade* já foi paga e sua vaga está garantida, sendo assim, tenho uns avisos:

Tendo em vista a situação do novo Coronavírus, solicitamos a realização e apresentação do resultado do *RT-PCR* ou *Teste Rápido* , bem como a apresentação do *Cartão de Vacinação* do participante com pelo menos *1 dose* da vacina.

- A realização do *RT-PCR* deverá ser feita em *até 48h antes* do dia do evento (a partir do dia 27/10). Já o *Teste Rápido* deverá ser realizado em *até 24h antes* do dia do evento (a partir do dia 28/10). O resultado deve ser *NEGATIVO* e apresentado no dia do evento ou enviado previamente.

- Caso o resultado do participante dê *POSITIVO*, o valor do evento será reembolsado.

${RodapeEvento(realista.Evento)}` 
            break;
        case 'pagamento':
            text = `Olá, *${getNome(destinatarioGlobal)}*!

Estamos com a inscrição de *${getNome('realista')}* para o Realidade, cursilho para jovens da IECB Catedral da Trindade. Porém, para confirmá-la é preciso efetuar o pagamento.

Como ainda estamos em pandemia, precisamos tomar um cuidado extra e por isso teremos *apenas 100 vagas*;

O investimento está custando *R$ 300,00*, e poderá ser feito através do PIX: 100.778.704-09, cartão ou dinheiro. *A secretaria estará na frente da livraria após os cultos das quartas e domingos para recebê-lo(a).* 

No caso do PIX, lembra de enviar o comprovante de pagamento para mim! 

*Corre para garantir tua vaga!*  🥳

${RodapeEvento(realista.Evento)}` 
            break;
        case 'info':
            text = `Olá, *${getNome(destinatarioGlobal)}*!

Seguem alguns outros avisos a respeito do Realidade:

O Realidade começará as 19h da sexta-feira, dia 29 e se encerrará às 18h do dia 31 de outubro. 🥳

A localização do evento será no Colonial Aldeia, Km 11,5, nosso G2.
*R. Sete de Setembro, s\\n - Aldeia dos Camarás, Araça - PE, 54789-525*
https://goo.gl/maps/ZYcmct2f4jrMa1bw9

O *uso da máscara* durante todo o evento será obrigatório, dessa forma, deverá ser providenciado uma quantidade para a troca da máscara durante o dia. 😷

Lembrem-se de levar *roupa de cama e banho, produtos de higiene pessoal* e se fizer uso de alguma *medicação* também.

Nosso plenário é frio então é bom levar um *casaco*.

Os *quartos serão divididos a com no máximo outras 5 pessoas*, tendo todo o distanciamento possivel durante as dormidas; 

Vocês precisam providenciar mensagens, cartinhas, de amigos próximos e da família, *não é álbum*, são apenas mensagens!!

 Estaremos recebendo as mensagens no *sábado do evento das 8h30 até as 12h30* na Catedral da Trindade, Rua Carneiro Vilela 569.

Me confirma se já fizeste cursilho pra eu não te estragar uma surpresa! 🥰

${RodapeEvento(realista.Evento)}` 
            break;
        case 'carta':
            text = `Ficamos felizes em você ter participado de um dos nossos Cursilhos! 

Temos um momento no Realidade que se parece muito com a *manhãnita*, que acontece no sábado à noite, a partir das 17h. 

Esse convite vale para você e outras pessoas próximas do/da realista que *já participaram de algum Cursilho/Realidade*. E é de muita valia, já que demonstramos o importância de viver em corpo de Cristo para eles!

Te esperamos lá! 🥰
*Equipe da Secretaria | ${realista.Evento}*`
            break;
        default:
            break;
    }

        window.open(GetLinkWhatsApp(getTelefone(destinatarioGlobal), text), '_blank').focus();

}




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
        default:
            break;
    }
}

var tipoGlobal = 'pagamento'
var destinatarioGlobal = 'mae'
$(`.${tipoGlobal}`).addClass('moldura-modal')
$(`.${destinatarioGlobal}`).addClass('moldura-modal')

function GetText(row) {
    if (row.Status == Confirmado) {
        return `Olá ${row.Nome}, /n/nSeja Bem Vindo ao *${row.Evento}*. Estamos Felizes com sua participação e temos certeza que serão dias muito especiais em sua vida. Aproveite cada minuto desse Realidade!!
Avisos importantes:
. ${RodapeEvento()}`;

    } else {
        return `Olá ${row.Nome}, /n/nAguardamos o pagamento da sua inscrição no *${row.Evento}*
`;
    }


}