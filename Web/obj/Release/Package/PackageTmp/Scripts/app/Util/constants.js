const InscricoesAbertas = "Aberto";
const Ativo = "Ativo";
const Inativo = "Inativo";
const Inscrito = "Inscrito";
const Espera = "Em Espera";
const Cancelado = "Cancelado";
const Confirmado = "Confirmado";
const Membro = "Membro";
const Coordenador = "Coordenador";
const InscricoesEncerradas = "Encerrado";
const Transferencia = "Transferência Bancária";
const Boleto = "Boleto Bancário";

function GetCor(cor) {
    switch (cor) {
        case "Vermelho":
            return "#e23c2b";
        case "Amarelo":
            return "#c7a70d";
        case "Laranja":
            return "#d6690b";
        case "Rosa":
            return "#e838ba";
        case "Azul Escuro":
            return "#164779";
        case "Azul Claro":
            return "#5c99d8";
        case "Verde Escuro":
            return "#39791a";
        case "Verde Claro":
            return "#84d85c";
        case "Lilás":
            return "#8b52d8";
        case "Cinza":
            return "#7b787f";
        default:
    }
}

function GetCorById(id) {
    switch (id) {
        case 0:
            return "Vermelho";
        case 1:
            return "Laranja";
         case 2:
            return "Roxo";
         case 3:
            return "Azul";
         case 4:
            return "Rosa";
         case 5:
            return "Amarelo";     
    }
}
