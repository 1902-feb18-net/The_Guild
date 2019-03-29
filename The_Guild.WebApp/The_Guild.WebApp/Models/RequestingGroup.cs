using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class RequestingGroup
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int CustomerId { get; set; }
    }
}
