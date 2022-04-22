HideMenu();

$(document).ready(() => {
    Refresh();
});

function GetParticipantes(id) {
    $("#participantes").empty();
    $('#participantes').append($('<option>Pesquisar</option>'));
    $.ajax({
        url: '/Participante/GetParticipantesSelect',
        data: { EventoId: $("#eventoid").val() },
        datatype: "json",
        type: "POST",
        success: (result) => {
            result.data.forEach(function (participante, index, array) {
                if (participante.Status != Cancelado)
                    $('#participantes').append($(`<option value="${participante.Id}">${participante.Nome}</option>`));
            });
            $("#participantes").val(id > 0 ? id : "Pesquisar").trigger("chosen:updated");
            GetParticipante();
        }
    });
}

function GetEquipantes(id) {
    $("#equipantes").empty();
    $('#equipantes').append($('<option>Pesquisar</option>'));
    $.ajax({
        url: '/Equipe/GetEquipantesByEventoSelect',
        data: { EventoId: $("#eventoid").val() },
        datatype: "json",
        type: "GET",
        success: (result) => {
            result.data.forEach(function (equipante, index, array) {
                $('#equipantes').append($(`<option value="${equipante.Id}">${equipante.Nome}</option>`));
            });
            $("#equipantes").val(id > 0 ? id : "Pesquisar").trigger("chosen:updated");
            GetEquipante();
        }
    });
}

function Cracha() {
    $("#modal-etiquetas").modal();
}


function PostParticipante() {
    if (ValidateForm(`#form-participante`)) {
        if ($("#participante-id").val() > 0) {
            $.ajax({
                url: "/Inscricoes/Checkin/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: $("#participante-id").val(),
                        Checkin: $("#participante-checkin").val(),
                        CancelarCheckin: false,
                        Nome: $(`#participante-nome`).val(),
                        Apelido: $(`#participante-apelido`).val(),
                        DataNascimento: moment($("#participante-data-nascimento").val(), 'DD/MM/YYYY', 'pt-br').toJSON(),
                        Email: $(`#participante-email`).val(),
                        Fone: $(`#participante-fone`).val(),
                        CEP: $(`#participante-cep`).val(),
                        Logradouro: $(`#participante-logradouro`).val(),
                        Bairro: $(`#participante-bairro`).val(),
                        Cidade: $(`#participante-cidade`).val(),
                        Estado: $(`#participante-estado`).val(),
                        Numero: $(`#participante-numero`).val(),
                        Complemento: $(`#participante-complemento`).val(),
                        Referencia: $(`#participante-referencia`).val(),
                        Latitude: $(`#participante-latitude`).val(),
                        Longitude: $(`#participante-longitude`).val(),
                        HasRestricaoAlimentar: $("input[type=radio][name=participante-hasrestricaoalimentar]:checked").val(),
                        RestricaoAlimentar: $(`#participante-restricaoalimentar`).val(),
                        HasMedicacao: $("input[type=radio][name=participante-hasmedicacao]:checked").val(),
                        Medicacao: $(`#participante-medicacao`).val(),
                        HasAlergia: $("input[type=radio][name=participante-hasalergia]:checked").val(),
                        Alergia: $(`#participante-alergia`).val(),
                        Sexo: $("input[type=radio][name=participante-sexo]:checked").val(),
                        NomePai: $(`#participante-nome-pai`).val(),
                        FonePai: $(`#participante-fone-pai`).val(),
                        NomeMae: $(`#participante-nome-mae`).val(),
                        FoneMae: $(`#participante-fone-mae`).val(),
                        NomeConvite: $(`#participante-nome-convite`).val(),
                        FoneConvite: $(`#participante-fone-convite`).val(),
                        NomeContato: $(`#participante-nome-contato`).val(),
                        FoneContato: $(`#participante-fone-contato`).val(),
                    }),
                success: function (data) {
                    $.ajax({
                        url: "/Participante/PostInfo/",
                        datatype: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(
                            {
                                Id: $("#participante-id").val(),
                                Observacao: $('#participante-obs').val(),
                                MsgVacina: $(`#participante-msgcovid`).prop("checked"),
                                MsgPagamento: $(`#participante-msgpagamento`).prop("checked"),
                                MsgNoitita: $(`#participante-msgnoitita`).prop("checked"),
                                MsgGeral: $(`#participante-msggeral`).prop("checked"),
                                MsgFoto: $(`#participante-msgfoto`).prop("checked"),
                                Etiquetas: $('.participante-etiquetas').val()
                            }),
                        success: function () {

                            SuccessMesageOperation();
                            GetParticipantes(data.Id);
                            GetTotaisCheckin();
                        }
                    });
                }
            });

        } else {
            $.ajax({
                url: "/Equipante/PostEquipante/",
                datatype: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        Id: $("#equipante-id").val(),
                        Nome: $(`#participante-nome`).val(),
                        Apelido: $(`#participante-apelido`).val(),
                        DataNascimento: moment($("#participante-data-nascimento").val(), 'DD/MM/YYYY', 'pt-br').toJSON(),
                        Email: $(`#participante-email`).val(),
                        Fone: $(`#participante-fone`).val(),
                        HasRestricaoAlimentar: $("input[type=radio][name=participante-hasrestricaoalimentar]:checked").val(),
                        RestricaoAlimentar: $(`#participante-restricaoalimentar`).val(),
                        HasMedicacao: $("input[type=radio][name=participante-hasmedicacao]:checked").val(),
                        Medicacao: $(`#participante-medicacao`).val(),
                        HasAlergia: $("input[type=radio][name=participante-hasalergia]:checked").val(),
                        Alergia: $(`#participante-alergia`).val(),
                        Sexo: $("input[type=radio][name=participante-sexo]:checked").val(),
                        Etiquetas: $('.participante-etiquetas').val()
                    }),
                success: function () {
                    SuccessMesageOperation();
                    GetEquipantes($("#equipante-id").val())

                }
            });
        }

    }
}

function NewParticipante() {
    $("#participantes").val("Pesquisar").trigger("chosen:updated");
    GetParticipante();
}


function GetParticipante() {
    AplicarCssPadrao($('input'));
    id = $("#participantes").val() == "Pesquisar" ? 0 : $("#participantes").val();
    if (id > 0) {
        $.ajax({
            url: "/Participante/GetParticipante/",
            data: { Id: id },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('.div-map').css('display', 'none')
                $("#equipantes").val("Pesquisar").trigger("chosen:updated");
                $('#form-participante').removeClass('d-none');
                $('#form-info').removeClass('d-none');
                $("#equipante-id").val(0);
                $("#participante-id").val(data.Participante.Id);
                $("#participante-checkin").val(data.Participante.Checkin);
                $(`#participante-nome`).val(data.Participante.Nome);
                $(`#participante-apelido`).val(data.Participante.Apelido);
                $("#participante-data-nascimento").val(moment(data.Participante.DataNascimento).format('DD/MM/YYYY'));
                $(`#participante-email`).val(data.Participante.Email);
                $(`#participante-fone`).val(data.Participante.Fone);
                $(`#participante-nome-pai`).val(data.Participante.NomePai);
                $(`#participante-fone-pai`).val(data.Participante.FonePai);
                $(`#participante-nome-mae`).val(data.Participante.NomeMae);
                $(`#participante-fone-mae`).val(data.Participante.FoneMae);
                $(`#participante-cep`).val(data.Participante.CEP);
                $(`#participante-logradouro`).val(data.Participante.Logradouro);
                $(`#participante-bairro`).val(data.Participante.Bairro);
                $(`#participante-cidade`).val(data.Participante.Cidade);
                $(`#participante-estado`).val(data.Participante.Estado);
                $(`#participante-numero`).val(data.Participante.Numero);
                $(`#participante-complemento`).val(data.Participante.Complemento);
                $(`#participante-referencia`).val(data.Participante.Referencia);

                $(`#participante-latitude`).val((data.Participante.Latitude || '').replaceAll(',', '.'));
                $(`#participante-longitude`).val((data.Participante.Longitude || '').replaceAll(',', '.'));
                montarMapa()
                $(`#participante-nome-convite`).val(data.Participante.NomeConvite);
                $(`#participante-fone-convite`).val(data.Participante.FoneConvite);
                $(`#participante-nome-contato`).val(data.Participante.NomeContato);
                $(`#participante-fone-contato`).val(data.Participante.FoneContato);
                $(`#participante-restricaoalimentar`).val(data.Participante.RestricaoAlimentar);
                $(`#participante-medicacao`).val(data.Participante.Medicacao);
                $(`#participante-alergia`).val(data.Participante.Alergia);
                $(`input[type=radio][name=participante-sexo][value=${data.Participante.Sexo}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasalergia][value=${data.Participante.HasAlergia}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasmedicacao][value=${data.Participante.HasMedicacao}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasrestricaoalimentar][value=${data.Participante.HasRestricaoAlimentar}]`).iCheck('check');
                $('.dados-participante-contato').removeClass('d-none');
                $('.dados-participante-contato input[id*="nome"]').addClass('required');
                $('.dados-participante-contato input[id*="fone"]').addClass('fone');
                $('.dados-equipante').addClass('d-none');
                $('.padrinho').text(data.Participante.Padrinho)
                realista = data.Participante
                $('.maetext').text(realista.NomeMae)
                $('.realista-nome').text(realista.Nome)
                $('.paitext').text(realista.NomePai)
                $('.convitetext').text(realista.NomeConvite)
                $('.contatotext').text(realista.NomeContato)
                $('#marcadores').html(realista.Etiquetas.map(etiqueta => `<span  class="badge m-r-xs" style="background-color:${etiqueta.Cor};color:#fff">${etiqueta.Nome}</span>`).join().replace(/,/g, ''))
                $('#participante-etiquetas').html(`${data.Etiquetas.map(etiqueta => `<option data-cor="${etiqueta.Cor}" value=${etiqueta.Id}>${etiqueta.Nome}</option>`)
                    }`)
                $('#participante-etiquetas').val(data.Participante.Etiquetas.map(etiqueta => etiqueta.Id))
                $('.participante-etiquetas').select2()
                $('.pagamento').show()
                $('#participante-obs').val(realista.Observacao)
                $(`#participante-msgcovid`).iCheck(realista.MsgVacina ? 'check' : 'uncheck');
                $(`#participante-msgpagamento`).iCheck(realista.MsgPagamento ? 'check' : 'uncheck');
                $(`#participante-msgnoitita`).iCheck(realista.MsgNoitita ? 'check' : 'uncheck');
                $(`#participante-msggeral`).iCheck(realista.MsgGeral ? 'check' : 'uncheck');
                $(`#participante-msgfoto`).iCheck(realista.MsgFoto ? 'check' : 'uncheck');
                if (data.Participante.Foto) {

                    $('#foto').attr("src", 'data:image/jpeg;base64,' + data.Participante.Foto)
                    $('#div-foto').css('display', 'flex')
                } else {
                    $('#div-foto').css('display', 'none')
                }
                Pagamentos($("#participantes").val());
                GetAnexos();

                $('#vacina').html(`<form enctype="multipart/form-data" id="frm-vacina" method="post" novalidate="novalidate">
                        ${!data.Participante.HasVacina ? ` <label for="input-vacina" class="inputFile" style="margin-bottom:0px">
                            <span style="" class="text-mutted pointer p-l-xs"><i class="fa fa-syringe fa-3x" aria-hidden="true" title="Vacina"></i></span>
                            <input onchange='PostVacina(${data.Participante.Id},${JSON.stringify(data.Participante)})' style="display: none;" class="custom-file-input inputFile" id="input-vacina" name="input-vacina" type="file" value="">
                        </label>`: `<span class="text-success p-l-xs pointer" onclick="toggleVacina(${data.Participante.Id})"><i class="fa fa-syringe fa-3x" aria-hidden="true" title="Vacina"></i></span>`}
                    </form>`)

                $('#teste').html(`<form enctype="multipart/form-data" id="frm-teste" method="post" novalidate="novalidate">
                        ${!data.Participante.HasTeste ? ` <label for="input-teste" class="inputFile" style="margin-bottom:0px">
                            <span style="" class="text-mutted pointer p-l-xs"><i class="fa fa-microscope fa-3x" aria-hidden="true" title="Vacina"></i></span>
                            <input onchange='PostTeste(${data.Participante.Id},${JSON.stringify(data.Participante)})' style="display: none;" class="custom-file-input inputFile" id="input-teste" name="input-teste" type="file" value="">
                        </label>`: `<span class="text-success p-l-xs pointer" onclick="toggleTeste(${data.Participante.Id})"><i class="fa fa-microscope fa-3x" aria-hidden="true" title="Teste"></i></span>`}
                    </form>`)

                $('.status').text(data.Participante.Status);
                $('.circulo').text(data.DadosAdicionais.Circulo || "Sem Círculo");
                $('.quarto').text(data.DadosAdicionais.Quarto || "Sem Quarto");

                var quartoAtual = data.DadosAdicionais.QuartoAtual;


                $(".participante-info").removeClass('d-none');
                if (!data.DadosAdicionais.Quarto && quartoAtual.Quarto) {
                    $(".quarto-info").removeClass('d-none');
                    $('.quarto-atual').text(`${quartoAtual.Participantes}/${quartoAtual.Quarto.Capacidade}`);
                }
                else
                    $(".quarto-info").addClass('d-none');

                if (data.Participante.Checkin) {
                    $('.status').text("Presente");
                    $('.btn-cancelarcheckin').removeClass('d-none');
                    $('.btn-checkin').addClass('d-none');
                } else {
                    $('.btn-cancelarcheckin').addClass('d-none');
                    $('.btn-checkin').removeClass('d-none');
                }

            }
        });
    }

}



function GetEquipante() {
    AplicarCssPadrao($('input'));
    id = $("#equipantes").val() == "Pesquisar" ? 0 : $("#equipantes").val();
    if (id > 0) {
        $.ajax({
            url: "/Equipante/GetEquipanteEvento/",
            data: { Id: id, EventoId: $("#eventoid").val(), },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('.div-map').css('display', 'none')
                $("#participantes").val("Pesquisar").trigger("chosen:updated");
                $('#form-participante').removeClass('d-none');
                $("#equipante-id").val(data.Equipante.Id);
                $("#participante-id").val(0);
                $("#participante-checkin").val(data.Equipante.Checkin);
                $(`#participante-nome`).val(data.Equipante.Nome);
                $(`#participante-apelido`).val(data.Equipante.Apelido);
                $("#participante-data-nascimento").val(moment(data.Equipante.DataNascimento).format('DD/MM/YYYY'));
                $(`#participante-email`).val(data.Equipante.Email);
                $(`#participante-fone`).val(data.Equipante.Fone);

                $(`#participante-restricaoalimentar`).val(data.Equipante.RestricaoAlimentar);
                $(`#participante-medicacao`).val(data.Equipante.Medicacao);
                $(`#participante-alergia`).val(data.Equipante.Alergia);
                $(`input[type=radio][name=participante-sexo][value=${data.Equipante.Sexo}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasalergia][value=${data.Equipante.HasAlergia}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasmedicacao][value=${data.Equipante.HasMedicacao}]`).iCheck('check');
                $(`input[type=radio][name=participante-hasrestricaoalimentar][value=${data.Equipante.HasRestricaoAlimentar}]`).iCheck('check');
                $('.dados-participante-contato').addClass('d-none');
                $('#form-info').addClass('d-none');
                $('.dados-equipante').removeClass('d-none');
                $('.dados-participante-contato input').removeClass('required');
                $('.dados-participante-contato input[id*="fone"]').removeClass('fone');
                $('#marcadores').html(data.Equipante.EtiquetasList.map(etiqueta => `<span  class="badge m-r-xs" style="background-color:${etiqueta.Cor};color:#fff">${etiqueta.Nome}</span>`).join().replace(/,/g, ''))
                $('#participante-etiquetas').html(`${data.Etiquetas.map(etiqueta => `<option data-cor="${etiqueta.Cor}" value=${etiqueta.Id}>${etiqueta.Nome}</option>`)
                    }`)
                $('#participante-etiquetas').val(data.Equipante.EtiquetasList.map(etiqueta => etiqueta.Id))
                $('.participante-etiquetas').select2()
                if (data.Equipante.Foto) {

                    $('#foto').attr("src", 'data:image/jpeg;base64,' + data.Equipante.Foto)
                    $('#div-foto').css('display', 'flex')
                } else {
                    $('#div-foto').css('display', 'none')
                }
                Pagamentos($("#equipantes").val());
                GetAnexos();

                $(`.equipe`).text(data.Equipante.Equipe);
                $('#vacina').html(`<form enctype="multipart/form-data" id="frm-vacina" method="post" novalidate="novalidate">
                        ${!data.Equipante.HasVacina ? ` <label for="input-vacina" class="inputFile" style="margin-bottom:0px">
                            <span style="" class="text-mutted pointer p-l-xs"><i class="fa fa-syringe fa-3x" aria-hidden="true" title="Vacina"></i></span>
                            <input onchange='PostVacina(${data.Equipante.Id},${JSON.stringify(data.Equipante)})' style="display: none;" class="custom-file-input inputFile" id="input-vacina" name="input-vacina" type="file" value="">
                        </label>`: `<span class="text-success p-l-xs pointer" onclick="toggleVacina(${data.Equipante.Id})"><i class="fa fa-syringe fa-3x" aria-hidden="true" title="Vacina"></i></span>`}
                    </form>`)

                $('#teste').html(`<form enctype="multipart/form-data" id="frm-teste" method="post" novalidate="novalidate">
                        ${!data.Equipante.HasTeste ? ` <label for="input-teste" class="inputFile" style="margin-bottom:0px">
                            <span style="" class="text-mutted pointer p-l-xs"><i class="fa fa-microscope fa-3x" aria-hidden="true" title="Vacina"></i></span>
                            <input onchange='PostTeste(${data.Equipante.Id},${JSON.stringify(data.Equipante)})' style="display: none;" class="custom-file-input inputFile" id="input-teste" name="input-teste" type="file" value="">
                        </label>`: `<span class="text-success p-l-xs pointer" onclick="toggleTeste(${data.Equipante.Id})"><i class="fa fa-microscope fa-3x" aria-hidden="true" title="Teste"></i></span>`}
                    </form>`)

                $(".participante-info").removeClass('d-none');

                $(".quarto-info").addClass('d-none');


                if (data.Equipante.Checkin) {
                    $('.status').text("Presente");
                    $('.btn-cancelarcheckin').removeClass('d-none');
                    $('.btn-checkin').addClass('d-none');
                } else {
                    $('.status').text("Confirmado");
                    $('.btn-cancelarcheckin').addClass('d-none');
                    $('.btn-checkin').removeClass('d-none');
                }

            }
        });
    }

}

function toggleTeste(id) {
    $.ajax(
        {
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: $("#pagamentos-participanteid").val() > 0 ? "/Participante/ToggleTeste" : "/Equipante/ToggleTeste",
            data: JSON.stringify(
                {
                    Id: id
                }),

            success: function () {
                if ($("#pagamentos-participanteid").val() > 0) {

                    GetParticipante()
                } else {
                    GetEquipante()
                }



            }
        });
}

function toggleVacina(id) {
    $.ajax(
        {
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: $("#pagamentos-participanteid").val() > 0 ? "/Participante/ToggleVacina" : "/Equipante/ToggleVacina",
            data: JSON.stringify(
                {
                    Id: id
                }),

            success: function () {
                if ($("#pagamentos-participanteid").val() > 0) {

                    GetParticipante()
                } else {
                    GetEquipante()
                }



            }
        });
}


function PostTeste(id, realista) {
    var dataToPost = new FormData($(`#frm-teste`)[0]);
    dataToPost.set($("#pagamentos-participanteid").val() > 0 ? 'ParticipanteId' : 'EquipanteId', $("#pagamentos-participanteid").val() > 0 ? $("#pagamentos-participanteid").val() : $("#pagamentos-equipanteid").val())
    var filename = dataToPost.get(`input-teste`).name
    var arquivo = new File([dataToPost.get(`input-teste`)], 'Teste COVID ' + realista.Nome + filename.substr(filename.indexOf('.')));
    console.log(dataToPost.get(`input-teste`))
    dataToPost.set('Arquivo', arquivo)
    console.log(arquivo)
    dataToPost.set('EventoId', $("#eventoid").val())
    $.ajax(
        {
            processData: false,
            contentType: false,
            type: "POST",
            data: dataToPost,
            url: "/Arquivo/PostArquivo",
            success: function () {
                toggleTeste(id)

            }
        });
}

function PostVacina(id, realista) {
    var dataToPost = new FormData($(`#frm-vacina`)[0]);
    dataToPost.set($("#pagamentos-participanteid").val() > 0 ? 'ParticipanteId' : 'EquipanteId', $("#pagamentos-participanteid").val() > 0 ? $("#pagamentos-participanteid").val() : $("#pagamentos-equipanteid").val())
    var filename = dataToPost.get(`input-vacina`).name
    var arquivo = new File([dataToPost.get(`input-vacina`)], 'Vacina ' + realista.Nome + filename.substr(filename.indexOf('.')));
    dataToPost.set('Arquivo', arquivo)
    dataToPost.set('EventoId', $("#eventoid").val())
    $.ajax(
        {
            processData: false,
            contentType: false,
            type: "POST",
            data: dataToPost,
            url: "/Arquivo/PostArquivo",
            success: function () {
                toggleVacina(id)

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
                    EventoId: $("#eventoid").val(),
                    ParticipanteId: $("#pagamentos-participanteid").val() > 0 ? $("#pagamentos-participanteid").val() : null,
                    EquipanteId: $("#pagamentos-equipanteid").val() > 0 ? $("#pagamentos-equipanteid").val() : null,
                    MeioPagamentoId: $("#pagamentos-meiopagamento").val(),
                    ContaBancariaId: $('.contabancaria').hasClass('d-none') ? 0 : $("#pagamentos-contabancaria").val(),
                    Valor: Number($("#pagamentos-valor").val())
                }),
            success: function () {
                Pagamentos($("#pagamentos-participanteid").val() > 0 ? $("#pagamentos-participanteid").val() : $("#pagamentos-equipanteid").val());
                SuccessMesageOperation();
                GetParticipante();
                GetTotaisCheckin();
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
                    Pagamentos($("#participantes").val());
                    GetParticipante();
                    GetTotaisCheckin();
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

function Pagamentos(id) {
    $('.contabancaria').addClass('d-none');
    $("#pagamentos-valor").val($("#pagamentos-valor").data($("#equipante-id").val() > 0 ? "valor-equipante" : "valor-realista"));
    $("#pagamentos-participanteid").val($("#participante-id").val());
    $("#pagamentos-equipanteid").val($("#equipante-id").val());
    $("#pagamentos-meiopagamento").val($("#pagamentos-meiopagamento option:first").val());
    CarregarTabelaPagamentos($("#equipante-id").val() > 0 ? $("#equipante-id").val() : $("#participante-id").val());
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
            url: $("#participante-id").val() > 0 ? '/Arquivo/GetArquivosParticipante' : '/Arquivo/GetArquivosEquipanteEvento',
            data: $("#participante-id").val() > 0 ? { ParticipanteId: $("#participante-id").val() } : { EquipanteId: $("#equipante-id").val(), eventoid: $("#eventoid").val() },
            datatype: "json",
            type: "POST"
        }
    };
    $("#table-anexos").DataTable(tableArquivoConfig);
}

function PostArquivo() {

    var dataToPost = new FormData($('#frm-upload-arquivo-modal')[0]);
    var filename = dataToPost.get('arquivo-modal').name
    var arquivo = new File([dataToPost.get('arquivo-modal')], filename);
    dataToPost.set('Arquivo', arquivo)
    dataToPost.set('ParticipanteId', $("#participante-id").val() > 0 ? $("#participante-id").val() : null)
    dataToPost.set('EquipanteId', $("#equipante-id").val() > 0 ? $("#equipante-id").val() : null)
    dataToPost.set('EventoId', $("#eventoid").val())
    $.ajax(
        {
            processData: false,
            contentType: false,
            type: "POST",
            data: dataToPost,
            url: "/Arquivo/PostArquivo",
            success: function () {
                if (dataToPost.get('LancamentoIdModal')) {
                    GetAnexosLancamento();
                } else {
                    GetAnexos();
                }

            }
        });
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
                    return `${GetButton('DeletePagamento', data, 'red', 'fa-trash', 'Excluir')}`;
                }
            }
        ],
        order: [
            [0, "asc"]
        ],
        ajax: {
            url: '/Lancamento/GetPagamentos',
            data: $("#participante-id").val() > 0 ? { ParticipanteId: id } : { EquipanteId: id },
            datatype: "json",
            type: "POST"
        }
    };
    $("#table-pagamentos").DataTable(tablePagamentosConfig);
}

function Checkin() {
    $.ajax(
        {
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: $("#participante-id").val() > 0 ? "/Participante/ToggleCheckin" : "/Equipante/ToggleCheckin",
            data: JSON.stringify(
                {
                    Id: $("#participante-id").val() > 0 ? $("#participante-id").val() : $("#equipante-id").val(),
                    EventoId: $("#eventoid").val()
                }),

            success: function () {
                if ($("#pagamentos-participanteid").val() > 0) {

                    GetParticipante()
                } else {
                    GetEquipante()
                }
                GetTotaisCheckin()


            }
        });
}

function CancelarCheckin() {
    $("#participante-checkin").val("false");
    $("#participante-cancelarcheckin").val("true");
    PostParticipante();
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



function CarregarEtiquetaIndividual(position) {
    if ($("#participantes").val() > 0) {

        $.ajax({
            url: "/Participante/GetParticipante/",
            data: { Id: $("#participantes").val() },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                ImprimirIndividual(data, position);
            }
        });
    } else {
        $.ajax({
            url: "/Equipante/GetEquipante/",
            data: { Id: $("#equipantes").val() },
            datatype: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                ImprimirIndividual(data, position);
            }
        });
    }

}


function GetTotaisCheckin() {
    $.ajax({
        url: "/Participante/GetTotaisCheckin/",
        data: { EventoId: $("#eventoid").val() },
        datatype: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('.presentes').text(data.result.Presentes);
            $('.confirmados').text(data.result.Confirmados);
            $('.presentes-equipantes').text(data.result.PresentesEquipantes);
            $('.confirmados-equipantes').text(data.result.ConfirmadosEquipantes);
        }
    });
}

function Refresh() {
    GetTotaisCheckin();
    GetParticipantes();
    GetEquipantes()
}


function ImprimirIndividual(data, position) {
    var printDoc = new jsPDF('p', 'mm', [1056, 816]);
    printDoc.setFont('helvetica', "normal")
    printDoc.setFontSize(14);
    width = position % 2 == 0 ? 212 : 73;
    multiplicador = 0;

    switch (position) {
        case 1:
            multiplicador = 0;
            break;
        case 2:
            multiplicador = 0;
            break;
        case 3:
            multiplicador = 1;
            break;
        case 4:
            multiplicador = 1;
            break;
        case 5:
            multiplicador = 2;
            break;
        case 6:
            multiplicador = 2;
            break;
        case 7:
            multiplicador = 3;
            break;
        case 8:
            multiplicador = 3;
            break;
        case 9:
            multiplicador = 3;
            break;
        case 10:
            multiplicador = 4;
            break;
        case 11:
            multiplicador = 5;
            break;
        case 12:
            multiplicador = 5;
            break;
        case 13:
            multiplicador = 6;
            break;
        case 14:
            multiplicador = 6;
            break;
        default:
    }

    if ($('#participante-id').val() > 0) {
        var participante = data.Participante;
        heightNome = multiplicador * 45;
        heightApelido = multiplicador * 45;
        heightNome += 60;
        heightApelido += 50;
        printDoc.setFont('helvetica', "bold")
        printDoc.setFontSize(30);
        printDoc.text(width, heightApelido, participante.Apelido, 'center');
        printDoc.setFont('helvetica', "normal")
        printDoc.setFontSize(18);
        var splitNome = printDoc.splitTextToSize(participante.Nome, 100);
        printDoc.text(width, heightNome, splitNome, 'center');

    } else {
        var equipante = data.Equipante;
        heightNome = multiplicador * 45;
        heightApelido = multiplicador * 45;
        heightEquipe = multiplicador * 45;
        heightNome += 42;
        heightApelido += 57;
        heightEquipe += 68;
        printDoc.setFont('helvetica', "normal")
        printDoc.setFontSize(18);
        printDoc.text(width, heightEquipe, "  (" + equipante.Equipe + ")", 'center');
        var splitNome = printDoc.splitTextToSize(equipante.Nome, 100);
        printDoc.text(width, heightNome, splitNome, 'center');
        printDoc.setFont('helvetica', "bold")
        printDoc.setFontSize(30);
        printDoc.text(width, splitNome.length > 1 ? heightApelido + 4 : heightApelido, equipante.Apelido, 'center');
    }

    printDoc.autoPrint();
    window.open(printDoc.output('bloburl'), '_blank');
    $("#modal-etiquetas").modal("hide");
    Refresh();
}

$("#arquivo-modal").change(function () {
    PostArquivo();
});

if ($('#map').length > 0) {

    const map = initMap('map')
    const markerLayer = createMarkerLayer(map)
    function montarMapa() {
        markerLayer.getLayers().forEach(mark => mark.remove())
        var marker = L.marker([$(`#participante-latitude`).val().toString(), $(`#participante-longitude`).val().toString()], { icon: getIcon('vermelho') }).addTo(markerLayer);
        marker.bindPopup(`<h4>${$(`#participante-nome`).val()}</h4>`).openPopup();
        $('.div-map').css('display', 'block')
        map.setView([$(`#participante-latitude`).val(), $(`#participante-longitude`).val()], 18);
    }
    function verificaCep(input) {
        let cep = $(input).val()
        if (cep.length == 9) {
            $.ajax({
                url: `https://api.iecbeventos.com.br/cep/${cep.replaceAll('-', '')}`,
                datatype: "json",
                type: "GET",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $(`#participante-logradouro`).val(data.logradouro)
                    $(`#participante-bairro`).val(data.bairro)
                    $(`#participante-cidade`).val(data.localidade)
                    $(`#participante-estado`).val(data.uf)
                    $(`#participante-latitude`).val(data.lat)
                    $(`#participante-longitude`).val(data.lon)
                    montarMapa()
                }
            })
        }
    }
}

