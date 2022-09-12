using AutoMapper;
using DataAccess.DbModel;
using DataAccess.DbModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AutoMapper
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<DbUser, UserDisplay>();
            CreateMap<UserInsert, DbUser>();
        }
    }
}
