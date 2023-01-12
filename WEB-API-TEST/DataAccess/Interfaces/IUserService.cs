
using DataAccess.DbModel;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDisplay>> GetAll(int skip, int take);
        public Task<UserDisplay> GetUserById(int id);
        public Task<List<UserDisplay>> GetUsersByRoleId(int roleId);

        public Task<UserInsert> InsertUser(UserInsert user);
        public Task<UserInsert> UpdateUser(UserInsert user, int id);
        public Task<UserInsert> DeleteUser(int id);



    }
}
