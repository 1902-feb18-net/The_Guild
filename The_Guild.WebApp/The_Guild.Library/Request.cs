using System;
using System.Collections.Generic;
using System.Text;

namespace The_Guild.Library
{
    class Request
    {
        public int Id { get; set; }
        public int RankId { get; set; }
        public string Descript { get; set; }
        public string Requirements { get; set; }
        public decimal Reward { get; set; }
        public decimal Cost { get; set; }
        public int ProgressId { get; set; }

    }
}
