using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class AdventureParty
    {
        [Required]
        public string Name { get; set; }

        public int Id { get; set; }
        public int? AdventurerId { get; set; }
        public int? RequestId { get; set; }
    }
}
