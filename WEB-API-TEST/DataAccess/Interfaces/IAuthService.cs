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
        public Task<string> Register(UserRegister request);
        public Task<LoginResponse> Login(UserLogin request);
        public Task<LoginResponse> Refresh(string refresh);
    }
}
