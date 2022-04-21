namespace Core.Models.Carona
{
    public class PostCaronaModel
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int MotoristaId { get; set; }
        public string Motorista { get; set; }
        public int Capacidade { get; set; }
    }
}
