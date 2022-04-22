<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======
﻿using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Data.Entities
{
    public class Newsletter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
<<<<<<< HEAD
        public string Whatsapp { get; set; }
=======
        public string Whatsapp { get; set; }        
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}