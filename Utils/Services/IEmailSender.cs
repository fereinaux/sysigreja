<<<<<<< HEAD
﻿namespace Utils.Services
=======
﻿using System.Threading.Tasks;

namespace Utils.Services
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
    }
}
