using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> Register(UserRegister request);
        public Task<string> Login(UserLogin request);
    }
}
