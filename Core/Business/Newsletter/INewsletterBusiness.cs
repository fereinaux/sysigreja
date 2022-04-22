<<<<<<< HEAD
﻿using System.Linq;
=======
﻿using Data.Entities;
using System.Linq;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Core.Business.Newsletter
{
    public interface INewsletterBusiness
    {
        IQueryable<Data.Entities.Newsletter> GetFones(string whatsapp);

        void InsertWhatsapp(string whatsapp);
    }
}
