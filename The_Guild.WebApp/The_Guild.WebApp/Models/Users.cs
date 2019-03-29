using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }


        [Range(0,900000)]
        public decimal? Salary { get; set; }

        
        [Range(0, 40)]
        public int? Strength { get; set; }
                
        [Range(0, 40)]
        public int? Dex { get; set; }
                
        [Range(0, 40)]
        public int? Wisdom { get; set; }
                
        [Range(0, 40)]
        public int? Intelligence { get; set; }
                
        [Range(0, 40)]
        public int? Charisma { get; set; }
                
        [Range(0, 40)]
        public int? Constitution { get; set; }
             
        [Range(0, 900000)]
        [Display(Name = "Rank")]
        public int? RankId { get; set; }
        public Rank Rank { get; set; }

        //public IEnumerable<AdventureParty> AdventureParty { get; set; }
        //public IEnumerable<RequestingParty> RequestingParty { get; set; }
    }
}
