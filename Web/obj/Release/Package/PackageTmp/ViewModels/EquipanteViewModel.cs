namespace SysIgreja.ViewModels
{
    public class EquipanteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public int Idade { get; set; }
        public string Fone { get; set; }
        public string Equipe { get; set; }
        public string Sexo { get; set; }
        public bool Oferta { get; set; }
        public bool Vacina { get; set; }
        public bool Foto { get; set; }
        public int? Faltas { get; set; }
    }
}
