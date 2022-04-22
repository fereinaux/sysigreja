using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.ComponentModel;

namespace Utils.Enums
{
<<<<<<< HEAD
    public enum TipoCirculoEnum
    {
        [Description("Endereco")]
        Endereco,
        [Description("Aleatório")]
        Aleatorio,
        [Description("Idade")]
        Idade,
    }

    public enum TipoPessoaEnum
    {
        [Description("Equipante")]
        Equipante,
        [Description("Participante")]
        Participante
    }
    public enum CamposEnum
    {
        [Description("Email")]
        Email,
        [Description("Nome Completo")]
        Nome,
        [Description("Apelido")]
        Apelido,
        [Description("Gênero")]
        Genero,
        [Description("Data Nascimento")]
        DataNascimento,
        [Description("Fone")]
        Fone,
        [Description("Instagram")]
        Instagram,
        [Description("Profissão")]
        Profissao,
        [Description("Endereço")]
        Endereco,
        [Description("Dados da Mãe")]
        Mae,
        [Description("Dados do Pai")]
        Pai,
        [Description("Dados do Contato")]
        Contato,
        [Description("Dados do Convite")]
        Convite,
        [Description("Ônibus")]
        Onibus,
        [Description("Parente")]
        Parente,
        [Description("Congregação")]
        Congregacao,
        [Description("Alergia")]
        Alergia,
        [Description("Medicação")]
        Medicacao,
        [Description("Restrição Alimentar")]
        Restricao,


    }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    public enum StatusEnum
    {
        [Description("Ativo")]
        Ativo = 1,
        [Description("Inativo")]
        Inativo = 2,
        [Description("Deletado")]
        Deletado = 3,
        [Description("Aberto")]
        Aberto = 4,
        [Description("Quitado")]
        Quitado = 5,
        [Description("Cancelado")]
        Cancelado = 6,
        [Description("Encerrado")]
        Encerrado = 7,
        [Description("Inscrito")]
        Inscrito = 8,
        [Description("Confirmado")]
        Confirmado = 9,
        [Description("Em Espera")]
        Espera = 10
    }

    public enum SexoEnum
    {
        [Description("Masculino")]
        Masculino = 1,
        [Description("Feminino")]
        Feminino = 2
    }

    public enum EquipesEnum
    {
<<<<<<< HEAD
        [Description("Ordem")]
        Ordem = 1,
        [Description("Minimercado")]
        Minimercado = 2,
        [Description("Assistentes Espirituais")]
        AssistentesEspirituais = 3,
        [Description("Apresentadores")]
        Apresentadores = 4,
        [Description("Secretaria")]
        Secretaria = 5,
        [Description("Boa Vontade")]
        BoaVontade = 6,
        [Description("Círculos")]
        Circulo = 7,
        [Description("Liturgia")]
        Liturgia = 8,
        [Description("Som/Iluminação")]
        Som = 9,
        [Description("Bandinha")]
        Bandinha = 10,
        [Description("Cozinha")]
        Cozinha = 11,
        [Description("Médicos")]
        Medicos = 12,
        [Description("Lanche")]
        Lanche = 13,
        [Description("Garçons")]
        Garcons = 14,
        [Description("Vigília")]
        Vigilia = 15,
        [Description("Compras")]
        Compras = 16,
        [Description("Infraestrutura")]
        Infraestrutura = 17,
        [Description("Finanças")]
        Financas = 18,
        [Description("Recepção aos Palestrantes")]
        Recepcao = 19,
        [Description("Externa")]
        Externa = 20,
        [Description("Coordenador Geral")]
        Coordenador = 21,
        [Description("Livraria")]
        Livraria = 22,
        [Description("Trânsito")]
        Transito = 23,
        [Description("Pastores")]
        Pastores = 24,
        [Description("Equipe Dirigente")]
        ED = 25,
        [Description("Lider Espiritual")]
        LiderEspiritual = 26,
=======
        [Description("Vaila de Apoio Integral")]
        VailaAI = 1,
        [Description("Refeitório")]
        Refeitorio = 2,
        [Description("Grupo de Oração e Encorajamento")]
        GOE = 3,
        [Description("Reitores")]
        Reitores = 4,
        [Description("Secretaria")]
        Secretaria = 5,
        [Description("Circulação")]
        Circulacao = 6,
        [Description("Vaila de Pequeno Grupo")]
        Circulo = 7,
        [Description("Drama")]
        Drama = 8,
        [Description("Mídia")]
        Midia = 9,
        [Description("Louvor")]
        Louvor = 10,
        [Description("Cozinha")]
        Cozinha = 11,
        [Description("Pastores")]
        Pastores = 12,
        [Description("Bomboniere")]
        Bomboniere = 13,
        [Description("Coordenação")]
        Coordenacao = 14
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }

    public enum TiposEventoEnum
    {
<<<<<<< HEAD
        [Nickname("EJC")]
        [Description("Encontro de Jovens com Cristo")]
        [Equipes(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 })]
        EJC = 1,
=======
        [Nickname("Realidade")]
        [EmailPagSeguro("")]
        [TokenPagSeguro("")]
        [Description("Realidade")]
        [Equipes(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,13,14 })]
        Realiadde = 1,
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

    }

    public enum BancosEnum
    {
        [Description("Bradesco")]
        Bradesco = 1,
        [Description("Santander")]
        Santander = 2,
        [Description("Itaú")]
        Itau = 3,
        [Description("Caixa")]
        Caixa = 4,
        [Description("Banco do Brasil")]
        BancoBrasil = 5,
        [Description("NuBank")]
        Nubank = 6,
        [Description("Banco Inter")]
        Inter = 7,
        [Description("HSBC")]
        HSBC = 8
    }

    public enum TiposEquipeEnum
    {
        [Description("Coordenador")]
        Coordenador = 1,
        [Description("Membro")]
        Membro = 2
    }

    public enum PerfisUsuarioEnum
    {
        [Description("Master")]
        Master,
        [Description("Admin")]
        Admin,
        [Description("Coordenador")]
        Coordenador,
        [Description("Secretaria")]
        Secretaria,
    }

    public enum TiposLancamentoEnum
    {
        [Description("Receber")]
        Receber = 1,
        [Description("Pagar")]
        Pagar = 2
    }

    public enum TiposCentroCustoEnum
    {
        [Description("Receita")]
        Receita = 1,
        [Description("Despesa")]
        Despesa = 2
    }

    public enum CentroCustoPadraoEnum
    {
        [Description("Inscrições")]
        Inscricoes = 1,
        [Description("Taxa de Equipante")]
        TaxaEquipante = 2
    }

    public enum ValoresPadraoEnum
    {
        [Description("Inscrições")]
        Inscricoes = 300,
        [Description("Taxa de Equipante")]
        TaxaEquipante = 150
    }

    public enum MeioPagamentoPadraoEnum
    {
        [Description("Pix")]
        Transferencia,
        [Description("Dinheiro")]
        Dinheiro,
        [Description("Isenção")]
        Isencao
    }

    public enum CoresEnum
    {
        [Description("Vermelho")]
        Vermelho,
        [Description("Laranja")]
        Laranja,
        [Description("Rosa")]
        Rosa,
        [Description("Azul Claro")]
        AzulClaro,
        [Description("Azul Escuro")]
        AzulEscuro,
        [Description("Verde Claro")]
        VerdeClaro,
        [Description("Verde Escuro")]
        VerdeEscuro,
        [Description("Amarelo")]
        Amarelo,
        [Description("Cinza")]
        Cinza,
        [Description("Lilás")]
        Lilas
    }

    internal class NicknameAttribute : Attribute
    {
        public virtual string Nickname { get; }

        public NicknameAttribute(string Nickname)
        {
            this.Nickname = Nickname;
        }
    }

    internal class EquipesAttribute : Attribute
    {
        public virtual int[] Equipes { get; }

        public EquipesAttribute(int[] Equipes)
        {
            this.Equipes = Equipes;
        }
    }

    internal class EmailPagSeguroAttribute : Attribute
    {
        public virtual string EmailPagSeguro { get; }

        public EmailPagSeguroAttribute(string EmailPagSeguro)
        {
            this.EmailPagSeguro = EmailPagSeguro;
        }
    }

    internal class TokenPagSeguroAttribute : Attribute
    {
        public virtual string TokenPagSeguro { get; }

        public TokenPagSeguroAttribute(string TokenPagSeguro)
        {
            this.TokenPagSeguro = TokenPagSeguro;
        }
    }
}