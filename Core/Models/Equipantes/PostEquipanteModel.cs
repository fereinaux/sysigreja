<<<<<<< HEAD
﻿using Core.Models.Etiquetas;
using System;
using System.Collections.Generic;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Utils.Enums;

namespace Core.Models.Equipantes
{
    public class PostEquipanteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Equipe { get; set; }
        public string Apelido { get; set; }
<<<<<<< HEAD
        public DateTime? DataNascimento { get; set; }
        public string[] Etiquetas { get; set; }
        public IEnumerable<PostEtiquetaModel> EtiquetasList { get; set; }
=======
        public DateTime DataNascimento { get; set; }
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public string Email { get; set; }
        public string Fone { get; set; }
        public bool HasRestricaoAlimentar { get; set; }
        public string RestricaoAlimentar { get; set; }
        public bool HasMedicacao { get; set; }
        public string Medicacao { get; set; }
        public bool HasAlergia { get; set; }
        public bool HasVacina { get; set; }
        public bool HasTeste { get; set; }
        public bool Checkin { get; set; }
<<<<<<< HEAD
        public bool Inscricao { get; set; }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public string Alergia { get; set; }
        public string Foto { get; set; }
        public SexoEnum Sexo { get; set; }
    }
}
