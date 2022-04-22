function GetButton(functionClick, functionParameter, color, icon, title) {
    var button = '';

    switch (color) {
        case 'red': button = 'text-danger'; break;
        case 'yellow': button = 'text-warning'; break;
        case 'blue': button = 'text-info'; break;
        case 'green': button = 'text-success'; break;
        case 'verde': button = 'text-verde'; break;
        case 'cinza': button = 'text-mutted'; break;
        default: button = 'text-info'; break;
    }

    return `<span onclick='${functionClick}(${functionParameter})' style="font-size:18px" class="${button} pointer p-l-xs"><i class="${(!icon.includes('far') && !icon.includes('fab')) ? 'fa' : ''} ${icon}" aria-hidden="true" title="${title}"></i></span>`;
}

function GetAnexosButton(functionClick, functionParameter, qtd) {

   return `<span onclick='${functionClick}(${functionParameter})' style="font-size:18px;position:relative" class="text-success  has-badge pointer p-l-xs"  ${qtd > 0 ? 'data-count="' + qtd + '"' : ''}> <i class="fa fa-paperclip data-counter" aria-hidden="true" title="Anexos"></i></span>`

    
}

function GetIconWhatsApp(tel,text) {  
    
    return `<a target="_blank" href='${GetLinkWhatsApp(tel, text)}' style="font-size:18px; color:green; " class="pointer p-l-xs"><i class="fab fa-whatsapp" aria-hidden="true" title="${tel}"></i></a>`;
}

function GetConvidar(tel, nome) {
   
    return `<a target="_blank" href='${GetLinkWhatsApp(tel, Convidar(nome))}' style="font-size:13px" class="text-center label label-primary">Convidar</span>`
}

function GetLinkWhatsApp(tel, text) {
    if (!tel || !text)
        return ""
    tel = tel.replaceAll(' ', '').replaceAll('+', '').replaceAll('(', '').replaceAll(')', '').replaceAll('.', '').replaceAll('-', '');
    text = typeof text !== "undefined" ? `&text=${EncodeUrl(text)}` : "";
    return `https://api.whatsapp.com/send?phone=${tel}${text}&source=&data=`;
}


function GetIconTel(tel) {
    return `<a target="_blank" href="tel:${tel}" style="font-size:18px" class="pointer p-l-xs"><i class="fa fa-phone" aria-hidden="true" title="${tel}"></i></a>`;
}

function GetLabel(functionClick, functionParameter, color, title) {

    var button = '';

    switch (color) {
        case 'red': button = 'danger'; break;
        case 'yellow': button = 'warning'; break;
        case 'blue': button = 'success'; break;
        case 'green': button = 'primary'; break;
        case 'info': button = 'info'; break;
        default: button = 'info'; break;
    }

    return `<span onclick="${functionClick}(${functionParameter})" style="font-size:13px" class="m-r-sm pointer text-center label label-${button}">${title}</span>`;
}

function GetCheckBox(functionClick, functionParameter, id, checked) {
    return `<div class="checkbox i-checks-green"><label> <input type="checkbox" data-id="${id}" value="" ${checked ? 'checked=""' : ''}> <i></i></label></div>`;
}