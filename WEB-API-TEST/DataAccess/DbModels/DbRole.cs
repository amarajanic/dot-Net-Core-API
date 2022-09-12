using DataAccess.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbModels
{
    public class DbRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DbUser> Users { get; set; }
    }
}
