<<<<<<< HEAD
﻿using System.Web.Mvc;
=======
﻿using System.Web;
using System.Web.Mvc;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace SysIgreja
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new OutputCacheAttribute { VaryByParam = "*", Duration = 0, NoStore = true });
        }
    }
}
