using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class RankRequirements
    {
        public int Id { get; set; }
        public int CurrentRankId { get; set; }
        public int NextRankId { get; set; }

        [Range(0, 900000)]
        public int NumberRequests { get; set; }

        [Range(0, 900000)]
        public int MinTotalStats { get; set; }
    }
}
