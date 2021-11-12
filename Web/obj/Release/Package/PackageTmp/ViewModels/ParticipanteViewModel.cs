namespace SysIgreja.ViewModels
{
    public class ParticipanteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
        public string Apelido { get; set; }        
        public string Sexo { get; set; }
        public string Status { get; set; }
        public int Idade { get; set; }
        public string Fone { get; set; }       
        public bool PendenciaBoleto { get; set; }
        public bool Checkin { get; set; }
        public string DataCadastro { get; set; }
    }
}
