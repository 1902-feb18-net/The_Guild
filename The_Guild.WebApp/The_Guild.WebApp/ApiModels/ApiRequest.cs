using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.ApiModels
{
    public class ApiRequest
    {
        public int Id { get; set; }        
        public int ProgressId { get; set; }
        public int? RankId { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Reward { get; set; }
        public string Descript { get; set; }
        public string Requirements { get; set; }
    }
}
