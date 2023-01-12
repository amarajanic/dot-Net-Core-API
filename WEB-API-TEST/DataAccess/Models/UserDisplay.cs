using DataAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserDisplay
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public RoleDisplay Role { get; set; }
        public int RoleId { get; set; }
        public TaskDisplay? Task { get; set; }
        public int? TaskId { get; set; }
    }
}
