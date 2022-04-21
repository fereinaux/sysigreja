namespace SysIgreja.ViewModels
{
    public class InscricaoConcluidaViewModel
    {
        public int Id { get; set; }
        public string Apelido { get; set; }
        public string Logo { get; set; }
        public string Evento { get; set; }
        public string DataEvento { get; set; }
        public string Valor { get; set; }
        public string PadrinhoFone { get; internal set; }
        public string PadrinhoNome { get; internal set; }
    }
}
