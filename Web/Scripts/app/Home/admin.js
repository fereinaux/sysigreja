$(document).ready(() => {
    HideMenu();
    GetResultadosAdmin();
});

function getEquipantesExcel() {
    $.ajax({
        url: "/Equipante/getEquipantesExcel/",
        datatype: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            { EventoId: $("#eventoid").val() }),
        success: (data) => {

            window.location = `DownloadTempFile?fileName=Equipe ${$("#eventoid option:selected").text()}.xlsx&g=` + data;
        }
    });
}

function GetResultadosAdmin() {
    $.ajax({
        url: '/Home/GetResultadosAdmin',
        datatype: "json",
        data: { EventoId: $("#eventoid").val() },
        type: "GET",
        success: (data) => {
            result = data.result;
            if (result.Evento == InscricoesEncerradas) {
                $('.detalhamento-equipes').show();
                $.ajax({
                    url: '/Home/GetDetalhamentoEvento',
                    datatype: "json",
                    data: { EventoId: $("#eventoid").val() },
                    type: "GET",
                    success: (data2) => {
                        result2 = data2.result;
                        htmlDetalhamento = '';
                        htmlDetalhamentoMobile = '';
                        equipe = '';
                        totalEquipe = 0;
                        totalGeral = 0;
                        $(result2.Equipantes).each((i, element) => {
                            if (equipe != element.Equipe) {
                                if (totalEquipe > 0) {
                                    htmlDetalhamento += `<tr>                        
                                        <td class="font-bold">Total: ${totalEquipe}</td>                        
                                        <td></td>                                                
                                    </tr>`;
                                    htmlDetalhamentoMobile += `<div class="col col-xs-12 m-b-md">
                                                                <h2>Total: ${totalEquipe}</h2>
                                                            </div>
                                                            `

                                    totalEquipe = 0;
                                }
                                htmlDetalhamento += `<tr>                        
                                    <td class="font-bold">Equipe: ${element.Equipe}</td>                        
                                    <td></td>                                                
                                </tr>`;
                                htmlDetalhamentoMobile += `<div class="col col-xs-12 m-b-md">
                                                                <h2>${element.Equipe}</h2>
                                                            </div>
                                                            `

                             
                                if (element.Tipo == "Coordenador") {
                                    htmlDetalhamento += `<tr>                        
                                    <td class="font-bold">Coordenador: ${element.Nome}</td>                        
                                    <td class="equipante-fone">${element.Fone}</td>                                                
                                </tr>`;
                                  
                                } else {
                                    htmlDetalhamento += `<tr>                        
                                    <td>${element.Nome}</td>                        
                                    <td class="equipante-fone">${element.Fone}</td>                                                
                                </tr>`;
                                }                               

                                equipe = element.Equipe;
                            } else {
                                if (element.Tipo == "Coordenador") {
                                    htmlDetalhamento += `<tr>                        
                                    <td class="font-bold">Coordenador: ${element.Nome}</td>                        
                                    <td class="equipante-fone">${element.Fone}</td>                                                
                                </tr>`;
                                } else {
                                    htmlDetalhamento += `<tr>                        
                                    <td>${element.Nome}</td>                        
                                    <td class="equipante-fone">${element.Fone}</td>                                                
                                </tr>`;
                                }
                            }

                            htmlDetalhamentoMobile += `<div class="col col-xs-6">
                                                                <div class="equipe-mobile black-bg">
                                                                    ${element.Tipo == "Coordenador" ? '<img src="/Images/crown.png" />' : ''} 
                                                                    <div class="mobile-content">
                                                                        <h4>${element.Nome}</h4>
                                                                    </div>
                                                                    <a target="_blank" href='${GetLinkWhatsApp(element.Fone, Convidar(element.Nome))}' class="btn-convidar btn-primary btn">
                                                                        <i class="fab fa-whatsapp" aria-hidden="true" title="WhatsApp"></i>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            `

                            totalEquipe++;
                            totalGeral++;
                        });
                        htmlDetalhamento += `<tr>                        
                                        <td class="font-bold">Total: ${totalEquipe}</td>                        
                                        <td></td>                                                
                                    </tr>`;
                        htmlDetalhamentoMobile += `<div class="col col-xs-12 m-b-md">
                                                                <h2>Total: ${totalEquipe}</h2>
                                                            </div>
                                                            `

                        htmlDetalhamento += `<tr>                        
                                        <td class="font-bold">Total Geral: ${totalGeral}</td>                        
                                        <td></td>                                                
                                    </tr>`;

                        htmlDetalhamentoMobile += `<div class="col col-xs-12 m-b-md">
                                                                <h2>Total Geral: ${totalGeral}</h2>
                                                            </div>
                                                            `

                        $('#tb-detalhamento-equipes').html(htmlDetalhamento);
                        $('.detalhamento-mobile-container').html(htmlDetalhamentoMobile);

                        $(".equipante-fone").each((i, element) => {

                            $(element).html(`${GetConvidar($(element).text(), $($(element).parent().children()[0]).text().replace('Coordenador: ', ''))}
                            ${GetIconWhatsApp($(element).text())}
                            ${GetIconTel($(element).text())}`);
                        });
                    }
                })
            } else
                $('.detalhamento-equipes').hide();
            $("#total").text(result.Total);
            $("#espera").text(result.Espera);
            $("#confirmados").text(result.Confirmados);
            $("#isencoes").text(result.Isencoes);
            $("#presentes").text(result.Presentes);
            $("#cancelados").text(result.Cancelados);
            $("#meninos").text(result.Meninos);
            $("#meninas").text(result.Meninas);
            $("#equipe-male").text(result.EquipeMeninos);
            $("#equipe-female").text(result.EquipeMeninas);
            $("#boletos").text(result.Boletos);
            $("#contatos").text(result.Contatos);
            $("#saldo").text(result.SaldoGeral);
            $("#saldodinheiro").text(result.SaldoDinheir);
            $("#saldopix").text(result.SaldoPix);
            $("#receita").text(result.TotalReceber);
            $("#despesa").text(result.TotalPagar);

            htmlInscritos = '';
            $(result.UltimosInscritos).each((i, element) => {
                htmlInscritos += `<tr>                        
                        <td>${element.Nome}</td>                        
                        <td>${element.Idade}</td>                                                
                    </tr>`;
            });

            $('#ultimos-inscritos').html(htmlInscritos);

            htmlEquipes = '';
            htmlEquipesMobile = '';
            totalEquipe = 0;
            $(result.Equipes).each((i, element) => {
                totalEquipe += element.QuantidadeMembros;
                htmlEquipesMobile += `  <div class="col col-xs-6">
                                <div class="equipe-mobile black-bg">
                                    <div class="mobile-content">
                                        <h3>${element.QuantidadeMembros}</h3>
                                        <p>${element.Equipe}</p>
                                    </div>
                                </div>
                            </div>`
                htmlEquipes += `<tr>                        
                        <td>${element.Equipe}</td>                        
                        <td>${element.QuantidadeMembros}</td>                                                
                    </tr>`;
            });

            htmlEquipesMobile += `  <div class="col col-xs-6">
                                <div class="equipe-mobile navy-bg">
                                    <div class="mobile-content">
                                        <h3>${totalEquipe}</h3>
                                        <p>Total</p>
                                    </div>
                                </div>
                            </div>`

            $('#tb-equipes').html(htmlEquipes);
            $('.equipe-mobile-container').html(htmlEquipesMobile);
            $('#totalEquipe').text(totalEquipe);

            htmlReunioes = '';
            $(result.Reunioes).each((i, element) => {
                htmlReunioes += `<tr>                        
                        <td>${moment(element.DataReuniao).format('DD/MM/YYYY')}</td>                        
                        <td>${element.Presenca}</td>                                                
                    </tr>`;
            });

            $('#tb-reunioes').html(htmlReunioes);
        }
    });
}
