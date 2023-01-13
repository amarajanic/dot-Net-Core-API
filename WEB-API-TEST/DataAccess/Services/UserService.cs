using DataAccess.Models;
using DataAccess.DbContext;
using DataAccess.DbModel;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.DbModels;
using MimeKit.Encodings;
using DataAccess.Helpers;
using Org.BouncyCastle.Asn1.Ocsp;

namespace DataAccess.Services
{
    public class UserService: IUserService
    {
        private readonly TestDbContext _context;

        private readonly IMapper _mapper;

        PasswordHelper passwordHelper = new PasswordHelper();
        public UserService(TestDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDisplay>> GetAll(int skip = 0, int take = 100)
        {
            var dbUsers = await  _context.Users.Include(x=>x.Role).Include(x=>x.Task).Skip(skip).Take(take).ToListAsync();

            var users = dbUsers.Select(x => new UserDisplay
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName,
                Role = new RoleDisplay() { Id = x.Role.Id, Name = x.Role.Name },
                RoleId = x.RoleId,
                Task = x.Task != null ? new TaskDisplay() { Id = x.Task.Id, Name = x.Task.Name, Description = x.Task.Description, UserId = x.Task.UserId } : null,
                TaskId = x.Task?.Id
            }).ToList();


            return users;
        }

        public async Task<UserDisplay> GetUserById(int id)
        {
            var dbUser = await _context.Users.Where(x=> x.Id == id).Include(x=> x.Role).FirstAsync();

            var user = new UserDisplay()
            {
                Id=id,
                FullName = dbUser.FirstName + " " + dbUser.LastName,
                Role = new RoleDisplay() { Id = dbUser.Role.Id, Name = dbUser.Role.Name },
                RoleId = dbUser.RoleId

            };

            return user;
        }

        public async Task<UserInsert> InsertUser(UserInsert user)
        {
            passwordHelper.CreatePasswordHash(user.Password, out byte[] passwordhash, out byte[] passwordSalt);
            var dbUser = new DbUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                PasswordHash = passwordhash,
                PasswordSalt = passwordSalt,
                RoleId = user.RoleId
            };
            //var dbUser = _mapper.Map<DbUser>(user);

            await _context.AddAsync(dbUser);
            await _context.SaveChangesAsync();

            user.Password = null; //temp solution for user display after insert, user password should not be returned
            return user;
        }

        public async Task<UserInsert> UpdateUser(UserInsert user, int id)
        {
            var dbUser = await _context.Users.Where(x => x.Id == id).FirstAsync();

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.RoleId = user.RoleId;

            _context.Update(dbUser);
            await _context.SaveChangesAsync();
            return user;        
        }

        public async Task<UserInsert> DeleteUser(int id)
        {
            var dbUser = await _context.Users.Where(x => x.Id == id).FirstAsync();
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();
            return new UserInsert { 
                FirstName = dbUser.FirstName, 
                LastName = dbUser.LastName, 
                //Role = dbUser.Role
                };
        }

        public async Task<List<UserDisplay>> GetUsersByRoleId(int roleId)
        {
            var dbUsers = await _context.Users.Where(x => x.RoleId == roleId).Include(y=> y.Role).ToListAsync();

            var users = dbUsers.Select(x => new UserDisplay
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName,
                Role = new RoleDisplay(){ Id = x.Role.Id, Name = x.Role.Name },
                RoleId = x.RoleId
            }).ToList();

            return users;
        }
    }
}
