using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.ApiModels
{
    public class ApiRanks
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Fee { get; set; }
    }
}
