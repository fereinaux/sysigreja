function CarregarTabelaRelatorio() {
    const tableRelatorioConfig = {
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
        dom: domConfigNoButtons, 
        columns: [
            { autoWidth: true },
            { orderable: false, width: "5%" }            
        ],
        order: [
            [0, "asc"]
        ]
    };

    $("#table-relatorios").DataTable(tableRelatorioConfig);
}

$(document).ready(function () {
    CarregarTabelaRelatorio();
    MontarBotoes();
});

function MontarBotoes() {
    $('.aniversariantes').html(`${GetButton('PrintAniversariantes', '', 'green', 'fa-print', 'Imprimir Aniversariantes')}`);
    $('.restricoes').html(`${GetButton('PrintRestricoes', '', 'green', 'fa-print', 'Imprimir Restrições Alimentares')}`);
    $('.parentes').html(`${GetButton('PrintParentes', '', 'green', 'fa-print', 'Imprimir Relação de Parentes')}`);
    $('.financeiro').html(`${GetButton('PrintFinanceiro', '', 'green', 'fa-print', 'Imprimir Relatório Financeiro')}`);
}

