using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class Request
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Rank ID")]
        public int? RankId { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Descript { get; set; }

        [Required]
        public string Requirements { get; set; }

        [Range(0.00, 900000.00)]
        public decimal? Reward { get; set; }

        [Range(0.00, 900000.00)]
        public decimal? Cost { get; set; }

        [Display(Name = "Progress ID")]
        public int? ProgressId { get; set; }

        public IEnumerable<AdventureParty> AdventureParty { get; set; }
        public IEnumerable<RequestingGroup> RequestingParty { get; set; }

        public List<Users> Requesters { get; set; } = new List<Users>();
    }
}
