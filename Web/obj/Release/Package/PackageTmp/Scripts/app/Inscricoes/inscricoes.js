var lat, lon;

if ($('#map').length > 0) {

    const map = initMap('map')
    const markerLayer = createMarkerLayer(map)

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
                    markerLayer.getLayers().forEach(mark => mark.remove())
                    var marker = L.marker([data.lat, data.lon], { icon: getIcon('vermelho') }).addTo(markerLayer);
                    $('#map').css('display', 'block')
                    map.setView([data.lat, data.lon], 18);
                }
            })
        }
    }
}

function VerificaCadastro() {
    if (IsEmail($(`#participante-email`).val())) {
        AplicarCssPadrao($(`#participante-email`));
        $.ajax({
            url: "/Inscricoes/VerificaCadastro",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                Email: $(`#participante-email`).val()
            }),
            success: function (data) {
                if (data.Participante) {
                    $(`#participante-nome`).val(data.Participante.Nome);
                    $(`#participante-apelido`).val(data.Participante.Apelido);
                    $("#participante-data-nascimento").val(moment(data.Participante.DataNascimento).format('DD/MM/YYYY'));
                    $(`#participante-email`).val(data.Participante.Email);
                    $(`#participante-fone`).val(data.Participante.Fone);
                    $(`#participante-nome-pai`).val(data.Participante.NomePai);
                    $(`#participante-fone-pai`).val(data.Participante.FonePai);
                    $(`#participante-nome-mae`).val(data.Participante.NomeMae);
                    $(`#participante-fone-mae`).val(data.Participante.FoneMae);
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
                }
                else if (data) {
                    window.location.href = data;
                }
                $("#participante-email").prop("disabled", true)
                $(".pnl-cadastro").show();
                $(".pnl-verifica").hide();
                $('.inscricoes.middle-box').height('80%');
                $('.float').css("bottom", "40px")

            }
        })
    } else {
        AplicarCssErro($(`#participante-email`));
        ErrorMessage(`Erros de Formatação:
Email: exemplo@provedor.com.br`);
    }

}

function PostInscricao() {
    if (ValidateForm(`#form-inscricao`)) {
        $.ajax({
            url: "/Inscricoes/PostInscricao/",
            datatype: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    Nome: $(`#participante-nome`).val(),
                    Apelido: $(`#participante-apelido`).val(),
                    DataNascimento: moment($("#participante-data-nascimento").val(), 'DD/MM/YYYY', 'pt-br').toJSON(),
                    Email: $(`#participante-email`).val(),
                    Fone: $(`#participante-fone`).val(),
                    Instagram: $(`#participante-instagram`).val(),
                    Profissao: $(`#participante-profissao`).val(),
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
            success: function (url) {
                window.location.href = url;
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

$('#has-parente').on('ifChecked', function (event) {
    $('.parente').removeClass('d-none');
    $("#participante-parente").addClass('required');
});

$('#not-parente').on('ifChecked', function (event) {
    $('.parente').addClass('d-none');
    $("#participante-parente").removeClass('required');
});

$('#trindade').on('ifChecked', function (event) {
    $('.congregacao').addClass('d-none');
    $("#participante-congregacaodescricao").removeClass('required');
});

$('#recon').on('ifChecked', function (event) {
    $('.congregacao').addClass('d-none');
    $("#participante-congregacaodescricao").removeClass('required');
});

$('#outra').on('ifChecked', function (event) {
    $('.congregacao').removeClass('d-none');
    $("#participante-congregacaodescricao").addClass('required');
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

$(".pnl-cadastro").hide();
$('.inscricoes.middle-box').height('25%');
$('.float').css("bottom", "34%")