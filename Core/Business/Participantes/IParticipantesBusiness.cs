<<<<<<< HEAD
﻿using Core.Models.Participantes;
=======
﻿using Core.Models.Lancamento;
using Core.Models.Participantes;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Linq;

namespace Core.Business.Participantes
{
    public interface IParticipantesBusiness
    {
        Data.Entities.ParticipanteConsulta GetParticipanteConsulta(string email);
        IQueryable<Data.Entities.Participante> GetParticipantes();
        IQueryable<Data.Entities.Participante> GetParticipantesByEvento(int eventoId);
        IQueryable<Data.Entities.Participante> GetAniversariantesByEvento(int eventoId);
        IQueryable<Data.Entities.Participante> GetParentesByEvento(int eventoId);
        IQueryable<Data.Entities.Participante> GetRestricoesByEvento(int eventoId);
        Data.Entities.Participante GetParticipanteById(int id);
        Data.Entities.Participante GetParticipanteByReference(string reference);
        int PostInscricao(PostInscricaoModel model);
        void CancelarInscricao(int id);
        void TogglePendenciaContato(int id);
        void TogglePendenciaBoleto(int id);
        void ToggleVacina(int id);
        void ToggleTeste(int id);
        void ToggleCheckin(int id);
        void PostInfo(PostInfoModel model);
        void SolicitarBoleto(int id);
        void ToggleSexo(int id);
<<<<<<< HEAD
        void MakeEquipante(int id);
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}
