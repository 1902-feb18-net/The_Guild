using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class Progress
    {
        public int Id { get; set; }

        [Required]
        public string Nam { get; set; }

        public IEnumerable<Request> Request { get; set; }
    }
}
