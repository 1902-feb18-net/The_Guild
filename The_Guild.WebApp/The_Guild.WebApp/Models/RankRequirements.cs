using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class RankRequirements
    {
        [Display(Name = "Rank Requirements ID")]
        public int Id { get; set; }

        [Display(Name = "Current Rank ID")]
        public int CurrentRankId { get; set; }

        [Display(Name = "Next Rank ID")]
        public int NextRankId { get; set; }

        [Display(Name = "Number of Requests Needed")]
        [Range(0, 900000)]
        public int NumberRequests { get; set; }

        [Display(Name = "Minimum Total Stats Needed")]
        [Range(0, 900000)]
        public int MinTotalStats { get; set; }
    }
}
