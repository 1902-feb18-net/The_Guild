using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int? RankId { get; set; }

        [Required]
        public string Descript { get; set; }

        [Required]
        public string Requirements { get; set; }

        [Range(0.00, 900000.00)]
        public decimal? Reward { get; set; }

        [Range(0.00, 900000.00)]
        public decimal? Cost { get; set; }
        public int? ProgressId { get; set; }

        public IEnumerable<AdventureParty> AdventureParty { get; set; }
        public IEnumerable<RequestingParty> RequestingParty { get; set; }

    }
}
