using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserLogin
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
