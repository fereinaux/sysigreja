$(document).ready(function () {
    $('.chosen-select').chosen({ width: "100%" });

    $('.full-date').datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",        
        autoclose: true
    });


    $('.full-date').each((i) => {
        IMask($('.full-date')[i], {
            mask: '00/00/0000'
        });
    });   

    $('.i-checks-green').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });

    $('.i-checks-brown').iCheck({
        checkboxClass: 'icheckbox_square-brown',
        radioClass: 'iradio_square-brown'
    });

    $('.fone').each((i) => {
        IMask($('.fone')[i], {
            mask: '+{55}(00)0.0000-0000'
        });
    });  

    $('.cpf').each((i) => {
        IMask($('.cpf')[i], {
            mask: '000.000.000-00'
        });
    });

    $('.cep').each((i) => {
        IMask($('.cep')[i], {
            mask: '00000-000'
        });
    });
});
