using Core.Models.Arquivos;
<<<<<<< HEAD
=======
using Core.Models.Eventos;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Linq;

namespace Core.Business.Arquivos
{
    public interface IArquivosBusiness
    {
        IQueryable<Data.Entities.Arquivo> GetArquivos();
        IQueryable<Data.Entities.Arquivo> GetArquivosByEvento(int eventoId);
        IQueryable<Data.Entities.Arquivo> GetArquivosByParticipante(int participanteId);
        IQueryable<Data.Entities.Arquivo> GetArquivosByEquipante(int equipanteId);
        IQueryable<Data.Entities.Arquivo> GetArquivosByEquipanteEvento(int equipanteId, int eventoId);

        IQueryable<Data.Entities.Arquivo> GetArquivosByLancamento(int lancamentoId);
        Data.Entities.Arquivo GetArquivoById(int id);
<<<<<<< HEAD
        int PostArquivo(PostArquivoModel model);
        void SetEquipante(int participanteId, int equipanteId);
=======
        void PostArquivo(PostArquivoModel model);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        void DeleteArquivo(int id);
        void DeleteFotoParticipante(int id);
        void DeleteFotoEquipante(int id);
    }
}
