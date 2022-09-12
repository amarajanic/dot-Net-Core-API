using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string recipient, string body, string subject);
    }
}
