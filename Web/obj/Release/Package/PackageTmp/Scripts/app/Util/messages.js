function ErrorMessage(message) {
    swal({
        title: "Erro!",
        icon: "error",
        text: message
    });
}

function SuccessMessage(message) {
    swal({
        title: "Sucesso!",
        icon: "success",
        text: message
    });
}

function SuccessMesageDelete() {
    SuccessMessage("Registro excluído!");
}

function SuccessMesageOperation() {
    SuccessMessage("A operação foi concluída!");
}

async function ConfirmMessage(message) {
    return await swal({
        title: "Você tem certeza?",
        icon: "warning",
        text: message,
        buttons: {
            cancel: "Desistir",
            confirm: "Confirmar"
        }
    });
}

async function ConfirmMessageDelete() {
    return await ConfirmMessage("Essa ação excluirá permanentemente o registro, deseja continuar?");
}

async function ConfirmMessageCancelar(nome) {
    return await ConfirmMessage(`Deseja cancelar a inscrição de ${nome}?`);
}

function RebciboPagamento(valor, formaPagamento, evento) {
    return `Aqui está o seu recibo de pagamento do Realidade:/n/n*R$ ${valor} - ${formaPagamento}*${RodapeEvento(evento)}`;
}

function RodapeEvento(evento) {
    return `*Agradecemos a compreensão!*
*Equipe da Secretaria | ${evento}*
`;
}

function Convidar(nome) {
    return `Olá *${nome}*,

Você gostaria de trabalhar no próximo Realidade de 25 a 27 de Março nossa primeira reunião será na quarta feira (16/02) às 19h30 na *Catedral da Trindade*.

Esse convite é pessoal e *intransferível*.

Carol Bastos.`;
}