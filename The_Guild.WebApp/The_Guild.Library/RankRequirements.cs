using System;
using System.Collections.Generic;
using System.Text;

namespace The_Guild.Library
{
    class RankRequirements
    {
        public int Id { get; set; }
        public int CurrentRankId { get; set; }
        public int NumberRequests { get; set; }
        public int MinTotalStats { get; set; }
        public int NextRankId { get; set; }

    }
}
