using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.ViewModel
{
    public class RankViewModel
    {
        public Ranks Rank { get; set; }
        public RankRequirements RankRequirements { get; set; }

        public RankViewModel() { }

        public RankViewModel(Ranks rank, RankRequirements rankRequirements)
        {
            Rank = rank;
            RankRequirements = rankRequirements;
        }
    }
}
