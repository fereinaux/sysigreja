using Utils.Enums;

namespace Core.Models.Circulos
{
    public class PostCirculoModel
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int Dirigente1Id { get; set; }
        public int Dirigente2Id { get; set; }
        public CoresEnum Cor { get; set; }
    }
}
