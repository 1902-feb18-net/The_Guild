using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class Rank
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.00, 900000.00)]
        public decimal Fee { get; set; }
    }
}
