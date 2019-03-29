using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.ApiModels
{
    public class ApiRegister
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Salary { get; set; }
        public int? Strength { get; set; }
        public int? Dex { get; set; }
        public int? Wisdom { get; set; }
        public int? Intelligence { get; set; }
        public int? Charisma { get; set; }
        public int? Constitution { get; set; }
        public int? RankId { get; set; }

    }
}
