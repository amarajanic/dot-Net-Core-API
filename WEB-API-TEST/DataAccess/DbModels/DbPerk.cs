using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbModels
{
    public class DbPerk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DbTask> Tasks { get; set; }
    }
}
