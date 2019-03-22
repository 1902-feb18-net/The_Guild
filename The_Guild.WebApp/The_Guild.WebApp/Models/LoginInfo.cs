using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class LoginInfo
    {

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Pass { get; set; }

        public IEnumerable<Users> Users { get; set; }
    }
}
