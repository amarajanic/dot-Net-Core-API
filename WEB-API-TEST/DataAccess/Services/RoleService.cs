using AutoMapper;
using DataAccess.DbContext;
using DataAccess.DbModel;
using DataAccess.DbModels;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class RoleService : IRoleService
    {
        private readonly TestDbContext _context;
        private readonly IMapper _mapper;
        public RoleService(TestDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<RoleDisplay>> GetAll()
        {  
           var dbRoles = await _context.Roles.ToListAsync();

            return _mapper.Map<List<RoleDisplay>>(dbRoles);

        }

        public async Task<RoleDisplay> GetById(int id)
        {
            var dbRole = await _context.Roles.Where(x => x.Id == id).FirstAsync();

            return _mapper.Map<RoleDisplay>(dbRole);

        }
    }
}
