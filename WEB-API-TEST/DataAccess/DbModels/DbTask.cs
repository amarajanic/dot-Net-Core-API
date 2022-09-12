using DataAccess.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbModels
{
    public class DbTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DbUser User { get; set; }
        public int UserId { get; set; }
        public List<DbPerk> Perks { get; set; }

    }
}
