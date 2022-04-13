jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "num-br-pre": function (data) {
        return parseFloat(data.replace("R$", "").replace("&nbsp;", "").replace(/\./g, "").replace(",", "."));
    },
    "num-br-asc": function (a, b) {
        console.log(a);
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },
    "num-br-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    },
    "mesano-br-pre": function (data) {
        var partes = data.split("/");
        return partes[1] + partes[0];
    },
    "mesano-br-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },
    "mesano-br-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    },
    "data-br-pre": function (a) {
        if (a == null || a == "") {return 0;}
        var dataBR = a.split('/');
        return (dataBR[2] + dataBR[1] + dataBR[0]) * 1;
    },
    "data-br-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },
    "data-br-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    },
});

const languageConfig = {
    decimal: ",",
    thousands: ".",
    emptyTable: "Nenhum registro encontrado",
    info: "Apresentando _START_ até _END_ de _TOTAL_ registro(s)",
    infoEmpty: "",
    infoFiltered: "(filtrado de _MAX_ registros)",
    infoPostFix: "",
    lengthMenu: "Mostrar _MENU_ registros",
    loadingRecords: "Carregando...",
    processing: "Processando...",
    search: "Pesquisar:",
    zeroRecords: "Nenhum registro encontrado",
    paginate: {
        "first": "Primeira",
        "last": "Última",
        "next": "Próxima",
        "previous": "Anterior"
    },
    aria: {
        sortAscending: ": ative para ordenar a coluna em ordem crescente",
        sortDescending: ": ative para ordenar a coluna em ordem decrescente"
    }
};

const domConfig = '<"html5buttons"B>lfTgitpr';
const domConfigNoButtons = 'lfTgitpr';
const defaultLengthMenu = [100, 1000, 10000, 100000];
const defaultScrollY = 400;

function getButtonsConfig(fileName) {
    return [
        { extend: 'excel', title: fileName }        
    ];
}
