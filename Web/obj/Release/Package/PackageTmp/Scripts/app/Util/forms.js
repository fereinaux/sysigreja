
var isMobile = false; //initiate as false
// device detection
if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
    || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
    isMobile = true;
}

if ($(window).width() < 500)
    isMobile = true;

function HideMenu() {
    if (isMobile) {
        return
    }
    $("body").addClass("mini-navbar");
}

function ValidateForm(form) {
    var formResult = IniciarFormResult();

    formResult = ValidateRequired(form, formResult);
    formResult = ValidateMinLength(form, formResult);
    formResult = ValidateMaxLength(form, formResult);
    formResult = ValidateEmail(form, formResult);
    formResult = ValidateFone(form, formResult);

    var camposObrigatorios = "";

    if (!formResult.IsValid) {
        if (formResult.ErrorsInput != "") {
            camposObrigatorios = `Campos Obrigatórios:${formResult.ErrorsInput}`;
        }
        if (formResult.ErrorsMinLength != "") {
            camposMinLength = `Mínimo de caracteres:${formResult.ErrorsMinLength}`;
        }
        if (formResult.ErrorsMaxLength != "") {
            camposMaxLength = `Máximo de caracteres:${formResult.ErrorsMaxLength}`;
        }
        if (formResult.ErrorsFormatacao != "") {
            camposFormatacao = `Erros de Formatação:${formResult.ErrorsFormatacao}`;
        }
        formResult.MessageError = `${typeof camposObrigatorios === "undefined" ? "" : camposObrigatorios}  
                                   ${typeof camposMinLength === "undefined" ? "" : camposMinLength} 
                                   ${typeof camposMaxLength === "undefined" ? "" : camposMaxLength} 
                                   ${typeof camposFormatacao === "undefined" ? "" : camposFormatacao}`;

        ErrorMessage(formResult.MessageError);

        return false;
    }

    return true;
}

function ValidateRequired(form, formResult) {
    AplicarCssPadrao($(`${form} .required`));

    $(`${form} input.required`).each(function () {
        var input = $(this);
        if (!input.val()) {
            formResult.IsValid = false;
            AplicarCssErro(input);
            formResult.ErrorsInput += AddErro(input.data("field"));
        }
    });

    return formResult;
}

function ValidateMinLength(form, formResult) {
    $(`${form} input`).each(function () {
        var input = $(this);
        if (input.data("min-length") > 0) {
            AplicarCssPadrao(input);
            if (input.val().length < input.data("min-length")) {
                formResult.IsValid = false;
                AplicarCssErro(input);
                formResult.ErrorsMinLength += AddErro(`${input.data("field")} deve ter um mínimo de ${input.data("min-length")} caracteres`);
            }
        }
    });

    return formResult;
}

function ValidateEmail(form, formResult) {
    $(`${form} input[type=email]`).each(function () {
        var input = $(this);
        AplicarCssPadrao(input);
        if (!IsEmail(input.val())) {
            formResult.IsValid = false;
            AplicarCssErro(input);
            formResult.ErrorsFormatacao += AddErro(`${input.data("field")}: exemplo@provedor.com.br`);
        }
    });

    return formResult;
}

function ValidateFone(form, formResult) {
    $(`${form} input.fone`).each(function () {
        var input = $(this);
        AplicarCssPadrao(input);
        if (!IsFone(input.val())) {
            formResult.IsValid = false;
            AplicarCssErro(input);
            formResult.ErrorsFormatacao += AddErro(`${input.data("field")}: +55(81)9.9999-9999`);
        }
    });

    return formResult;
}

function ValidateCPF(form, formResult) {
    $(`${form} input.cpf`).each(function () {
        var input = $(this);
        AplicarCssPadrao(input);
        if (!IsCPF(input.val())) {
            formResult.IsValid = false;
            AplicarCssErro(input);
            formResult.ErrorsFormatacao += AddErro(`${input.data("field")}: 999.999.999-99`);
        }
    });

    return formResult;
}

function ValidateMaxLength(form, formResult) {
    $(`${form} input`).each(function () {
        var input = $(this);
        if (input.data("max-length") > 0) {
            AplicarCssPadrao(input);
            if (input.val().length > input.data("max-length")) {
                formResult.IsValid = false;
                AplicarCssErro(input);
                formResult.ErrorsMaxLength += AddErro(`${input.data("field")} deve ter um máximo de ${input.data("max-length")} caracteres`);
            }
        }
    });

    return formResult;
}

function IniciarFormResult() {
    return FormResult = {
        IsValid: true,
        ErrorsInput: "",
        ErrorsMinLength: "",
        ErrorsMaxLength: "",
        ErrorsFormatacao: "",
        MessageError: ""
    };
}

function AddErro(erro) {
    return `\n - ${erro}`;
}

function AplicarCssPadrao(input) {
    input.css("background-color", "#ffffff");
    input.css("border", "1px solid #e5e6e7");
}

function AplicarCssErro(input) {
    input.css("background-color", "#f4bebe");
    input.css("border", "2px solid #dc0b17");
}

function IsEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function IsFone(fone) {
    return fone.indexOf("+55") == 0 && fone.indexOf("(") == 3 && fone.indexOf(")") == 6 && fone.indexOf("9.") == 7 && fone.indexOf("-") == 13 && fone.length == 18;
}

function IsCPF(cpf) {
    return cpf.length == 14;
}