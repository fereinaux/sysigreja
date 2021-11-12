using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Services
{
    public class EmailSettings
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String UsernameEmail { get; set; }
        public String UsernamePassword { get; set; }
        public String FromEmail { get; set; }
        public String ToEmail { get; set; }
        public String CcEmail { get; set; }
        public EmailSettings()
        {
            PrimaryDomain = "mail.encareasua.com.br";
            PrimaryPort = 25;
            UsernameEmail = "contato@encareasua.com.br";
            UsernamePassword = "seminario";
            FromEmail = "contato@encareasua.com.br";
        }
    }

}
