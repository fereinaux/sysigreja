using Utils.Enums;

namespace Core.Models.CentroCusto
{
    public class PostCentroCustoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TiposCentroCustoEnum Tipo { get; set; }
    }
}
