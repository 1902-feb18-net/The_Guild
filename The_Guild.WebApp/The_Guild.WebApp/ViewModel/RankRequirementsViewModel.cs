using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using The_Guild.WebApp.ApiModels;
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.ViewModel
{
    public class RankRequirementsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Current Rank")]
        public int CurrentRankId { get; set; }

        [Display(Name = "Required Quests")]
        public int NumberRequests { get; set; }

        [Display(Name = "Required Min Stats")]
        public int MinTotalStats { get; set; }

        [Display(Name = "Next Rank")]
        public int NextRankId { get; set; } 

        public Ranks CurrentRank { get; set; }
        public Ranks NextRank { get; set; }
        public IEnumerable<ApiRanks> Ranks { get; set; }
    }
}
